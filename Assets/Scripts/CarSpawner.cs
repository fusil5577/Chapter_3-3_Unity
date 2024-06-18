using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float spawnInterval = 0.5f;  // 생성 간격
    public float horizontalDistance = 15f; // 수평 생성 거리
    public float verticalspawnDistance = 10f; // 수직 생성 거리
    public GameObject player; // 플레이어 객체

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

            // 무작위로 왼쪽 또는 오른쪽에서 자동차 생성
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // 랜덤하게 차량 종류 선택
            string[] carTags = { "HatchBack", "Police", "Sedan", "StationWagon", "Taxi" };
            string randomCarTag = carTags[Random.Range(0, carTags.Length)];

            GameObject car = GameManager.Instance.ObjectPool.SpawnFromPool(randomCarTag);

            if (car != null)
            {
                car.transform.position = spawnPosition;

                // 자동차의 위치와 회전 설정
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
            int randx = Random.Range(-1, 2); // -1, 0, 1 중 하나의 값을 반환
            int randy = Random.Range(-1, 2); // -1, 0, 1 중 하나의 값을 반환

            // randx와 randy가 둘 다 0이 아닐 때만 생성 위치 설정
            if (randx != 0 && randy != 0)
            {
                float spawnX = Mathf.Round(x) + randx * (int)Random.Range(-verticalspawnDistance, verticalspawnDistance) + 0.5f;
                float spawnZ = Mathf.Round(z) + randy * horizontalDistance + 0.4f;
                return new Vector3(spawnX, 0.5f, spawnZ);
            }
        }
    }
}