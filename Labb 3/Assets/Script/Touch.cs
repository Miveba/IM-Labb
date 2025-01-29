using UnityEngine;
using UnityEngine.Audio;

public class Touch : MonoBehaviour
{
    public float force = 100f;
    public AudioClip hitSound; // Assign this in the Inspector
    private AudioSource audioSource;

    public float volume;

    void Start()
    {
        // Add an AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // V�nster musknapp
        {
            // Skapa en ray fr�n muspositionen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // H�ll en tr�ffinformation
            RaycastHit hit;

            // Utf�r raycasten
            if (Physics.Raycast(ray, out hit))
            {
                // Logga tr�ffobjektet
                Debug.Log("Hit object: " + hit.collider.name);

                // Exempel: Rita ett streck fr�n rayens start till tr�ffpunkten
                Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForce(ray.direction * force);

                    if (hitSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(hitSound);
                    }

                }

            }
        }

    }


}
