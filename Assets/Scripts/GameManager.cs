using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text bestScoreTxt;

    public ObjectPool ObjectPool { get; private set; }

    private float maxXPosition; // 플레이어 x축 최대 위치
    private int currentScore; // 현재 점수

    public PlayerController playerController;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        ObjectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        maxXPosition = Mathf.RoundToInt(playerController.transform.position.x); // 플레이어 초기 위치 설정
        currentScore = 0; // 점수 초기화
        UpdateUI();
    }

    private void Update()
    {
        // 플레이어의 x축 위치가 증가할 때마다 점수를 증가시킴
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
        // 게임 오버 처리
    }
}
