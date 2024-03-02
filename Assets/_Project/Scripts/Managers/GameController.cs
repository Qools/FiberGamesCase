using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : Singleton<GameController>
{
    [SerializeField] private float startLevelDelay;

    public void Init()
    { 
        SetStatus(Status.ready);
    }


    private void OnEnable()
    {
        BusSystem.OnLivesReduced += OnLivesReduced;
        BusSystem.OnNewLevelLoad += OnNewLevelLoad;
    }

    private void OnDisable()
    {
        BusSystem.OnLivesReduced -= OnLivesReduced;
        BusSystem.OnNewLevelLoad -= OnNewLevelLoad;
    }

    private void OnNewLevelLoad()
    {
        DOVirtual.DelayedCall(startLevelDelay, () => BusSystem.CallStartGame());
    }

    private void OnLivesReduced(int _lives)
    {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        if (_lives <= 0)
        {
            Debug.Log("Game Over");
            BusSystem.CallGameOver(GameResult.Lose);
        }
    }
}
