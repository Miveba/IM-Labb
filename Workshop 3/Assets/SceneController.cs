using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadNewScene(string sceneName)
    {
        // Hämta den aktuella scenen
        Scene currentScene = SceneManager.GetActiveScene();

        // Hämta alla objekt i den aktuella scenen och ta bort dem
        foreach (GameObject obj in currentScene.GetRootGameObjects())
        {
            Destroy(obj);
        }

        // Ladda den nya scenen
        SceneManager.LoadScene(sceneName);
    }
}