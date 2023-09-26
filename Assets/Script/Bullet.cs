using UnityEngine;

public class Bullet : MonoBehaviour
{
    //���� ��� ������Ʈ�� (ex)pooter �� ������ ����

    [SerializeField] Transform target;
    [SerializeField] Vector3 destination;
    [SerializeField] Animator animator;

    [SerializeField] Enemy _enemy;
    [SerializeField] float speed;
    [SerializeField] float betDistance;
    [SerializeField] bool isReadyDestroy;
    [SerializeField] float wait;

    void Start()
    {
        animator = GetComponent<Animator>();

        _enemy = gameObject.transform.parent.GetComponent<Enemy>();  // ������ �����Ǵ� enemy �� ��ũ��Ʈ�� ������
        speed = _enemy.getBulletSpped(); // �θ��� enemy ��ũ��Ʈ���� bulletSpeed ��������

        target = GameObject.FindGameObjectWithTag("Player").transform;
        destination = new Vector3(target.position.x , target.position.y , 0);
        isReadyDestroy = false;

        wait = 0.5f;
    }

    void Update()
    {
        if (_enemy == null) 
        {
            return;
        }

        //�Ѿ� ������
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        isReadyDestroy = isReachDestination();
        if (isReadyDestroy) 
        {
            animator.SetTrigger("isDestory");
            Destroy(gameObject , wait);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //�÷��̾�� �浹�ϸ� ����
        {
            animator.SetTrigger("isDestory");
            Destroy(gameObject , wait);
        }
        if (collision.gameObject.CompareTag("Wall")) //���̶� �浹�ϸ� ����
        {
            animator.SetTrigger("isDestory");
            Destroy(gameObject, wait);
        }
    }

    // destination��ġ�� ���� (<) �ϸ� ���� �ִϸ��̼�
    // �÷��̾�� ��Ƶ� ����
    bool isReachDestination() 
    {
        betDistance = Vector3.Distance(transform.position, destination);
        if (betDistance < 0.05f)
            return true;
        return false;
    }
    
}
