using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25; // Amount of damage the bullet causes

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet on impact
        Destroy(gameObject);
    }
}