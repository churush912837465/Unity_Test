using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform target;
    //[SerializeField] Rigidbody enemyRb;
    float findTime = 1f;
    Transform movePosi;
    public bool isPlayerIn;
    public Enemy _enemy;

    [Header("--")]
    float speed = 4f;
    float sight = 5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GetComponent<Enemy>();

        movePosi = target; //�ʱ⿡�� target���� ����
        StartCoroutine("checkPosi"); //findTime ���� ����
    }

    void Update()
    {
        SearchingPlayer(); //�÷��̾� ���� 
        if (isPlayerIn) //�÷��̾ �����Ǹ�
        {
            moveEnemy();
        }
    }

    // ���� ǥ��
    private void OnDrawGizmos()
    {
        // enemy�� player�� �ν��ϴ� ����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);
    }

    //Playerã��
    public void SearchingPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sight); //���� ��ġ , ����
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
