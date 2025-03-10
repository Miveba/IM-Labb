using UnityEngine;
using UnityEngine.InputSystem; // Krävs för vibrationer på mobil
using UnityEngine.UI; // För att hantera UI-element
using UnityEngine.SceneManagement;
using TMPro; // För att byta scen

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    private float raycastDistance = 5f;
    public Slider healthBar; // UI Slider för HP

    public int score = 0; // Håller koll på poängen
    public TMP_Text scoreText; // UI-text för att visa poäng
    private AudioManager audioManager;

    private void Start()
    {
        currentHP = maxHP;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = currentHP;
        }

        UpdateScoreText(); // Uppdatera poängvisningen vid start
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Player is dead!");
            // Lägg till dödslogik här
            SceneManager.LoadScene("Game Over");
        }
        UpdateHealthBar();
        Debug.Log("Player HP: " + currentHP);
    }

    public void GainHP(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;

        UpdateHealthBar();
        Debug.Log("Player healed! Current HP: " + currentHP);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHP;
        }
    }

    private void Update()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        audioManager = FindObjectOfType<AudioManager>();
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            Debug.Log("Hit something: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                TakeDamage(10);
                Vibrate();
                audioManager.DamageSound(1, 1);

            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score >= 6)
        {
            Debug.Log("Level avklarad!");
            SceneManager.LoadScene("Victory"); // Byt ut med namnet på din scen
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Antal dödade monster: " + score + "/ 6";
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
