using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text bestScoreTxt;

    public ObjectPool ObjectPool { get; private set; }

    private float maxXPosition; // 플레이어 x축 최대 위치
    private int currentScore; // 현재 점수

    public PlayerController playerController;
    public GameObject gameOverUI;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (Instance != null) Destroy(gameObject);
        Instance = this;

        ObjectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
        StopAllCoroutines();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
