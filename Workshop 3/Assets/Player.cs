using UnityEngine;
using UnityEngine.InputSystem; // Krävs för vibrationer på mobil

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float raycastDistance = 2f;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Player is dead!");
            // Lägg till dödslogik här
        }
        Debug.Log("Player HP: " + currentHP);
    }

    public void GainHP(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;

        Debug.Log("Player healed! Current HP: " + currentHP);
    }

    private void Update()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            Debug.Log("Hit something: " + hit.collider.gameObject.name);
            TakeDamage(10);
            Vibrate();
        }
    }

    private void Vibrate()
    {
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
        Debug.Log("Vibrating on impact!");
    }
}