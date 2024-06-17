using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f; // 자동차 속도
    public Vector3 direction = Vector3.forward; // 이동 방향

    private void Update()
    {
        MoveCar();
    }

    private void MoveCar()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
