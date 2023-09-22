using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackFly : Enemy
{
    void Awake()
    {
        initialize();

        hp = 5f;
        moveSpeed = 3f;
        sight = 3f;
        findTime = 7f;
        waitDieSecond = 0.1f;
        isPlayerinSight = false;
        ani = GetComponent<Animator>();
    }
    
    void Update()
    {
        // 플레이어 searching , Die 구현
        SearchingPlayer();
        isEnemyDie = isDie();

        if (isEnemyDie)
        {
            deadAction(); //애니메이션 실행
        }
    }

   
}
