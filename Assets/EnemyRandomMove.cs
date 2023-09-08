using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random; //Random "모호합니다" 오류

public class EnemyRandomMove : MonoBehaviour
{
    float speed = 2f;
    float x , y ;
    float xRan , yRan ;

    [Header("상속으로 가져올것")]
    float randRange = 5f;
    float findTime = 1f;
    Transform movePosi;

    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;

        StartCoroutine("checkPosi");
    }

    void Update()
    {
        moveRandom();
    }

    public void moveRandom()
    {
        Vector3 moveRan = new Vector3(xRan, yRan , transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, speed * Time.deltaTime);
        //MoveToward : 본인 위치 , 움직일 위치 , 속도
    }

    // find타임마다 움직일 위치 업데이트
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(findTime);

            // x위치는 현재 위치 +5부터 , 현재위치 -5까지
            // y위치 동일
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }
}
