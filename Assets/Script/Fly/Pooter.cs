using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pooter : Enemy
{
    [Header("Pooter")]
    [SerializeField] float oriMoveSpeed;
    [SerializeField] float waitPlayAniSecond;
    [SerializeField] GameObject bullet;

    void Awake()
    {
        initialize();

        hp = 8f;
        moveSpeed = 3f;
        sight = 8f;
        findTime = 7f;
        bulletSpeed = 2f; // 총알 발사
        isPlayerinSight = false;
        isEnemyAttack = false;

        geneTime = 0;
        //geneObjCount = 0;

        ani = GetComponent<Animator>();

        //unit 상위 변수
        attackDelay = 3f; // 공격 쿨타임 
        oriMoveSpeed = moveSpeed;

        waitPlayAniSecond = 0.5f;
    }

    void Update()
    {
        // 플레이어 searching , Die 구현
        SearchingPlayer();
        if(isPlayerinSight) // 플레이어가 범위내에 들어오면 -> 공격
        {
            pooterAttack();
        }

        isEnemyDie = isDie();
        if (isEnemyDie)
        {
            deadAction();
        }
    }


    //이게 왜 되징
    void pooterAttack()
    {
        //총알발사
        geneTime += Time.deltaTime;
        if (geneTime >= waitPlayAniSecond && !isEnemyAttack)
        {
            moveSpeed = oriMoveSpeed;
            geneTime = 0;
            isEnemyAttack = true;
            shoot(); //총알 쏘기
        }

        if (geneTime >= attackDelay && isEnemyAttack) // 총알 쏘는 시간 잠시 speed = 0으로
        {
            ani.SetTrigger("isAttack");

            moveSpeed = 0;
            geneTime = 0;
            isEnemyAttack = false;
        }
    }

    private void shoot()
    {
        // pooter의 자식으로 생성하고
        // bullet 스크립트에서 부모 오브젝트enemy의 속도를 가져오는 식으로?
        GameObject b = Instantiate(tearPrefab, transform.position , transform.rotation) as GameObject;
        b.transform.parent = gameObject.transform; //자식오브젝트로 생성
    }
}
