using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class WaveProgressView : MonoBehaviour
{
    [SerializeField] private Slider _waveProgressSlider;
    [SerializeField] private TextMeshProUGUI _waveCountText;
    [SerializeField] private CanvasGroup _canvasGroup;

    private int _enemyCount;

    private void Start()
    {
        _waveProgressSlider.DOValue(_waveProgressSlider.maxValue, 0.1f);
    }

    private void OnEnable()
    {
        BusSystem.OnWavesCount += _setWaveCountText;
        BusSystem.OnGameOver += _disableWaveProgress;
        BusSystem.OnStartGame += _enableWaveProgress;
        BusSystem.OnCurrentWaveEnemyCount += _setWaveProgressSlider;
        BusSystem.OnEnemyDestroyed += _updateWaveProgressSlider;
    }

    private void OnDisable()
    {
        BusSystem.OnWavesCount -= _setWaveCountText;
        BusSystem.OnGameOver -= _disableWaveProgress;
        BusSystem.OnStartGame -= _enableWaveProgress;
        BusSystem.OnCurrentWaveEnemyCount -= _setWaveProgressSlider;
        BusSystem.OnEnemyDestroyed -= _updateWaveProgressSlider;
    }

    private void _setWaveCountText(int waveCount, int currentWaveId)
    {
        _waveCountText.text = PlayerPrefKeys.wave + " " + currentWaveId.ToString() + "/" + waveCount.ToString();
    }

    private void _setWaveProgressSlider(int enemyCount)
    {
        _waveProgressSlider.maxValue = enemyCount;
        _waveProgressSlider.DOValue(enemyCount, 0.1f);

        _enemyCount = enemyCount;
    }

    private void _updateWaveProgressSlider()
    {
        _enemyCount--;
        _waveProgressSlider.DOValue(_enemyCount, 0.5f);
    }

    private void _enableWaveProgress()
    {
        _canvasGroup.alpha = 1f;
    }

    private void _disableWaveProgress(GameResult gameResult)
    {
        _canvasGroup.alpha = 0f;
    }
}
