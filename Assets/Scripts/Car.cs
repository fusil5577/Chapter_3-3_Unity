using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f; // �ڵ��� �ӵ�
    public Vector3 direction = Vector3.forward; // �̵� ����

    private void Update()
    {
        MoveCar();
    }

    private void MoveCar()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
