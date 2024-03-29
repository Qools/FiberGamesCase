using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LevelList levelList;

    [HideInInspector]
    public GameObject currentLevel;

    public bool isGameStarted = false;
    public bool isGameOver = false;

    private IEnumerator Start()
    {
        yield return StartCoroutine(DataManager.Instance.WaitInit());

        yield return StartCoroutine(MenuManager.Instance.WaitInit(MenuManager.Instance.Init));

        yield return StartCoroutine(GameController.Instance.WaitInit(GameController.Instance.Init));

        yield return StartCoroutine(WaitInit(Init));

        MenuManager.Instance.loadingScreen.SetActive(false);

        MenuManager.Instance.SwitchPanel<StartMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //GameOver();
        }
    }

    private void OnEnable()
    {
        BusSystem.OnGameOver += GameResult;
        BusSystem.OnStartGame += OnGameStarted;
    }

    private void OnDisable()
    {
        BusSystem.OnGameOver -= GameResult;
        BusSystem.OnStartGame -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        isGameStarted = true;
        isGameOver = false;
    }

    public void GameOver()
    {
        isGameStarted = false;
        isGameOver = true;
    }

    private void GameResult(GameResult gameResult)
    {
        isGameStarted = false;

        if (gameResult == global::GameResult.Win)
        {
            Win();
        }

        else
        {
            GameOver();
        }
    }

    public void Init()
    {
        SetStatus(Status.ready);
    }

    public void LoadLevel(int _level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levelList.LoopLevelsByIndex(_level));

        MenuManager.Instance.loadingScreen.SetActive(false);

        MenuManager.Instance.CloseAllPanels();

        BusSystem.CallNewLevelLoad();
    }

    public void LoadLevel(string _levelName)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levelList.LoopLevelsByName(_levelName));

        MenuManager.Instance.loadingScreen.SetActive(false);

        MenuManager.Instance.CloseAllPanels();

        BusSystem.CallNewLevelLoad();
    }

    public void Win()
    {
        DataManager.Instance.SetLevel(DataManager.Instance.GetLevel() + 1);

        isGameOver = true;
    }

}
