using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Textos UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Pantalla Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        //eventos programados en el GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged += UpdateScore;
            GameManager.Instance.OnLivesChanged += UpdateLives;
            GameManager.Instance.OnGameOver += ShowGameOver;

            //textos
            UpdateScore(GameManager.Instance.Score);
            UpdateLives(GameManager.Instance.Lives);
        }

        //panel esté oculto al iniciar
        gameOverPanel.SetActive(false);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Puntaje: " + score;
    }

    private void UpdateLives(int lives)
    {
        livesText.text = "Vidas: " + lives;
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}