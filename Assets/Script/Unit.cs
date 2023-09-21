using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //플레이어와 Enemy가 둘다 가지고 있는 스탯

    [SerializeField] protected float hp; // 체력
    [SerializeField] protected float moveSpeed; //이동 속도

    protected float attackDelay; //공격 속도
    protected float attackRange; //공격 사거리
    protected float attackDamage; // 공격 데미지
    protected float tearSpeed; // 투사체 속도
 
}
