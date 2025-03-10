using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Hitta AudioSource-komponenten på detta GameObject
        audioSource = GetComponent<AudioSource>();

        // Om audioSource inte är null, spela ljudet
        if (audioSource != null)
        {
            audioSource.loop = true; // Se till att loopen är aktiverad
            audioSource.Play(); // Spela upp ljudet
        }
        else
        {
            Debug.LogError("Ingen AudioSource hittades på detta GameObject.");
        }
    }
}