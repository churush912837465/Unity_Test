using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] Player player;
    [SerializeField] protected GameObject tearPrefab;

    [SerializeField] protected Transform target;
    [SerializeField] protected Transform movePosi;
    [SerializeField] protected Animator ani;

    [SerializeField] protected float sight; // 시야 범위
    [SerializeField] protected float findTime; // 플레이어 찾는 쿨타임
    [SerializeField] protected float waitDieSecond = 0.7f ; // 죽기전 wait

    [SerializeField] protected float hp; // 체력
    [SerializeField] protected float moveSpeed; //이동 속도
    [SerializeField] protected float attackDelay; //공격 속도
    [SerializeField] protected float bulletSpeed; //투사체 속도

    [SerializeField] protected float geneTime; // 생성 시간 : 0으로 초기화
    [SerializeField] protected int geneObjCount; // 현재 생성한 카운터 : 0으로 초기화

    [SerializeField] protected bool isPlayerinSight; // 플레이어가 범위안에 있는가?
    [SerializeField] protected bool isEnemyDie; // 적이 죽었는가?
    [SerializeField] protected bool isEnemyAttack; // 적이 공격을 받고 있는가?

    // 하위 스크립트에서 공통적으로 실행되야 하는
    protected void initialize()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<Player>();

        movePosi = target; // 움직일 위치를 초기설정
        StartCoroutine("checkPosi"); //findTime 마다 실행
    }

    //프로퍼티
    public bool getIsPlayerinSight() { return isPlayerinSight; }
    public Transform getMovePosi() { return movePosi; }
    public float getSight() { return sight; }
    public float getSpeed() { return moveSpeed; }
    public float getBulletSpped() { return bulletSpeed; }
    public float getWaitDieSecond() { return waitDieSecond; }

    //플레이어에게 데미지를 입힘
    protected void attackPlayer() 
    {
        player.playerHp -= 1f;
    }


    // 몬스터가 피격
    public void hitEnemy() 
    {
        hp -= 1f;
    }

    //몬스터 hp검사
    protected bool isDie()
    {
        if (hp <= 0)
            return true;
        else
            return false;
    }

    // Die 했을 때
    protected void deadAction()
    {
        ani.SetBool("isDie" , true); //공통 Tirgger 파라미터 Die
        Destroy(gameObject, waitDieSecond);
    }
 
    // collision 충돌 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //눈물이랑 충돌하면 
        if (collision.gameObject.CompareTag("Tears"))
        {
            //Debug.Log("몬스터 - 눈물 충돌");
            hitEnemy();
        }
        //플레이어랑 충돌하면
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("몬스터 - 플레이어 충돌");
            attackPlayer();
        }
    }

    // player을 찾음
    protected void SearchingPlayer()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 vector2 = new Vector2(x, y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(vector2, sight); //시작 위치 , 범위
        // sight크기의 원 만큼의 Sphere안의 Collider추출
        // OverlapSphere()사용

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerinSight = true; //플레이어를 감지했으면 true return
                return; //함수탈출
            }

        }
        isPlayerinSight = false;
        return;
    }

    //일정시간마다 움직일 posi 구하기
    IEnumerator checkPosi()
    {
        yield return new WaitForSeconds(findTime);
        movePosi = target; //움직여야할 posi는 findTIme마다 target의 위치를 가져옴
        //Debug.Log(movePosi.position);

    }

}
