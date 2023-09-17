using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : Enemy
{
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
    }

    void Update()
    {
        sameChildMethod();
    }

    void sameChildMethod()
    {
        SearchingPlayer(); //Enemy상위에 있음
        isDie(); //hp가 0이하인지 검사 , isEnemyDie를 true로
        if (isEnemyDie)
        { 
            Destroy(gameObject, waitforDie); //waitforDie만큼 기다렸다가 죽게
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
