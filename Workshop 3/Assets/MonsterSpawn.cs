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
                SpawnMonster(lockedPlane);
                spawnedPlanes.Add(lockedPlane);
                monstersSpawned = true; // Se till att vi bara spawnar monster en gång
            }
        }
    }


    private void SpawnMonster(ARPlane plane)
    {
        int monsterCount = 5; // Antal monster att spawna per prefab

        if (monsterPrefab1 != null)
        {
            for (int i = 0; i < monsterCount; i++)
            {
                // Justera Random.Range så att det går utanför planetets storlek
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% större än planetets storlek
                    0,
                    Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% större än planetets storlek
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // Sätt planet som förälder
            }
        }

        if (monsterPrefab2 != null)
        {
            for (int i = 0; i < monsterCount; i++)
            {
                // Justera Random.Range så att det går utanför planetets storlek
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% större än planetets storlek
                    0,
                    Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% större än planetets storlek
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // Sätt planet som förälder
            }
        }
    }

}

