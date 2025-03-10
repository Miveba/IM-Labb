using UnityEngine;

public class GroundMonsterAI : MonsterAI
{
    private MonstergGrowl m_AudioManager;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<MonstergGrowl>();
        m_AudioManager.MonsterMove(1, 1);
    }
    protected override void Move(Vector3 direction)
    {
        // Låt monstret röra sig, men behåll Y-positionen oförändrad
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        newPosition.y = transform.position.y; // Lås Y-axeln så att monstret inte svävar

        transform.position = newPosition;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
