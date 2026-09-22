

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configuración de vidas")]
    [SerializeField] private int startingLives = 3;

    public int Score { get; private set; }
    public int Lives { get; private set; }
    public bool IsGameOver { get; private set; }

    //marcador, vidas, panel de Game Over
    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;
    public event Action OnGameOver;

    private void Awake()
    {
        //si ya existe una instancia, esta se destruye.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Lives = startingLives;
        Score = 0;
        IsGameOver = false;

        OnLivesChanged?.Invoke(Lives);
        OnScoreChanged?.Invoke(Score);
    }

public void AddScore(int points)
    {
        if (IsGameOver) return;

        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    //resta una vida
    public void LoseLife()
    {
        if (IsGameOver) return;

        Lives = Mathf.Max(0, Lives - 1);
        OnLivesChanged?.Invoke(Lives);

        if (Lives <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
        Debug.Log("GAME OVER");
    }

    //recarga la escena actual
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
