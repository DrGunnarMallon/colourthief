using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance { get; private set; }

    #region Events

    public event Action<int> OnScoreChanged;
    public event Action<int> OnIncreaseScore;

    public event Action OnResetGame;
    public event Action OnRestartLevel;
    public event Action OnNewLevel;
    public event Action OnNextLevel;
    public event Action OnReturnToMenu;
    public event Action OnLevelCompleted;

    #endregion

    #region UI Events

    public event Action<ColorData> OnTargetColorChanged;
    public event Action<ColorData> OnMixingColorChanged;

    #endregion

    // Singleton Pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region UI Event Triggers

    public void TriggerScoreChanged(int score) { OnScoreChanged?.Invoke(score); }

    #endregion

    #region Game Management Event Triggers

    public void TriggerRestartLevel() { OnRestartLevel?.Invoke(); }
    public void TriggerResetGame() { OnResetGame?.Invoke(); }
    public void TriggerNewLevel() { OnNewLevel?.Invoke(); }
    public void TriggerNextLevel() { OnNextLevel?.Invoke(); }
    public void TriggerReturnToMenu() { OnReturnToMenu?.Invoke(); }
    public void TriggerLevelCompleted() { OnLevelCompleted?.Invoke(); }

    #endregion

    #region Score Management Event Triggers

    public void TriggerIncreaseScore(int amount) { OnIncreaseScore?.Invoke(amount); }

    #endregion

    #region UI Event Triggers

    public void TriggerTargetColorChanged(ColorData data) { OnTargetColorChanged?.Invoke(data); }
    public void TriggerMixingColorChanged(ColorData data) { OnMixingColorChanged?.Invoke(data); }

    #endregion

}
