using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;
    //[SerializeField] Rigidbody enemyRb;
    [SerializeField] float findTime = 1f;

    [Header("������� �޾ƿ�")]
    [SerializeField] Transform movePosi;
    [SerializeField] float speed = 2f;
    [SerializeField] float sight = 5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        movePosi = target; //�ʱ⿡�� target���� ����
        StartCoroutine("checkPosi"); //findTime ���� ����

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
            movePosi = target; //���������� posi�� findTIme���� target�� ��ġ�� ������
            Debug.Log(movePosi.position.x + " " + movePosi.position.y + " " + movePosi.position.z);
        }
    }

    // ���� ǥ��
    /*
    private void OnDrawGizmos()
    {
        // enemy�� player�� �ν��ϴ� ����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , sight);
    }
    */
    // Player����
    /*
    public bool SearchingPlayer() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sight); //���� ��ġ , ����
        // sightũ���� �� ��ŭ�� Sphere���� Collider����
        // OverlapSphere()���

        if (colliders.Length >= 0)  //���� ��������
        {
            for (int i = 0; i < colliders.Length; i++) 
            {
                if (colliders[i].CompareTag("Player"))
                {
                    return true; //�÷��̾ ���������� true return
                }
            }
        }

        return false;
    }
    */

}
