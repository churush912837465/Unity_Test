using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] Player player;
    [SerializeField] protected GameObject tearPrefab;

    [SerializeField] protected Transform target;
    [SerializeField] protected Transform movePosi;
    [SerializeField] protected Animator ani;

    [SerializeField] protected float sight; // �þ� ����
    [SerializeField] protected float findTime; // �÷��̾� ã�� ��Ÿ��
    [SerializeField] protected float waitDieSecond = 0.7f ; // �ױ��� wait

    [SerializeField] protected float hp; // ü��
    [SerializeField] protected float moveSpeed; //�̵� �ӵ�
    [SerializeField] protected float attackDelay; //���� �ӵ�
    [SerializeField] protected float bulletSpeed; //����ü �ӵ�

    [SerializeField] protected float geneTime; // ���� �ð� : 0���� �ʱ�ȭ
    [SerializeField] protected int geneObjCount; // ���� ������ ī���� : 0���� �ʱ�ȭ

    [SerializeField] protected bool isPlayerinSight; // �÷��̾ �����ȿ� �ִ°�?
    [SerializeField] protected bool isEnemyDie; // ���� �׾��°�?
    [SerializeField] protected bool isEnemyAttack; // ���� ������ �ް� �ִ°�?

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
    public float getBulletSpped() { return bulletSpeed; }
    public float getWaitDieSecond() { return waitDieSecond; }

    //�÷��̾�� �������� ����
    protected void attackPlayer() 
    {
        player.playerHp -= 1f;
    }


    // ���Ͱ� �ǰ�
    public void hitEnemy() 
    {
        hp -= 1f;
    }

    //���� hp�˻�
    protected bool isDie()
    {
        if (hp <= 0)
            return true;
        else
            return false;
    }

    // Die ���� ��
    protected void deadAction()
    {
        ani.SetBool("isDie" , true); //���� Tirgger �Ķ���� Die
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
        yield return new WaitForSeconds(findTime);
        movePosi = target; //���������� posi�� findTIme���� target�� ��ġ�� ������
        //Debug.Log(movePosi.position);

    }

}
