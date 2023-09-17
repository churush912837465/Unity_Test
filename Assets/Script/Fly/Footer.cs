using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : Enemy
{
    //���� �� ������ ����
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
        SearchingPlayer(); //Enemy������ ����
        isDie(); //hp�� 0�������� �˻� , isEnemyDie�� true��
        if (isEnemyDie)
        { 
            Destroy(gameObject, waitforDie); //waitforDie��ŭ ��ٷȴٰ� �װ�
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

    // enemy�� player�� �ν��ϴ� ����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
