using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;    // Hastigheten som kulan ska röra sig med
    private float lifeTime = 5f;  // Hur länge kulan ska existera innan den förstörs

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Applicera en kraft framåt i den riktning objektet är roterat (transform.forward)
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }

        Destroy(gameObject, lifeTime); // Förstör kulan efter en viss tid
    }
}
