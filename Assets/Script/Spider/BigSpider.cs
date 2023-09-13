using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpider : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 10f;
        attackSpeed = 0f;
        speed = 5f;
        sight = 7f;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
