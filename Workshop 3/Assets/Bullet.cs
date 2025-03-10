using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;    // Hastigheten som kulan ska röra sig med
    private float lifeTime = 5f;  // Hur länge kulan ska existera innan den förstörs
    private Player player;       // Player-referens

    void Start()
    {
        // Hämta Player-skriptet från Main Camera
        player = Camera.main.GetComponent<Player>();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Applicera en kraft framåt i den riktning objektet är roterat (transform.forward)
            rb.linearVelocity = transform.forward * speed;
        }

        Destroy(gameObject, lifeTime); // Förstör kulan efter en viss tid
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy1") || other.CompareTag("Enemy2")) // Om vi träffar ett fiendeobjekt
        {    
            Destroy(other.gameObject); // Förstör fienden
            Destroy(gameObject);       // Förstör kulan
        }
        else if (other.CompareTag("HP")) // Om vi träffar ett HP-objekt
        {
            Destroy(other.gameObject);  // Förstör HP-objektet
            Destroy(gameObject);        // Förstör kulan
            if (player != null)
            {
                player.GainHP(40); // Ge spelaren 40 HP om referensen är korrekt
            }
            else
            {
                Debug.LogWarning("Player script not found on Main Camera!");
            }
        }
    }
}
