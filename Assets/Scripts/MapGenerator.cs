using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject player; // 플레이어 객체
    public GameObject tilePrefab; // 타일 프리팹
    public float tileSize = 2.0f; // 타일 크기
    public int renderDistance = 5; // 플레이어 주변 타일 활성화 거리 (타일 단위)
    public float deactivateDistance = 20.0f; // 비활성화 거리

    private Vector3 lastPlayerPosition; // 플레이어의 마지막 위치 저장용 변수
    private Dictionary<Vector3, GameObject> activeTiles = new Dictionary<Vector3, GameObject>(); // 활성화된 타일 관리

    private void Start()
    {
        lastPlayerPosition = player.transform.position;
        UpdateMap();
    }

    private void Update()
    {
        Vector3 playerPosition = player.transform.position;

        // 플레이어 위치가 변했을 때만 맵 업데이트
        if (playerPosition != lastPlayerPosition)
        {
            UpdateMap();
            lastPlayerPosition = playerPosition;
        }
    }

    private void UpdateMap()
    {
        Vector3 playerPosition = player.transform.position;

        // 비활성화된 타일 제거
        List<Vector3> tilesToRemove = new List<Vector3>();
        foreach (var tilePos in activeTiles.Keys)
        {
            float distance = Vector3.Distance(playerPosition, tilePos);
            if (distance > deactivateDistance)
            {
                activeTiles[tilePos].SetActive(false);
                tilesToRemove.Add(tilePos);
            }
        }
        foreach (var tilePos in tilesToRemove)
        {
            activeTiles.Remove(tilePos);
        }

        // 플레이어 주변 타일 활성화
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector3 tilePosition = new Vector3(
                    Mathf.Round(playerPosition.x / tileSize) * tileSize + x * tileSize,
                    0.5f,
                    Mathf.Round(playerPosition.z / tileSize) * tileSize + z * tileSize
                );

                // 이미 활성화된 타일인지 확인
                if (!activeTiles.ContainsKey(tilePosition))
                {
                    GameObject tile = GameManager.Instance.ObjectPool.SpawnFromPool("TileTag"); // 오브젝트 풀에서 타일 가져오기
                    if (tile != null)
                    {
                        tile.transform.position = tilePosition;
                        tile.SetActive(true);
                        activeTiles.Add(tilePosition, tile);
                    }
                }
                else
                {
                    activeTiles[tilePosition].SetActive(true);
                }
            }
        }
    }
}
