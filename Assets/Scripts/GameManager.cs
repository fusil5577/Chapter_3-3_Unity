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

    private float maxXPosition; // �÷��̾� x�� �ִ� ��ġ
    private int currentScore; // ���� ����

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
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
