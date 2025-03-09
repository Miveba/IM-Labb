using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float speed = 0.1f; // Hastighet på monstret

    protected Transform player; // Spelaren (AR-kameran)


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
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
