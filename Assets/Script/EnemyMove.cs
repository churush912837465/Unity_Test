using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("���� �پ��ִ� enemy ��ũ��Ʈ")]
    [SerializeField] Enemy enemy;

    [Header("enemy�� �ִ� ����")]
    [SerializeField] float sight;
    [SerializeField] float speed;
    [SerializeField] bool isIn;
    [SerializeField] Transform movePosi;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        sight = enemy.getSight();
        speed = enemy.getSpeed();
    }

    void Update()
    {
        isIn = enemy.getIsPlayerinSight(); //���� �ȿ� ���Դ��� üũ
        movePosi = enemy.getMovePosi(); //������ ������ -> enemyŬ�������� �˻��� movePosi�� ��ġ
        if (isIn)
        {
            moveEnemy();
        }
    }

    public void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosi.position, speed * Time.deltaTime);
    }

    // ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
