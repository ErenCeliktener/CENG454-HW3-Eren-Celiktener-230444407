using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [Header("Observed Systems")]
    [SerializeField] private CoreHealth coreHealth;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Result UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private bool gameEnded;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnCoreDestroyed += HandleCoreDestroyed;
        }

        if (enemySpawner != null)
        {
            enemySpawner.OnAllWavesCompleted += HandleAllWavesCompleted;
        }
    }

    private void OnDisable()
    {
        if (coreHealth != null)
        {
            coreHealth.OnCoreDestroyed -= HandleCoreDestroyed;
        }

        if (enemySpawner != null)
        {
            enemySpawner.OnAllWavesCompleted -= HandleAllWavesCompleted;
        }
    }

    private void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    private void HandleCoreDestroyed()
    {
        if (gameEnded)
        {
            return;
        }

        LoseGame();
    }

    private void HandleAllWavesCompleted()
    {
        if (gameEnded)
        {
            return;
        }

        WinGame();
    }

    private void WinGame()
    {
        gameEnded = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    private void LoseGame()
    {
        gameEnded = true;

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}