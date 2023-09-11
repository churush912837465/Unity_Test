using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpdier : Enemy
{

    void Start() 
    {
        getSpiderInfo(); // spider - spider Á¤º¸ 

    }

    void getSpiderInfo() 
    {
        hp = 7f;
        attackSpeed = 0;
        speed = 10f;
        sight = 5f;
    }
}
