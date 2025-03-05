using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Referens till AudioSource
    private AudioSource audioSource;

    // De tre ljudklippen
    public AudioClip soundClip1;
    public AudioClip soundClip2;
    public AudioClip soundClip3;

    // Startmetod där vi hämtar AudioSource komponenten
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Metod för att spela upp ljud 1
    public void PlaySound1(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip1); // Spela upp ljudklippet
    }

    // Metod för att spela upp ljud 2
    public void PlaySound2(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip2); // Spela upp ljudklippet
    }

    // Metod för att spela upp ljud 3
    public void PlaySound3(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip3); // Spela upp ljudklippet
    }
}