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
        int monsterCount = 5; // Antal monster att spawna för varje prefab
        if (monsterPrefab1 != null)
        {

            // Spawna monsterPrefab1
            for (int i = 0; i < monsterCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x / 2, plane.size.x / 2),
                    0,
                    Random.Range(-plane.size.y / 2, plane.size.y / 2)
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // Sätt planet som förälder
            }
        }

        if (monsterPrefab2 != null)
        {
            // Spawna monsterPrefab2
            for (int i = 0; i < monsterCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x / 2, plane.size.x / 2),
                    0,
                    Random.Range(-plane.size.y / 2, plane.size.y / 2)
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // Sätt planet som förälder
            }
        }
    }



}

