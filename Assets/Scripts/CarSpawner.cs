using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float spawnInterval = 0.5f;  // ���� ����
    public float horizontalDistance = 15f; // ���� ���� �Ÿ�
    public float verticalspawnDistance = 10f; // ���� ���� �Ÿ�
    public GameObject player; // �÷��̾� ��ü

    private float timer;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = spawnInterval;

            // �������� ���� �Ǵ� �����ʿ��� �ڵ��� ����
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // �����ϰ� ���� ���� ����
            string[] carTags = { "HatchBack", "Police", "Sedan", "StationWagon", "Taxi" };
            string randomCarTag = carTags[Random.Range(0, carTags.Length)];

            GameObject car = GameManager.Instance.ObjectPool.SpawnFromPool(randomCarTag);

            if (car != null)
            {
                car.transform.position = spawnPosition;

                // �ڵ����� ��ġ�� ȸ�� ����
                if (spawnPosition.z > player.transform.position.z)
                {
                    car.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    car.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                car.SetActive(true);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;

        while (true)
        {
            int randx = Random.Range(-1, 2); // -1, 0, 1 �� �ϳ��� ���� ��ȯ
            int randy = Random.Range(-1, 2); // -1, 0, 1 �� �ϳ��� ���� ��ȯ

            // randx�� randy�� �� �� 0�� �ƴ� ���� ���� ��ġ ����
            if (randx != 0 && randy != 0)
            {
                float spawnX = Mathf.Round(x) + randx * (int)Random.Range(-verticalspawnDistance, verticalspawnDistance) + 0.5f;
                float spawnZ = Mathf.Round(z) + randy * horizontalDistance + 0.4f;
                return new Vector3(spawnX, 0.5f, spawnZ);
            }
        }
    }
}