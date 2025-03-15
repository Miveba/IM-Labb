using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Health : MonoBehaviour
{
    public GameObject hpPickup; 
    public ARPlaneManager planeManager; 
    private List<ARPlane> spawnedPlanes = new List<ARPlane>(); 
    private bool hpSpawned = false; 


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
                SpawnHP(lockedPlane);
                spawnedPlanes.Add(lockedPlane);
                hpSpawned = true; 
            }
        }
    }


    private void SpawnHP(ARPlane plane)
    {
        int hpSpawn = 2;

        if (hpPickup != null)
        {
            for (int i = 0; i < hpSpawn; i++)
            {
                // Justera Random.Range sÅEatt det gÂr utanfˆr planetets storlek
                Vector3 randomOffset = new Vector3(
                    Random.Range(-plane.size.x * 2f, plane.size.x * 2f),  // 50% stˆrre ‰n planetets storlek
                    0,
                    Random.Range(-plane.size.y * 2f, plane.size.y * 2f)   // 50% stˆrre ‰n planetets storlek
                );

                Vector3 spawnPosition = plane.transform.position + randomOffset;
                GameObject monster = Instantiate(hpPickup, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform); // S‰tt planet som fˆr‰lder
            }
        }
    }


}

