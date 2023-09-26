using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomfly : Enemy
{
    void Awake()
    {
        initialize();

        hp = 5f;
        moveSpeed = 3f;
        sight = 3f;
        findTime = 7f;
        isPlayerinSight = false;
        isEnemyDie = false;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // �÷��̾� searching , Die ����
        SearchingPlayer();
        isEnemyDie = isDie();

        if (isEnemyDie)
        {
            deadAction(); //�ִϸ��̼� ����
        }
    }
}
