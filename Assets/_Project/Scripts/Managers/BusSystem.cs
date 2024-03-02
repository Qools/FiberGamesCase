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
}
