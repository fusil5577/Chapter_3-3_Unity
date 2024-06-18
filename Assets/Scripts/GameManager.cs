using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text bestScoreTxt;

    public ObjectPool ObjectPool { get; private set; }

    private float maxXPosition; // �÷��̾� x�� �ִ� ��ġ
    private int currentScore; // ���� ����

    public PlayerController playerController;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        ObjectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        maxXPosition = Mathf.RoundToInt(playerController.transform.position.x); // �÷��̾� �ʱ� ��ġ ����
        currentScore = 0; // ���� �ʱ�ȭ
        UpdateUI();
    }

    private void Update()
    {
        // �÷��̾��� x�� ��ġ�� ������ ������ ������ ������Ŵ
        int currentPlayerPosition = Mathf.RoundToInt(playerController.transform.position.x);
        if (currentPlayerPosition < maxXPosition)
        {
            currentScore++;
            maxXPosition = currentPlayerPosition;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (bestScoreTxt != null)
        {
            bestScoreTxt.text = currentScore.ToString();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // ���� ���� ó��
    }
}
