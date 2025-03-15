using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Hitta AudioSource-komponenten pÅEdetta GameObject
        audioSource = GetComponent<AudioSource>();

        // Om audioSource inte ‰r null, spela ljudet
        if (audioSource != null)
        {
            audioSource.loop = true; // Se till att loopen ‰r aktiverad
            audioSource.Play(); // Spela upp ljudet
        }
        else
        {
            Debug.LogError("Ingen AudioSource hittades pÅEdetta GameObject.");
        }
    }
}