using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moter : Enemy
{
    [Header("MoggerFly")]
    [SerializeField] bool motterDie;
    public GameObject attackFly;
    public int copyAttackFly = 10; // �����ö��̸� �ΰ�

    // ������ Attack Fly �θ��� ����
    void Awake()
    {
        initialize();

        hp = 10f;
        moveSpeed = 3f;
        sight = 3f;
        findTime = 7f;
        geneTime = 0; // ���� �� 
        geneObjCount = 0; // �ִ� ������

        isPlayerinSight = false;
        //ani = GetComponent<Animator>();
    }

    void Update()
    {
        // �÷��̾� searching , Die ����
        SearchingPlayer();
        isEnemyDie = isDie();

        if (isEnemyDie)
        {
            if (geneObjCount >= copyAttackFly)
            {
                return;
            }

            gengeAttackFly();
            Destroy(gameObject, waitDieSecond);
        }
    }

    private void gengeAttackFly()
    {
        geneTime += Time.deltaTime;
        if (geneTime >= 0.1f)
        {
            //GameObject gameObject = Instantiate(attackFly, transform.position, Quaternion.identity) as GameObject;
            Instantiate(attackFly, transform.position, Quaternion.identity);
            geneObjCount++;
            geneTime = 0;
        }
    }

}
