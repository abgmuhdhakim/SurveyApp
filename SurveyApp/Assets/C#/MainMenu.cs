using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject RegisterCanvas;
    public GameObject LoginCanvas;
    public GameObject MainMenuCanvas;

    public void OnRegisterCanvasButtonClick()
    {
        RegisterCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
        LoginCanvas.SetActive(false);
    }

    public void OnLoginCanvasButtonClick()
    {
        LoginCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
        RegisterCanvas.SetActive(false);
    }

    public void OnMainMenuCanvasButtonClick()
    {
        MainMenuCanvas.SetActive(true);
        RegisterCanvas.SetActive(false);
        LoginCanvas.SetActive(false);
    }
}
