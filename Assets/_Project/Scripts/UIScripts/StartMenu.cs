using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : UIPanel
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => StartGame());
    }

    public void StartGame()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());
    }
}
