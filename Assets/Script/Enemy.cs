using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Unit
{
    [SerializeField] Player player;

    [SerializeField] protected Transform target;
    [SerializeField] protected Transform movePosi;
    [SerializeField] protected Animator ani;

    [SerializeField] protected float sight;
    [SerializeField] protected float findTime;
    [SerializeField] protected float waitDieSecond = 0.15f;

    [SerializeField] protected bool isPlayerinSight;
    [SerializeField] protected bool isEnemyDie;

    // ���� ��ũ��Ʈ���� ���������� ����Ǿ� �ϴ�
    protected void initialize()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<Player>();

        movePosi = target; // ������ ��ġ�� �ʱ⼳��
        StartCoroutine("checkPosi"); //findTime ���� ����
    }

    //������Ƽ
    public bool getIsPlayerinSight() { return isPlayerinSight; }
    public Transform getMovePosi() { return movePosi; }
    public float getSight() { return sight; }
    public float getSpeed() { return moveSpeed; }

    //�÷��̾�� �������� ����
    protected void attackPlayer() 
    {
        player.playerHp -= 1f;
    }


    // ���Ͱ� �ǰ�
    public void hitEnemy() 
    {
        hp -= player.playerAttack;
    }

    //���� hp�˻�
    protected bool isDie()
    {
        if (hp < 0)
            return true;
        else
            return false;
    }

    //Die �ִϸ��̼� 
    protected void deadAction()
    {
        ani.SetTrigger("isDie"); //���� Tirgger �Ķ���� Die
    }
    //�״� �̺�Ʈ���� ����
    protected void destroyObj()
    {
        Destroy(gameObject, waitDieSecond);
    }


    // collision �浹 ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�����̶� �浹�ϸ� 
        if (collision.gameObject.CompareTag("Tears"))
        {
            //Debug.Log("���� - ���� �浹");
            hitEnemy();
        }
        //�÷��̾�� �浹�ϸ�
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("���� - �÷��̾� �浹");
            attackPlayer();
        }
    }

    // player�� ã��
    protected void SearchingPlayer()
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

    //�����ð����� ������ posi ���ϱ�
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(findTime);
            movePosi = target; //���������� posi�� findTIme���� target�� ��ġ�� ������
            Debug.Log(movePosi.position);
        }
    }

}
