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
                SpawnMonster(lockedPlane);
                spawnedPlanes.Add(lockedPlane);
                monstersSpawned = true; // Se till att vi bara spawnar monster en gÂng
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
                // Justera Random.Range sÅEatt det gÂr utanfˆr planetets storlek
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% stˆrre ‰n planetets storlek
                    0,
                    Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% stˆrre ‰n planetets storlek
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // S‰tt planet som fˆr‰lder
            }
        }

        if (monsterPrefab2 != null)
        {
            for (int i = 0; i < monsterCount; i++)
            {
                // Justera Random.Range sÅEatt det gÂr utanfˆr planetets storlek
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% stˆrre ‰n planetets storlek
                    0,
                    Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% stˆrre ‰n planetets storlek
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // S‰tt planet som fˆr‰lder
            }
        }
    }

}

