using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTimer : MonoBehaviour
{
    public float timerDuration = 10f; // Tid i sekunder innan scenbytet
    private float timer;
    private bool timerActive = false;

    void Start()
    {
        timer = timerDuration;
        timerActive = true;
    }

    void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                LoadNextRoom();
            }
        }
    }

    void LoadNextRoom()
    {
        SceneManager.LoadScene("Meny"); // Byt ut "NextScene" mot namnet på din scen
    }
}
