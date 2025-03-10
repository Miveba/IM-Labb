using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;    // Hastigheten som kulan ska röra sig med
    public float lifeTime = 0.01f;  // Hur länge kulan ska existera innan den förstörs

    void Start()
    {
        // Förstör kulan efter livslängden
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Flytta kulan framåt varje frame i den riktning den är roterad
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
