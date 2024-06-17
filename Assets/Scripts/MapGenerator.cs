using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject player; // �÷��̾� ��ü
    public GameObject tilePrefab; // Ÿ�� ������
    public float tileSize = 2.0f; // Ÿ�� ũ��
    public int renderDistance = 5; // �÷��̾� �ֺ� Ÿ�� Ȱ��ȭ �Ÿ� (Ÿ�� ����)
    public float deactivateDistance = 20.0f; // ��Ȱ��ȭ �Ÿ�

    private Vector3 lastPlayerPosition; // �÷��̾��� ������ ��ġ ����� ����
    private Dictionary<Vector3, GameObject> activeTiles = new Dictionary<Vector3, GameObject>(); // Ȱ��ȭ�� Ÿ�� ����

    private void Start()
    {
        lastPlayerPosition = player.transform.position;
        UpdateMap();
    }

    private void Update()
    {
        Vector3 playerPosition = player.transform.position;

        // �÷��̾� ��ġ�� ������ ���� �� ������Ʈ
        if (playerPosition != lastPlayerPosition)
        {
            UpdateMap();
            lastPlayerPosition = playerPosition;
        }
    }

    private void UpdateMap()
    {
        Vector3 playerPosition = player.transform.position;

        // ��Ȱ��ȭ�� Ÿ�� ����
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

        // �÷��̾� �ֺ� Ÿ�� Ȱ��ȭ
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector3 tilePosition = new Vector3(
                    Mathf.Round(playerPosition.x / tileSize) * tileSize + x * tileSize,
                    0.5f,
                    Mathf.Round(playerPosition.z / tileSize) * tileSize + z * tileSize
                );

                // �̹� Ȱ��ȭ�� Ÿ������ Ȯ��
                if (!activeTiles.ContainsKey(tilePosition))
                {
                    GameObject tile = GameManager.Instance.ObjectPool.SpawnFromPool("TileTag"); // ������Ʈ Ǯ���� Ÿ�� ��������
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
