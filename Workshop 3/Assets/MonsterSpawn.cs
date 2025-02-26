using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Monster-prefaben som ska spawna
    public ARPlaneManager planeManager; // Hanterar AR-plan

    private List<ARPlane> spawnedPlanes = new List<ARPlane>(); // Håller koll på plan som redan har monster

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
            }
        }
    }



    private void SpawnMonster(ARPlane plane)
    {
        if (monsterPrefab != null)
        {
            Vector3 spawnPosition = plane.transform.position; // Placera monstret på planet
            GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            monster.transform.SetParent(plane.transform); // Sätt planet som förälder så att det följer med
        }
    }
}

