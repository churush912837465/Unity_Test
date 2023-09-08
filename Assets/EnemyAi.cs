using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;
    //[SerializeField] Rigidbody enemyRb;
    [SerializeField] float findTime = 1f;

    [Header("상속으로 받아올")]
    [SerializeField] Transform movePosi;
    [SerializeField] float speed = 2f;
    [SerializeField] float sight = 5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        movePosi = target; //초기에는 target으로 지정
        StartCoroutine("checkPosi"); //findTime 마다 실행

    }

    void Update()
    {
        moveEnemy();
    }

    public void moveEnemy() 
    {
        transform.position = Vector3.MoveTowards(transform.position , movePosi.position , speed * Time.deltaTime);
        //enemyRb.AddForce(movePosi.position);
        //enemyRb.velocity = movePosi.position * Time.deltaTime * speed;
        //enemyRb.MovePosition(movePosi.position);
    }

    IEnumerator checkPosi() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(findTime);
            movePosi = target; //움직여야할 posi는 findTIme마다 target의 위치를 가져옴
            Debug.Log(movePosi.position.x + " " + movePosi.position.y + " " + movePosi.position.z);
        }
    }

    // 범위 표현
    /*
    private void OnDrawGizmos()
    {
        // enemy가 player를 인식하는 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , sight);
    }
    */
    // Player감지
    /*
    public bool SearchingPlayer() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sight); //시작 위치 , 범위
        // sight크기의 원 만큼의 Sphere안의 Collider추출
        // OverlapSphere()사용

        if (colliders.Length >= 0)  //뭔가 들어왔으면
        {
            for (int i = 0; i < colliders.Length; i++) 
            {
                if (colliders[i].CompareTag("Player"))
                {
                    return true; //플레이어를 감지했으면 true return
                }
            }
        }

        return false;
    }
    */

}
