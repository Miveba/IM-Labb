using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Referens till AudioSource
    private AudioSource audioSource;
    private AudioSource bg;

    // De tre ljudklippen
    public AudioClip soundClip1;
    public AudioClip soundClip2;
    public AudioClip soundClip3;
    public AudioClip soundClip4;
    public AudioClip soundClip5;
    public AudioClip backgroundMusic;

    // Startmetod där vi hämtar AudioSource komponenten
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bg = GetComponent<AudioSource>();
        bg.clip = backgroundMusic;
        bg.loop = true;  // Sätt musikens loop till true
        bg.Play();  // Börja spela musiken
    }

    // Metod för att spela upp ljud 1
    public void BulletSound(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip1); // Spela upp ljudklippet
    }

    // Metod för att spela upp ljud 2
    public void DamageSound(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip2); // Spela upp ljudklippet
    }

    // Metod för att spela upp ljud 3
    public void MonsterMove(float pitch = 1.0f, float volume = 1.0f)
    {
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.loop = true;  // Gör att ljudklippet loopar
        audioSource.PlayOneShot(soundClip3); // Spela upp ljudklippet
    }

    public void Monster(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip4); // Spela upp ljudklippet
    }

    public void HealthSound(float pitch = 1.0f, float volume = 1.0f)
    {
        // Sätt pitch och volume innan vi spelar upp ljudet
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.PlayOneShot(soundClip4); // Spela upp ljudklippet
    }

}