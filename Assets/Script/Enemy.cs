using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy :MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float speed;
    [SerializeField] protected float sight;
    [SerializeField] protected int damage = 1; // 데미지는 1로 고정
    [SerializeField] protected float waitforDie;

    [SerializeField] protected bool isPlayerin; // attack범위 안에 들어오면 true로
    [SerializeField] protected bool isPlayerinSight;
    [SerializeField] protected bool isDelay;
    [SerializeField] protected bool isEnemyDie;

    [SerializeField] protected Player player;
    [SerializeField] protected int pHp;
    [SerializeField] protected float pDamage;

    [SerializeField] protected Animator ani;
    void Awake()
    {
        //Player 태그가 붙은 게임오브젝트의 Player스크립트 들고오기
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pDamage = 1f;
        pHp = 5;

    }

    private void Update()
    {
        
    }

    // 프로퍼티
    public float getHp() { return hp; }
    public float getAttackSpeed() { return attackSpeed; }
    public float getAttackRange() {  return attackRange; }
    public float getSpeed() { return speed;}
    public float getSight() { return sight; }

    //플레이어에게 데미지를 주는
    protected void getDamage() 
    {
        pHp -= damage;
    }

    // Enemy가 데미지 입는
    protected void enemyHit() 
    {
        hp -= pDamage; 
    }

    // 죽었을때 : 죽는애니메이션 실행 ,  게임오브젝트 삭제
    protected void isDie() 
    {
        if (hp < 0)
            isEnemyDie = true;
    }

    //플레이어 attack 최종
    protected void attackPlayer()
    {
        if (isPlayerin && isDelay == false)
        {
            isDelay = true;

            StartCoroutine("getCoolTime");
            getDamage();
        }
    }

    // 닿이면 공격하는
    protected void playerInAttackRange()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 vector2 = new Vector2(x, y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(vector2, attackRange); //시작 위치 , 범위
        // sight크기의 원 만큼의 Sphere안의 Collider추출
        // OverlapSphere()사용

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerin = true; //플레이어를 감지했으면 true return
                return; //함수탈출
            }

        }
        isPlayerin = false;
        return;
    }

    //sight안에 들어오면 검사
    protected void playerInSight()
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

    // enemy 공격 쿨타임
    IEnumerator getCoolTime()
    {
        yield return new WaitForSeconds(attackSpeed);
        isDelay = false;
    }

}
