using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //�÷��̾�� Enemy�� �Ѵ� ������ �ִ� ����

    [SerializeField] protected float hp; // ü��
    [SerializeField] protected float moveSpeed; //�̵� �ӵ�
    [SerializeField] protected float attackDelay; //���� �ӵ�

    protected float attackRange; //���� ��Ÿ�
    protected float attackDamage; // ���� ������
    protected float tearSpeed; // ����ü �ӵ�
 
}
