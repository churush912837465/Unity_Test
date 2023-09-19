using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform movePosi;

    [SerializeField] float findTime = 1f;
    [SerializeField] bool isPlayerIn;
    [SerializeField] Enemy _enemy;
    //[SerializeField] Transform fireposi;

    [Header("�ش� Enemy���� �ٸ�")]
    [SerializeField] float speed;
    [SerializeField] float sight;
    [SerializeField] float attackRange;
    [SerializeField] bool isFilpped; // ȸ���ϸ� true, �ƴϸ� false
    //[SerializeField] bool ifFirePosiFilpped; //ȸ���� ���� firePosi �ű��

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GetComponent<Enemy>();

        // �ش� ���ӿ�����Ʈ�� �پ��ִ� enemy ��ũ��Ʈ (��ӵǾ�����) ��Ҹ� ����
        speed = _enemy.getSpeed();
        sight = _enemy.getSight();
        attackRange = _enemy.getAttackRange();

        movePosi = target; //�ʱ⿡�� target���� ����   
        isFilpped = false;
        //ifFirePosiFilpped = false;
        StartCoroutine("checkPosi"); //findTime ���� ����
    }

    void Update()
    {
        SearchingToMove(); //�÷��̾� ���� 
        if (isPlayerIn) //�÷��̾ �����Ǹ�
        {
            moveEnemy();
            lookAtPlayer();
        }
        //rotateFirePosi();

    }

    // ���� ǥ��
    private void OnDrawGizmos()
    {
        // enemy�� player�� �ν��ϴ� ����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight); //���� ��ġ���� , ���� sight��ŭ (���� ���� �ٸ�)
    }


    // �÷��̾� ��ġ ���� 180�� ������
    public void lookAtPlayer() 
    {
        if (transform.position.x < target.position.x && isFilpped ) //���� --  �÷��̾� 
        {
            transform.Rotate(0f, 180f, 0f);
            isFilpped = false;
        }
        if (transform.position.x > target.position.x && !isFilpped) //�÷��̾� -- ����
        {
            transform.Rotate(0f, 180f, 0f);
            isFilpped = true;
        }
    }


    // �÷��̾� ��ġ ���� �Ѿ� �߻� posi ������
    /*
    public void rotateFirePosi()
    {
        if (transform.position.x < target.position.x && ifFirePosiFilpped) //���� --  �÷��̾� 
        {
            fireposi.Rotate(0f, 180f, 0f);
            ifFirePosiFilpped = false;
        }
        if (transform.position.x > target.position.x && !ifFirePosiFilpped) //�÷��̾� -- ����
        {
            fireposi.Rotate(0f, 180f, 0f);
            ifFirePosiFilpped = true;
        }
    }
    */


    //Playerã��
    public void SearchingToMove()
    {

        float x = transform.position.x;
        float y  = transform.position.y;
        Vector2 vector2 = new Vector2(x, y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(vector2, sight); //���� ��ġ , ����
        // sightũ���� �� ��ŭ�� Sphere���� Collider����
        // OverlapSphere()���

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerIn = true; //�÷��̾ ���������� true return
                return; //�Լ�Ż��
            }

        }
        isPlayerIn = false;
        return;
    }

    //enemy ������
    public void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosi.position, speed * Time.deltaTime);
        //enemyRb.AddForce(movePosi.position);
        //enemyRb.velocity = movePosi.position * Time.deltaTime * speed;
        //enemyRb.MovePosition(movePosi.position);
    }

    // findTIme���� Player����
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(findTime);
            movePosi = target; //���������� posi�� findTIme���� target�� ��ġ�� ������
        }
    }
}
