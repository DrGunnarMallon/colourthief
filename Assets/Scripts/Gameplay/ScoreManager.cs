using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    private void OnEnable()
    {
        EventsManager.Instance.OnRestartGame += ResetScore;
        EventsManager.Instance.OnNewLevel += ResetScore;
        EventsManager.Instance.OnRestartLevel += ResetScore;
        EventsManager.Instance.OnReturnToMenu += ResetScore;

        EventsManager.Instance.OnIncreaseScore += IncreaseScore;
        EventsManager.Instance.OnDecreaseScore += DecreaseScore;
        EventsManager.Instance.OnResetScore += ResetScore;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnRestartGame -= ResetScore;
        EventsManager.Instance.OnNewLevel -= ResetScore;
        EventsManager.Instance.OnRestartLevel -= ResetScore;
        EventsManager.Instance.OnReturnToMenu -= ResetScore;

        EventsManager.Instance.OnIncreaseScore -= IncreaseScore;
        EventsManager.Instance.OnDecreaseScore -= DecreaseScore;
        EventsManager.Instance.OnResetScore -= ResetScore;
    }

    #region Score Management

    private void IncreaseScore(int amount = 1)
    {
        score += amount;
        EventsManager.Instance.TriggerScoreChanged(score);
    }

    private void DecreaseScore(int amount = 1)
    {
        score -= amount;
        EventsManager.Instance.TriggerScoreChanged(score);
    }

    private void ResetScore()
    {
        score = 0;
        EventsManager.Instance.TriggerScoreChanged(score);
    }

    public int GetScore() => score;

    #endregion
}
