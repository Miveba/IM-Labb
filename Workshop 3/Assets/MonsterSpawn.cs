using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab1; // Monster-prefaben som ska spawna
    public GameObject monsterPrefab2; // Monster-prefaben som ska spawna
    public ARPlaneManager planeManager; // Hanterar AR-plan
    private List<ARPlane> spawnedPlanes = new List<ARPlane>(); // HÂller koll pÅEplan som redan har monster
    private bool monstersSpawned = false; // HÂller koll pÅEom monster redan spawnats

    private float minSpawnDistance = 0.5f; // Minsta avstÂnd mellan monster vid spawn

    private List<Vector3> spawnedMonsterPositions = new List<Vector3>(); // HÂller koll pÅEspawnade monsterpositioner

    private void OnEnable()
    {
        // Lyssna pÅEeventet n‰r nya plan uppt‰cks
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        // Sluta lyssna n‰r scriptet inaktiveras
        planeManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (planeManager == null || planeManager.trackables.count == 0)
            return;

        // H‰mta LockedPlane frÂn DrivingSurfaceManager
        DrivingSurfaceManager drivingSurfaceManager = FindObjectOfType<DrivingSurfaceManager>();
        if (drivingSurfaceManager != null && drivingSurfaceManager.LockedPlane != null)
        {
            ARPlane lockedPlane = drivingSurfaceManager.LockedPlane;

            if (!spawnedPlanes.Contains(lockedPlane))
            {
                SpawnMonsters(lockedPlane);
                spawnedPlanes.Add(lockedPlane);
                monstersSpawned = true; // Se till att vi bara spawnar monster en gÂng
            }
        }
    }

    private void SpawnMonsters(ARPlane plane)
    {
        int monsterCount1 = 2; // Antal monster att spawna fˆr monsterPrefab1
        int monsterCount2 = 2; // Antal monster att spawna fˆr monsterPrefab2

        // Spawna monsterPrefab1
        if (monsterPrefab1 != null)
        {
            for (int i = 0; i < monsterCount1; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition(plane);
                if (spawnPosition != Vector3.zero) // Om en giltig spawnposition hittas
                {
                    GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
                    monster.transform.SetParent(plane.transform); // S‰tt planet som fˆr‰lder
                    spawnedMonsterPositions.Add(spawnPosition); // L‰gg till positionen i listan
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
                    monster.transform.SetParent(plane.transform); // S‰tt planet som fˆr‰lder
                    spawnedMonsterPositions.Add(spawnPosition); // L‰gg till positionen i listan
                }
            }
        }
    }

    private Vector3 GetValidSpawnPosition(ARPlane plane)
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int attempts = 0; attempts < 100; attempts++) // Begr‰nsa antal fˆrsˆk
        {
            // Justera Random.Range sÅEatt det gÂr utanfˆr planetets storlek
            Vector3 randomOffset = new Vector3(
                Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% stˆrre ‰n planetets storlek
                0,
                Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% stˆrre ‰n planetets storlek
            );

            spawnPosition = plane.transform.position + randomOffset;

            // Kontrollera om positionen ‰r giltig
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
        // Kontrollera om positionen ‰r tillr‰ckligt lÂngt frÂn andra spawnade monster
        foreach (Vector3 otherPosition in spawnedMonsterPositions)
        {
            if (Vector3.Distance(position, otherPosition) < minSpawnDistance)
            {
                return false; // Positionen ‰r fˆr n‰ra ett annat monster
            }
        }

        // Kontrollera om positionen krockar med andra objekt i v‰rlden (som monster)
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f); // Justera radien beroende pÅEmonsterstorlek
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // Om en annan monster finns h‰r
            {
                return false; // Positionen ‰r upptagen
            }
        }

        return true; // Positionen ‰r giltig 
    }
}
