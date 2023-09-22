using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pooter : Enemy
{
    [Header("Pooter")]
    [SerializeField] float oriMoveSpeed;
    [SerializeField] float waitPlayAniSecond;

    void Awake()
    {
        initialize();

        hp = 8f;
        moveSpeed = 3f;
        sight = 8f;
        findTime = 7f;
        waitDieSecond = 0.1f;
        isPlayerinSight = false;
        isEnemyAttack = false;

        geneTime = 0;
        //geneObjCount = 0;

        ani = GetComponent<Animator>();

        //unit ���� ����
        attackDelay = 5f; // ���� ��Ÿ��
        oriMoveSpeed = moveSpeed;

        waitPlayAniSecond = 0.5f;
    }

    void Update()
    {
        // �÷��̾� searching , Die ����
        SearchingPlayer();
        if(isPlayerinSight) // �÷��̾ �������� ������ -> ����
        {
            pooterAttack();
        }

        isEnemyDie = isDie();
        if (isEnemyDie)
        {
            deadAction();
        }
    }


    //�̰� �� ��¡
    void pooterAttack()
    {
        //�Ѿ˹߻�
        geneTime += Time.deltaTime;
        if (geneTime >= waitPlayAniSecond && !isEnemyAttack)
        {
            moveSpeed = oriMoveSpeed;
            geneTime = 0;
            isEnemyAttack = true;
        }

        if (geneTime >= attackDelay && isEnemyAttack) // �Ѿ� ��� �ð� ��� speed = 0����
        {
            ani.SetTrigger("isAttack");

            moveSpeed = 0;
            geneTime = 0;
            isEnemyAttack = false;
        }
    }
}
