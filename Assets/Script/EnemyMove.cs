using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum moveState
    {
        Prowl, //��ȸ
        Tracking, //����
        Nothing // �Ѵ� ���ϴ� ������ ���
    }
    public moveState mState;

    [Header("���� �پ��ִ� enemy ��ũ��Ʈ")]
    [SerializeField] Enemy enemy;

    [Header("enemy�� �ִ� ����")]
    [SerializeField] float sight;
    [SerializeField] float speed;
    [SerializeField] bool isIn;
    [SerializeField] Transform movePosi;


    [Header("move����")]
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float xRan;
    [SerializeField] float yRan;
    [SerializeField] float randRange; //�������� �̵��� ���� 
    [SerializeField] float fTime; //�����̵� �ð�

    void Start()
    {
        // enemy �ʱ� ��ġ
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        xRan = x;
        yRan = y;
        randRange = 0.1f;
        fTime = 0.3f;

        // �ʱ⼳��
        enemy = GetComponent<Enemy>();
        sight = enemy.getSight();
        speed = enemy.getSpeed();

        StartCoroutine("checkPosi");
    }

    void Update()
    {
        // enemy ��ũ��Ʈ���� �ǽð����� �޾ƿ�
        speed = enemy.getSpeed(); //���ǵ� �ǽð����� �޾ƿ���
        isIn = enemy.getIsPlayerinSight(); //���� �ȿ� ���Դ��� üũ
        movePosi = enemy.getMovePosi(); //������ ������ -> enemyŬ�������� �˻��� movePosi�� ��ġ

        // ���� isIn�� true�̸� -> �÷��̾ �����ȿ� ������
        // mSate�� Tracking���·�
        if (isIn)
        { 
            mState = moveState.Tracking;
        }

        //enum ���º�ȯ
        switch (mState) 
        {
            case moveState.Prowl: // ��ȸ
                {
                    prowlMove();
                    break;
                }
            case moveState.Tracking: //����
                {
                    trackingMove();
                    break;
                }
            case moveState.Nothing: //�Ѵ� ���ϴ�
                {
                    break;
                }
        }

    }

    // ��ȸ ������
    public void prowlMove() 
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, speed * Time.deltaTime);
    }

    // findŸ�Ӹ��� ���� ��ġ ��ȯ
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(fTime);

            // x��ġ�� ���� ��ġ randRange���� , ������ġ -randRange����
            // y��ġ ����
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }

    //���� ������
    public void trackingMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosi.position, speed * Time.deltaTime);
    }

    // ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
