using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance { get; private set; }

    #region Events

    public event Action<int> OnScoreChanged;
    public event Action<int> OnIncreaseScore;
    public event Action<int> OnDecreaseScore;
    public event Action OnResetScore;

    public event Action OnRestartGame;
    public event Action OnRestartLevel;
    public event Action OnNewLevel;
    public event Action OnNextLevel;
    public event Action OnReturnToMenu;

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

    public void TriggerRestartGame() { OnRestartGame?.Invoke(); }
    public void TriggerRestartLevel() { OnRestartLevel?.Invoke(); }
    public void TriggerNewLevel() { OnNewLevel?.Invoke(); }
    public void TriggerNextLevel() { OnNextLevel?.Invoke(); }
    public void TriggerReturnToMenu() { OnReturnToMenu?.Invoke(); }

    #endregion

    #region Score Management Event Triggers

    public void TriggerIncreaseScore(int amount) { OnIncreaseScore?.Invoke(amount); }
    public void TriggerDecreaseScore(int amount) { OnDecreaseScore?.Invoke(amount); }
    public void TriggerResetScore() { OnResetScore?.Invoke(); }

    #endregion
}
