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

        if (Input.GetMouseButtonDown(0)) // Vänster musknapp
        {
            // Skapa en ray från muspositionen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Håll en träffinformation
            RaycastHit hit;

            // Utför raycasten
            if (Physics.Raycast(ray, out hit))
            {
                // Logga träffobjektet
                Debug.Log("Hit object: " + hit.collider.name);

                // Exempel: Rita ett streck från rayens start till träffpunkten
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
