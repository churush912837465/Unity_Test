using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform movePosi;

    [SerializeField] float findTime = 1f;
    [SerializeField] bool isPlayerIn;
    [SerializeField] Enemy _enemy;

    [Header("해당 Enemy마다 다름")]
    [SerializeField] float speed;
    [SerializeField] float sight;
    [SerializeField] float attackRange;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GetComponent<Enemy>();
        
        // 해당 게임오브젝트에 붙어있는 enemy 스크립트 (상속되어있음) 요소를 들고옴
        speed = _enemy.getSpeed();
        sight = _enemy.getSight();
        attackRange = _enemy.getAttackRange();

        movePosi = target; //초기에는 target으로 지정
        StartCoroutine("checkPosi"); //findTime 마다 실행
    }

    void Update()
    {
        SearchingPlayer(); //플레이어 감지 
        if (isPlayerIn) //플레이어가 감지되면
        {
            moveEnemy();
        }
    }

    // 범위 표현
    private void OnDrawGizmos()
    {
        // enemy가 player를 인식하는 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight); //현재 위치에서 , 몬스터 sight만큼 (몬스터 마다 다름)
    }

    //Player찾기
    public void SearchingPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sight); //시작 위치 , 범위
        // sight크기의 원 만큼의 Sphere안의 Collider추출
        // OverlapSphere()사용

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerIn = true; //플레이어를 감지했으면 true return
                return; //함수탈출
            }

        }
        isPlayerIn = false;
        return;
    }

    //enemy 움직임
    public void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosi.position, speed * Time.deltaTime);
        //enemyRb.AddForce(movePosi.position);
        //enemyRb.velocity = movePosi.position * Time.deltaTime * speed;
        //enemyRb.MovePosition(movePosi.position);
    }

    // findTIme마다 Player감지
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(findTime);
            movePosi = target; //움직여야할 posi는 findTIme마다 target의 위치를 가져옴
        }
    }
}
