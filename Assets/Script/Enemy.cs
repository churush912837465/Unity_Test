using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy :MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float speed;
    [SerializeField] protected float sight;
    [SerializeField] protected int damage = 1; // �������� 1�� ����
    [SerializeField] protected float waitforDie;

    [SerializeField] protected bool isPlayerin; // attack���� �ȿ� ������ true��
    [SerializeField] protected bool isPlayerinSight;
    [SerializeField] protected bool isDelay;
    [SerializeField] protected bool isEnemyDie;

    [SerializeField] protected Player player;
    [SerializeField] protected int pHp;
    [SerializeField] protected float pDamage;

    [SerializeField] protected Animator ani;
    void Awake()
    {
        //Player �±װ� ���� ���ӿ�����Ʈ�� Player��ũ��Ʈ ������
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pDamage = 1f;
        pHp = 5;

    }

    private void Update()
    {
        
    }

    // ������Ƽ
    public float getHp() { return hp; }
    public float getAttackSpeed() { return attackSpeed; }
    public float getAttackRange() {  return attackRange; }
    public float getSpeed() { return speed;}
    public float getSight() { return sight; }

    //�÷��̾�� �������� �ִ�
    protected void getDamage() 
    {
        pHp -= damage;
    }

    // Enemy�� ������ �Դ�
    protected void enemyHit() 
    {
        hp -= pDamage; 
    }

    // �׾����� : �״¾ִϸ��̼� ���� ,  ���ӿ�����Ʈ ����
    protected void isDie() 
    {
        if (hp < 0)
            isEnemyDie = true;
    }

    //�÷��̾� attack ����
    protected void attackPlayer()
    {
        if (isPlayerin && isDelay == false)
        {
            isDelay = true;

            StartCoroutine("getCoolTime");
            getDamage();
        }
    }

    // ���̸� �����ϴ�
    protected void playerInAttackRange()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 vector2 = new Vector2(x, y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(vector2, attackRange); //���� ��ġ , ����
        // sightũ���� �� ��ŭ�� Sphere���� Collider����
        // OverlapSphere()���

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerin = true; //�÷��̾ ���������� true return
                return; //�Լ�Ż��
            }

        }
        isPlayerin = false;
        return;
    }

    //sight�ȿ� ������ �˻�
    protected void playerInSight()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 vector2 = new Vector2(x, y);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(vector2, sight); //���� ��ġ , ����
        // sightũ���� �� ��ŭ�� Sphere���� Collider����
        // OverlapSphere()���

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerinSight = true; //�÷��̾ ���������� true return
                return; //�Լ�Ż��
            }

        }
        isPlayerinSight = false;
        return;
    }

    // enemy ���� ��Ÿ��
    IEnumerator getCoolTime()
    {
        yield return new WaitForSeconds(attackSpeed);
        isDelay = false;
    }

}
