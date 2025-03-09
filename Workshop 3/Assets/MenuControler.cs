using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    void Start()
    {
        // Lägg till lyssnare på knapparna för att anropa rätt metod när de trycks
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        // Ladda spel-scenen
        SceneManager.LoadScene("Game"); // Ändra till din spelscen
    }

    public void QuitGame()
    {
        // Avsluta applikationen
        Application.Quit();
    }
}
