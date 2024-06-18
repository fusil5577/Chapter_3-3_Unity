using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 5f; // �ڵ��� �ӵ�
    private Vector3 direction = Vector3.forward; // �̵� ����
    public float deactivateDistance = 20f; // ��Ȱ��ȭ �Ÿ�
    private GameObject player; // �÷��̾� ��ü

    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // �÷��̾� ��ü ã��
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
            gameObject.SetActive(false); // ���� �Ÿ� �̻� �־����� ��Ȱ��ȭ
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
