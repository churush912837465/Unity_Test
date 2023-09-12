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

    // Player���� �������� �ִ�
    protected void takeDamage(float enemyDamage) 
    {
        player.playerHp -= enemyDamage;
    }
    
    // enemy�� �������� �Դ�
    protected void inDamage(float PlayerDamage)
    {
        this.hp -= PlayerDamage;
    }

    //enemy �� �״�
    

}
