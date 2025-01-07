using UnityEngine;

public class GameUIButtons : MonoBehaviour
{
    public void OnNewLevelButtonPressed()
    {
        GameManager.Instance.ResetGame();
    }

    public void OnReturnToMenuButtonPressed()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
