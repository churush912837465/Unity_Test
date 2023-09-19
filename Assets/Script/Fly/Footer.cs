using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : Enemy
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
    public float geneTime = 0; // 생성 초 
    public float fireTime = 1f; //쿨타임
    [SerializeField] Transform firePosi;

    //접촉 시 데미지 없음
    void Start()
    {
        hp = 8f;
        attackSpeed = 0.5f;
        attackRange = 0.5f;
        speed = 5f;
        sight = 5f;
        isPlayerin = false;
        isDelay = false;
        isEnemyDie = false;

        ani = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public int count = 0;

    void Update()
    {
        playerInAttackRange(); //Enemy상위에 있음
        isDie(); //hp가 0이하인지 검사 , isEnemyDie를 true로
        if (isEnemyDie)
        {
            Destroy(gameObject, waitforDie); //waitforDie만큼 기다렸다가 죽게
        }
        playerInSight(); //플레이어가 sight범위 안에 들어오면
        if (isPlayerinSight && count <= 1) 
        {
            Shoot();
            count = 0;
        }
    }


    void Shoot()
    {
        geneTime += Time.deltaTime;

        if (geneTime > fireTime) 
        {
            GameObject _bullt = Instantiate(bullet, firePosi.position, Quaternion.identity);

            count++;
            geneTime = 0;
        }
    }

   
    // ------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tears"))
        {
            enemyHit();
        }
    }

    // enemy가 player를 인식하는 범위
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
