using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotterFly : Enemy
{
    // MotterFly�� �״� �ִϸ��̼� x -> ������ ������Ʈ ���� �� AttackFly ��ȯ
    [Header("MoggerFly")]
    [SerializeField] bool motterDie;
    public GameObject attackFly;
    public int geneObjCount = 0;
    public int copyAttackFly = 2; // �����ö��̸� �ΰ�
    public float geneTime = 0; // ���� �� 

    void Start()
    {
        hp = 10f;
        attackSpeed = 0.5f;
        attackRange = 0.5f;
        speed = 5f;
        sight = 5f;
        waitforDie = 0.5f;
        isPlayerin = false;
        isDelay = false;

        motterDie = false;
    }

    void Update()
    {
        sameChildMethod();

        if (motterDie) 
        {
            if(geneObjCount >= copyAttackFly) 
            {
                return;
            }
            gengeAttackFly();
            Destroy(gameObject , waitforDie);
        }
    }
        
    void sameChildMethod()
    {
        playerInAttackRange(); //Enemy������ ����
        attackPlayer();
        isDie(); //hp�� 0�������� �˻� , isEnemyDie�� true��
        if (isEnemyDie)
        {
            //�����ö��̸� �̺κ� �ٸ�
            motterDie = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tears"))
        {
            enemyHit();
        }
    }

    // enemy�� player�� �ν��ϴ� ����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
