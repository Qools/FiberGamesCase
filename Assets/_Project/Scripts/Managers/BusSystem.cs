using System;

public static class BusSystem
{
    public static Action OnStartGame;
    public static void CallStartGame() => OnStartGame?.Invoke();

    public static Action<GameResult> OnGameOver;
    public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);

    public static Action OnNewLevelLoad;
    public static void CallNewLevelLoad() => OnNewLevelLoad?.Invoke();

    public static Action OnEnemyDestroyed;
    public static void CallEnemyDestroyed() => OnEnemyDestroyed?.Invoke();

    public static Action<int> OnLivesReduced;
    public static void CallLivesReduced(int _lives) => OnLivesReduced?.Invoke(_lives);

    public static Action<int> OnCurrentWaveEnemyCount;
    public static void CallCurrentWaveEnemyCount(int enemyCount) => OnCurrentWaveEnemyCount?.Invoke(enemyCount);
    
    public static Action<int, int> OnWavesCount;
    public static void CallWavesCount(int totalWavesCount, int currentWaveId) => OnWavesCount?.Invoke(totalWavesCount, currentWaveId);
}
