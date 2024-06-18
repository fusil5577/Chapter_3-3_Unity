using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f; // 자동차 속도
    private Vector3 direction = Vector3.forward; // 이동 방향
    public float deactivateDistance = 20f; // 비활성화 거리
    private GameObject player; // 플레이어 객체

    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어 객체 찾기
    }

    private void Update()
    {
        MoveCar();
        CheckDistanceToPlayer();
    }

    private void MoveCar()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckDistanceToPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > deactivateDistance)
        {
            gameObject.SetActive(false); // 일정 거리 이상 멀어지면 비활성화
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
