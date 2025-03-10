using UnityEngine;
using UnityEngine.InputSystem; // Krävs för vibrationer på mobil
using UnityEngine.UI; // För att hantera UI-element
using UnityEngine.SceneManagement; // För att byta scen

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    public float raycastDistance = 50f;
    public Slider healthBar; // UI Slider för HP

    public int score = 0; // Håller koll på poängen
    public Text scoreText; // UI-text för att visa poäng

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
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            Debug.Log("Hit something: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Enemy1") || hit.collider.CompareTag("Enemy2"))
            {
                TakeDamage(10);
                Vibrate();
                IncreaseScore(10); // Ökar poängen när en fiende träffas
            }
        }
    }

    private void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score >= 30)
        {
            Debug.Log("Level avklarad!");
            SceneManager.LoadScene("LevelCompleteScene"); // Byt ut med namnet på din scen
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Poäng: " + score;
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
