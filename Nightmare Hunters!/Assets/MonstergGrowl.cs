using UnityEngine;

public class MonstergGrowl : MonoBehaviour
{
    public AudioClip soundClip3;
    private AudioSource loopSource;

    private float lastGrowlTime = 0f; // Senaste gången ljudet spelades
    private float growlCooldown = 2f; // Cooldown i sekunder (justera vid behov)

    void Start()
    {
        loopSource = gameObject.AddComponent<AudioSource>();
    }

    public void MonsterMove(float pitch = 1.0f, float volume = 1.0f)
    {
        // Kolla om tillräckligt med tid har gått
        if (Time.time - lastGrowlTime >= growlCooldown)
        {
            loopSource.pitch = pitch;
            loopSource.volume = volume;
            loopSource.PlayOneShot(soundClip3);

            lastGrowlTime = Time.time; // Uppdatera senaste ljudspelning
        }
    }
}
