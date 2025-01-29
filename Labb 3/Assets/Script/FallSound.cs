using UnityEngine;
using UnityEngine.Audio;

public class FallSound : MonoBehaviour
{
    private AudioSource audioSource;
    public float volume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.Play();
            Debug.Log("Lådan träffade marken!");

        }
    }
}
