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

        // Hämta den nya storleken på planet från dess transform (eftersom vi skalat upp det)
        float planeSizeX = plane.transform.localScale.x * 10; // Multiplicera med 10 för att få rätt skala
        float planeSizeZ = plane.transform.localScale.z * 10;

        if (monsterPrefab1 != null)
        {
            for (int i = 0; i < monsterCount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(plane, planeSizeX, planeSizeZ);
                GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform);
            }
        }

        if (monsterPrefab2 != null)
        {
            for (int i = 0; i < monsterCount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(plane, planeSizeX, planeSizeZ);
                GameObject monster = Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
                monster.transform.SetParent(plane.transform);
            }
        }
    }

    // Ny metod för att få en slumpmässig position över hela det expanderade planet
    private Vector3 GetRandomSpawnPosition(ARPlane plane, float width, float height)
    {
        bool spawnAtEdge = Random.value > 0.7f; // 30% chans att spawna vid kanterna

        float xOffset, zOffset;

        if (spawnAtEdge)
        {
            // Välj antingen en position nära kanterna
            if (Random.value > 0.5f)
            {
                xOffset = (Random.value > 0.5f ? 1 : -1) * (width / 2 * 0.9f); // 90% av maxbredden
                zOffset = Random.Range(-height / 2, height / 2);
            }
            else
            {
                xOffset = Random.Range(-width / 2, width / 2);
                zOffset = (Random.value > 0.5f ? 1 : -1) * (height / 2 * 0.9f);
            }
        }
        else
        {
            // Slumpa en position över hela planet
            xOffset = Random.Range(-width / 2, width / 2);
            zOffset = Random.Range(-height / 2, height / 2);
        }

        Vector3 randomOffset = new Vector3(xOffset, 0, zOffset);
        return plane.transform.position + randomOffset;
    }

}

