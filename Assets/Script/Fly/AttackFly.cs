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
    }

    void Update()
    {
        SearchingPlayer();
        attackPlayer();

    }

    // enemy가 player를 인식하는 범위
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
