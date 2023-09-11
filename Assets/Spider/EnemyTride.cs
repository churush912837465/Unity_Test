using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTride : Enemy
{
    void Start()
    {
        getTrideInfo(); // spider - tride Á¤º¸ 
    }

    void getTrideInfo()
    {
        hp = 10f;
        attackSpeed = 0;
        speed = 20f;
        sight = 10f;
    }
}
