using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random; //Random "��ȣ�մϴ�" ����

public class EnemyRandomMove : MonoBehaviour
{
    float speed = 2f;
    float x , y ;
    float xRan , yRan ;

    [Header("������� �����ð�")]
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
        //MoveToward : ���� ��ġ , ������ ��ġ , �ӵ�
    }

    // findŸ�Ӹ��� ������ ��ġ ������Ʈ
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(findTime);

            // x��ġ�� ���� ��ġ +5���� , ������ġ -5����
            // y��ġ ����
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }
}
