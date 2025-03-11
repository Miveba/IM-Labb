using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    protected float speed = 2f; // Hastighet på monstret

    protected Transform player; // Spelaren (AR-kameran)

    private AudioManager manager;
    protected virtual void Start()
    {
        // Hämta kamerans transform (spelaren)
        player = Camera.main.transform;
    }

    protected virtual void Update()
    {
        if (player == null) return;

        // Räkna ut riktningen mot spelaren
        Vector3 direction = (player.position - transform.position).normalized;

        // Röra sig mot spelaren
        Move(direction);
    }

    protected virtual void Move(Vector3 direction)
    {
        manager = FindObjectOfType<AudioManager>();
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        manager.Monster(2f, 2f);
    }
}
