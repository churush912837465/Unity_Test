using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("같이 붙어있는 enemy 스크립트")]
    [SerializeField] Enemy enemy;

    [Header("enemy애 있는 변수")]
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
        isIn = enemy.getIsPlayerinSight(); //범위 안에 들어왔는지 체크
        movePosi = enemy.getMovePosi(); //움직일 포지션 -> enemy클래스에서 검사한 movePosi의 위치
        if (isIn)
        {
            moveEnemy();
        }
    }

    public void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosi.position, speed * Time.deltaTime);
    }

    // 범위보기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
