using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFly : Enemy
{
    void Start()
    {
        hp = 5f;
        attackSpeed = 0.5f;
        attackRange = 0.5f;
        speed = 5f;
        sight = 5f;
        isPlayerin = false;
        isDelay = false;
        isEnemyDie = false;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        sameChildMethod();
    }

    void sameChildMethod() 
    {
        playerInAttackRange(); //Enemy상위에 있음
        attackPlayer();
        isDie(); //hp가 0이하인지 검사 , isEnemyDie를 true로
        if (isEnemyDie)
        {
            ani.SetTrigger("isDie");
            Destroy(gameObject, waitforDie); //waitforDie만큼 기다렸다가 죽게
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
