using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGameButtonPressed()
    {
        GameManager.Instance.LoadGame();
    }

    public void OnCreditsButtonPressed()
    {
        GameManager.Instance.SwitchScene("Credits");
    }

    public void OnInstructionsButtonPressed()
    {
        GameManager.Instance.SwitchScene("Instructions");
    }

    public void OnMainMenuButtonPressed()
    {
        GameManager.Instance.SwitchScene("MainMenu");
    }
}
