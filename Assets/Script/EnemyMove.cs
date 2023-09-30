using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum moveState
    {
        Prowl, //배회
        Tracking, //추적
        Nothing // 둘다 안하는 몬스터일 경우
    }
    public moveState mState;

    [Header("같이 붙어있는 enemy 스크립트")]
    [SerializeField] Enemy enemy;

    [Header("enemy애 있는 변수")]
    [SerializeField] float sight;
    [SerializeField] float speed;
    [SerializeField] bool isIn;
    [SerializeField] Transform movePosi;


    [Header("move변수")]
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float xRan;
    [SerializeField] float yRan;
    [SerializeField] float randRange; //랜덤으로 이동할 범위 
    [SerializeField] float fTime; //랜덤이동 시간

    void Start()
    {
        // enemy 초기 위치
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        xRan = x;
        yRan = y;
        randRange = 0.1f;
        fTime = 0.3f;

        // 초기설정
        enemy = GetComponent<Enemy>();
        sight = enemy.getSight();
        speed = enemy.getSpeed();

        StartCoroutine("checkPosi");
    }

    void Update()
    {
        // enemy 스크립트에서 실시간으로 받아옴
        speed = enemy.getSpeed(); //스피드 실시간으로 받아오기
        isIn = enemy.getIsPlayerinSight(); //범위 안에 들어왔는지 체크
        movePosi = enemy.getMovePosi(); //움직일 포지션 -> enemy클래스에서 검사한 movePosi의 위치

        // 만약 isIn이 true이면 -> 플레이어가 범위안에 들어오면
        // mSate는 Tracking상태로
        if (isIn)
        { 
            mState = moveState.Tracking;
        }

        //enum 상태변환
        switch (mState) 
        {
            case moveState.Prowl: // 배회
                {
                    prowlMove();
                    break;
                }
            case moveState.Tracking: //추적
                {
                    trackingMove();
                    break;
                }
            case moveState.Nothing: //둘다 안하는
                {
                    break;
                }
        }

    }

    // 배회 움직임
    public void prowlMove() 
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, speed * Time.deltaTime);
    }

    // find타임마다 랜덤 위치 반환
    IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(fTime);

            // x위치는 현재 위치 randRange부터 , 현재위치 -randRange까지
            // y위치 동일
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }

    //추적 움직임
    public void trackingMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosi.position, speed * Time.deltaTime);
    }

    // 범위보기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
