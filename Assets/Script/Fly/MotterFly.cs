using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotterFly : Enemy
{
    // MotterFly는 죽는 애니메이션 x -> 죽으면 오브젝트 삭제 후 AttackFly 소환
    [Header("MoggerFly")]
    [SerializeField] bool motterDie;
    public GameObject attackFly;
    public int geneObjCount = 0;
    public int copyAttackFly = 2; // 어택플라이를 두개
    public float geneTime = 0; // 생성 초 

    void Start()
    {
        hp = 10f;
        attackSpeed = 0.5f;
        attackRange = 0.5f;
        speed = 5f;
        sight = 5f;
        waitforDie = 0.5f;
        isPlayerin = false;
        isDelay = false;

        motterDie = false;
    }

    void Update()
    {
        sameChildMethod();

        if (motterDie) 
        {
            if(geneObjCount >= copyAttackFly) 
            {
                return;
            }
            gengeAttackFly();
            Destroy(gameObject , waitforDie);
        }
    }
        
    void sameChildMethod()
    {
        playerInAttackRange(); //Enemy상위에 있음
        attackPlayer();
        isDie(); //hp가 0이하인지 검사 , isEnemyDie를 true로
        if (isEnemyDie)
        {
            //모터플라이만 이부분 다름
            motterDie = true;
        }
    }

    private void gengeAttackFly()
    {
        geneTime += Time.deltaTime;
        if (geneTime >= 0.1f) 
        {
            //GameObject gameObject = Instantiate(attackFly, transform.position, Quaternion.identity) as GameObject;
            Instantiate(attackFly, transform.position, Quaternion.identity);
            geneObjCount++;
            geneTime = 0;
        }
    }

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
