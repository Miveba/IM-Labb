using UnityEngine;
using UnityEngine.InputSystem; // Krävs för vibrationer på mobil

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float raycastDistance = 50f;

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
        // Kontrollera om raycasten träffar något framför spelaren
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // Om vi träffar något, visa vad vi träffade i debug-loggen
            Debug.Log("Hit something: " + hit.collider.gameObject.name);

            // Om objektet som träffades är ett monster, ge skada
            if (hit.collider.CompareTag("Enemy")) // Se till att ditt monster har taggen "Monster"
            {
                TakeDamage(10); // Ta skada
                Vibrate(); // Vibrera enheten
            }
        }
    }


    private void Vibrate()
    {
#if UNITY_ANDROID || UNITY_IOS
        Debug.Log("Attempting to vibrate");
        Handheld.Vibrate();
#else
    Debug.Log("Vibration not supported on this platform");
#endif
        Debug.Log("Vibrating on impact!");
    }
}