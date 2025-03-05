using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float speed = 2.0f; // Hastighet på monstret

    private Transform player; // Spelaren (AR-kameran)

    private void Start()
    {
        // Hämta kamerans transform (spelaren)
        player = Camera.main.transform;
    }

    private void Update()
    {
        if (player == null) return;

        // Räkna ut riktningen mot spelaren
        Vector3 direction = (player.position - transform.position).normalized;

        // Röra sig mot spelaren
        transform.position += direction * speed * Time.deltaTime;

        // Rotera mot spelaren
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
