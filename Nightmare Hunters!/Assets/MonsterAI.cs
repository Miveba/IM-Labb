using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    protected float speed = 0.5f; // Hastighet på monstret
    protected Transform player;   // Spelaren (AR-kameran)
    private AudioManager manager;

    private float lastGrowlTime = 0f; // Tidpunkt för senaste ljudet
    private float growlCooldown = 2f; // Cooldown för monsterljud

    protected virtual void Start()
    {
        player = Camera.main.transform;
        manager = FindObjectOfType<AudioManager>();
    }

    protected virtual void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        Move(direction);
    }

    protected virtual void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        // 🔥 Spela ljud bara om cooldownen är över
        if (Time.time - lastGrowlTime >= growlCooldown)
        {
            lastGrowlTime = Time.time; // Uppdatera senaste ljudtiden
            manager.Monster(0.5f, 0.5f);
        }
    }
}
