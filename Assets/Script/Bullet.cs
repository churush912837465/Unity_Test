using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총을 쏘는 오브젝트의 (ex)pooter 의 하위에 생김

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

        _enemy = gameObject.transform.parent.GetComponent<Enemy>();  // 상위에 생성되는 enemy 의 스크립트를 가져옴
        speed = _enemy.getBulletSpped(); // 부모의 enemy 스크립트에서 bulletSpeed 가져오기

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

        //총알 움직임
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
        if (collision.gameObject.CompareTag("Player")) //플레이어랑 충돌하면 삭제
        {
            animator.SetTrigger("isDestory");
            Destroy(gameObject , wait);
        }
        if (collision.gameObject.CompareTag("Wall")) //벽이랑 충돌하면 삭제
        {
            animator.SetTrigger("isDestory");
            Destroy(gameObject, wait);
        }
    }

    // destination위치에 도달 (<) 하면 폭팔 애니메이션
    // 플레이어랑 닿아도 폭팔
    bool isReachDestination() 
    {
        betDistance = Vector3.Distance(transform.position, destination);
        if (betDistance < 0.05f)
            return true;
        return false;
    }
    
}
