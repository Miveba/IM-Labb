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

    public int desiredMonsterCount1 = 3; // Antal monsterPrefab1 som alltid ska finnas
    public int desiredMonsterCount2 = 3; // Antal monsterPrefab2 som alltid ska finnas

    private float minSpawnDistance = 0.5f; // Minsta avstånd mellan monster vid spawn
    private List<Vector3> spawnedMonsterPositions = new List<Vector3>(); // Håller koll på spawnade monsterpositioner

    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (planeManager == null || planeManager.trackables.count == 0)
            return;

        DrivingSurfaceManager drivingSurfaceManager = FindObjectOfType<DrivingSurfaceManager>();
        if (drivingSurfaceManager != null && drivingSurfaceManager.LockedPlane != null)
        {
            ARPlane lockedPlane = drivingSurfaceManager.LockedPlane;
            if (!spawnedPlanes.Contains(lockedPlane))
            {
                SpawnMonsters(lockedPlane);
                spawnedPlanes.Add(lockedPlane);
            }
        }
    }

    private void SpawnMonsters(ARPlane plane)
    {
        EnsureMonsterCount(plane);
    }

    private void EnsureMonsterCount(ARPlane plane)
    {
        int currentMonsterCount1 = GameObject.FindGameObjectsWithTag("Enemy1").Length;
        int currentMonsterCount2 = GameObject.FindGameObjectsWithTag("Enemy2").Length;

        while (currentMonsterCount1 < desiredMonsterCount1)
        {
            Vector3 spawnPosition = GetValidSpawnPosition(plane);
            GameObject monster = Instantiate(monsterPrefab1, spawnPosition, Quaternion.identity);
            monster.tag = "Enemy1";
            spawnedMonsterPositions.Add(spawnPosition);
            currentMonsterCount1++;
        }

        while (currentMonsterCount2 < desiredMonsterCount2)
        {
            Vector3 spawnPosition = GetValidSpawnPosition(plane);
            GameObject monster = Instantiate(monsterPrefab2, spawnPosition, Quaternion.identity);
            monster.tag = "Enemy2";
            spawnedMonsterPositions.Add(spawnPosition);
            currentMonsterCount2++;
        }
    }

    private Vector3 GetValidSpawnPosition(ARPlane plane)
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int attempts = 0; attempts < 100; attempts++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-plane.size.x * 2f, plane.size.x * 2f),
                0,
                Random.Range(-plane.size.y * 2f, plane.size.y * 2f)
            );

            spawnPosition = plane.transform.position + randomOffset;

            if (IsPositionValid(spawnPosition))
            {
                validPositionFound = true;
                break;
            }
        }

        return validPositionFound ? spawnPosition : Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 otherPosition in spawnedMonsterPositions)
        {
            if (Vector3.Distance(position, otherPosition) < minSpawnDistance)
            {
                return false;
            }
        }

        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy1") || collider.CompareTag("Enemy2"))
            {
                return false;
            }
        }

        return true;
    }

    private void Update()
    {
        if (planeManager != null && planeManager.trackables.count > 0)
        {
            DrivingSurfaceManager drivingSurfaceManager = FindObjectOfType<DrivingSurfaceManager>();
            if (drivingSurfaceManager != null && drivingSurfaceManager.LockedPlane != null)
            {
                EnsureMonsterCount(drivingSurfaceManager.LockedPlane);
            }
        }
    }
}
