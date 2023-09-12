using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float speed;
    [SerializeField] protected float sight;

    [SerializeField] Player player;

    //public abstract void info();

    // speed return
    public float getSpeed() { return speed;}

    // Player에게 데미지를 주는
    protected void takeDamage(float enemyDamage) 
    {
        player.playerHp -= enemyDamage;
    }
    
    // enemy가 데미지를 입는
    protected void inDamage(float PlayerDamage)
    {
        this.hp -= PlayerDamage;
    }

    //enemy 가 죽는
    

}
