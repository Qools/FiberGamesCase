using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameMenu : UIPanel
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;

    public TextMeshProUGUI endGameText;
    public TextMeshProUGUI endGameWaveText;
    public TextMeshProUGUI endGameEnemyText;

    private void Start()
    {
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        BusSystem.OnGameOver += OnGameEnd;
    }

    public void OnDisable()
    {
        BusSystem.OnGameOver -= OnGameEnd;
    }

    private void OnGameEnd(GameResult gameResult)
    {
        MenuManager.Instance.SwitchPanel<EndGameMenu>();

        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        if (gameResult == GameResult.Win)
        {
            Win();
        }

        else
        {
            GameOver();
        }
        
    }

    private void Win()
    {
        nextLevelButton.gameObject.SetActive(true);

        endGameText.text = "Level " + DataManager.Instance.GetLevel().ToString() + " Completed";
        endGameWaveText.text = "Killed Enemy" + " " + PlayerStats.KilledEnemy.ToString();
        endGameEnemyText.text = "Survived Waves" + " " + PlayerStats.Rounds.ToString();
    }

    private void GameOver()
    {
        retryButton.gameObject.SetActive(true);

        endGameText.text = "Level Failed";
        endGameWaveText.text = "Killed Enemy" + " " + PlayerStats.KilledEnemy.ToString();
        endGameEnemyText.text = "Survived Waves" + " " + PlayerStats.Rounds.ToString();
    }

    public void OnNextButtonPressed()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());

        MenuManager.Instance.CloseAllPanels();
    }

    public void OnRetryButtonPressed()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());

        MenuManager.Instance.CloseAllPanels();
    }
}
