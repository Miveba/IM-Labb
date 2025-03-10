using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;   // Kulans prefab
    public Transform firePoint;       // Var kulorna skjuts ifrån
    public float fireRate = 3f;     // Hur snabbt vi skjuter

    void Start()
    {
        InvokeRepeating(nameof(Shoot), 0f, fireRate); // Börja skjuta direkt
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
