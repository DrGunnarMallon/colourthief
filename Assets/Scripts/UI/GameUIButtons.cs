using UnityEngine;

public class GameUIButtons : MonoBehaviour
{
    public void OnNewLevelButtonPressed()
    {
        GameManager.Instance.LoadNewLevel();
    }

    public void OnReturnToMenuButtonPressed()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
