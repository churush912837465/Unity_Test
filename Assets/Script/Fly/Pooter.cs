using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pooter : Enemy
{
    [Header("Pooter")]
    [SerializeField] float oriMoveSpeed;
    [SerializeField] float waitPlayAniSecond;
    [SerializeField] GameObject bullet;

    void Awake()
    {
        initialize();

        hp = 8f;
        moveSpeed = 3f;
        sight = 8f;
        findTime = 7f;
        bulletSpeed = 2f; // �Ѿ� �߻�
        isPlayerinSight = false;
        isEnemyAttack = false;

        geneTime = 0;
        //geneObjCount = 0;

        ani = GetComponent<Animator>();

        //unit ���� ����
        attackDelay = 3f; // ���� ��Ÿ�� 
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
            shoot(); //�Ѿ� ���
        }

        if (geneTime >= attackDelay && isEnemyAttack) // �Ѿ� ��� �ð� ��� speed = 0����
        {
            ani.SetTrigger("isAttack");

            moveSpeed = 0;
            geneTime = 0;
            isEnemyAttack = false;
        }
    }

    private void shoot()
    {
        // pooter�� �ڽ����� �����ϰ�
        // bullet ��ũ��Ʈ���� �θ� ������Ʈenemy�� �ӵ��� �������� ������?
        GameObject b = Instantiate(tearPrefab, transform.position , transform.rotation) as GameObject;
        b.transform.parent = gameObject.transform; //�ڽĿ�����Ʈ�� ����
    }
}
