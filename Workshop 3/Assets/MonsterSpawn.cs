using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab1; // Monster-prefaben som ska spawna
    public GameObject monsterPrefab2; // Monster-prefaben som ska spawna
    public ARPlaneManager planeManager; // Hanterar AR-plan
    private List<ARPlane> spawnedPlanes = new List<ARPlane>(); // Håller koll på plan som redan har monster
    private bool monstersSpawned = false; // Håller koll på om monster redan spawnats

    public float minSpawnDistance = 1.0f; // Minsta avstånd mellan monster vid spawn

    private List<Vector3> spawnedMonsterPositions = new List<Vector3>(); // Håller koll på spawnade monsterpositioner

    private void OnEnable()
    {
        // Lyssna på eventet när nya plan upptäcks
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        // Sluta lyssna när scriptet inaktiveras
        planeManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (planeManager == null || planeManager.trackables.count == 0)
            return;

        // Hämta LockedPlane från DrivingSurfaceManager
        DrivingSurfaceManager drivingSurfaceManager = FindObjectOfType<DrivingSurfaceManager>();
        if (drivingSurfaceManager != null && drivingSurfaceManager.LockedPlane != null)
        {
            ARPlane lockedPlane = drivingSurfaceManager.LockedPlane;

            if (!spawnedPlanes.Contains(lockedPlane))
            {
                SpawnMonsters(lockedPlane);
                spawnedPlanes.Add(lockedPlane);
                monstersSpawned = true; // Se till att vi bara spawnar monster en gång
            }
        }
    }

    private void SpawnMonsters(ARPlane plane)
    {
        int monsterCount1 = 1; // Antal monster att spawna för monsterPrefab1
        int monsterCount2 = 2; // Antal monster att spawna för monsterPrefab2

        // Spawna monsterPrefab1
        if (monsterPrefab1 != null)
        {
            for (int i = 0; i < monsterCount1; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition(plane);
                if (spawnPosition != Vector3.zero) // Om en giltig spawnposition hittas
                {
                    GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
                    monster.transform.SetParent(plane.transform); // Sätt planet som förälder
                    spawnedMonsterPositions.Add(spawnPosition); // Lägg till positionen i listan
                }
            }
        }

        // Spawna monsterPrefab2
        if (monsterPrefab2 != null)
        {
            for (int i = 0; i < monsterCount2; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition(plane);
                if (spawnPosition != Vector3.zero) // Om en giltig spawnposition hittas
                {
                    GameObject monster = Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
                    monster.transform.SetParent(plane.transform); // Sätt planet som förälder
                    spawnedMonsterPositions.Add(spawnPosition); // Lägg till positionen i listan
                }
            }
        }
    }

    private Vector3 GetValidSpawnPosition(ARPlane plane)
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int attempts = 0; attempts < 100; attempts++) // Begränsa antal försök
        {
            // Justera Random.Range så att det går utanför planetets storlek
            Vector3 randomOffset = new Vector3(
                Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% större än planetets storlek
                0,
                Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% större än planetets storlek
            );

            spawnPosition = plane.transform.position + randomOffset;

            // Kontrollera om positionen är giltig
            if (IsPositionValid(spawnPosition))
            {
                validPositionFound = true;
                break;
            }
        }

        return validPositionFound ? spawnPosition : Vector3.zero; // Retur om giltig position hittas, annars Vector3.zero
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Kontrollera om positionen är tillräckligt långt från andra spawnade monster
        foreach (Vector3 otherPosition in spawnedMonsterPositions)
        {
            if (Vector3.Distance(position, otherPosition) < minSpawnDistance)
            {
                return false; // Positionen är för nära ett annat monster
            }
        }

        // Kontrollera om positionen krockar med andra objekt i världen (som monster)
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f); // Justera radien beroende på monsterstorlek
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // Om en annan monster finns här
            {
                return false; // Positionen är upptagen
            }
        }

        return true; // Positionen är giltig 
    }
}
