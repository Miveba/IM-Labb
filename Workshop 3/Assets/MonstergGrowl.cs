using UnityEngine;

public class MonstergGrowl : MonoBehaviour
{

    public AudioClip soundClip3;

    private AudioSource loopSource;  // Extra ljudkälla för loopande ljud
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Skapa en extra AudioSource för loopande ljud
        loopSource = gameObject.AddComponent<AudioSource>();
    }

    public void MonsterMove(float pitch = 1.0f, float volume = 1.0f)
    {
        loopSource.pitch = pitch;
        loopSource.volume = volume;
        loopSource.PlayOneShot(soundClip3);
    }
}
