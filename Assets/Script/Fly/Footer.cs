using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : Enemy
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
    public float geneTime = 0; // ���� �� 
    public float fireTime = 1f; //��Ÿ��
    [SerializeField] Transform firePosi;

    //���� �� ������ ����
    void Start()
    {
        hp = 8f;
        attackSpeed = 0.5f;
        attackRange = 0.5f;
        speed = 5f;
        sight = 5f;
        isPlayerin = false;
        isDelay = false;
        isEnemyDie = false;

        ani = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public int count = 0;

    void Update()
    {
        playerInAttackRange(); //Enemy������ ����
        isDie(); //hp�� 0�������� �˻� , isEnemyDie�� true��
        if (isEnemyDie)
        {
            Destroy(gameObject, waitforDie); //waitforDie��ŭ ��ٷȴٰ� �װ�
        }
        playerInSight(); //�÷��̾ sight���� �ȿ� ������
        if (isPlayerinSight && count <= 1) 
        {
            Shoot();
            count = 0;
        }
    }


    void Shoot()
    {
        geneTime += Time.deltaTime;

        if (geneTime > fireTime) 
        {
            GameObject _bullt = Instantiate(bullet, firePosi.position, Quaternion.identity);

            count++;
            geneTime = 0;
        }
    }

   
    // ------------------------------------------
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
