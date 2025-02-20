using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp = 100; // Health points of the monster
    public float speed = 3.0f; // Speed at which the monster moves

    private Renderer monsterRenderer; // To control the color of the monster
    private Color originalColor;

    void Start()
    {
        // Store the renderer and the original color of the monster
        monsterRenderer = GetComponent<Renderer>();
        if (monsterRenderer != null)
        {
            originalColor = monsterRenderer.material.color;
        }
    }

    void Update()
    {
        // Call movement in every frame to move the monster
        Movement();
    }

    // Method to move the monster towards the AR Camera
    private void Movement()
    {
        // Find the AR Camera in the scene
        GameObject arCamera = Camera.main.gameObject;

        if (arCamera != null)
        {
            // Move the monster towards the AR Camera
            Vector3 direction = (arCamera.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Method to handle damage
    public void TakeDamage(int damage)
    {
        hp -= damage;

        // If the monster has a renderer, flash red to show damage
        if (monsterRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        // If HP is 0 or below, call Death method
        if (hp <= 0)
        {
            Death();
        }
    }

    // Coroutine to flash red when taking damage
    private System.Collections.IEnumerator FlashRed()
    {
        monsterRenderer.material.color = Color.red; // Change color to red
        yield return new WaitForSeconds(0.2f); // Wait for a short time
        monsterRenderer.material.color = originalColor; // Revert to original color
    }

    // Method to handle the monster's death
    private void Death()
    {
        // Destroy this monster game object
        Destroy(gameObject);
    }

    // This method should be triggered by bullets or other sources of damage
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object is tagged as "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Get damage value from bullet's script (example below)
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage); // Apply damage
            }
        }
    }
}