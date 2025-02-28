using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ImageRecognitionBehaviour : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;
    public GameObject prefab; // Prefaben som ska placeras

    private Dictionary<ARTrackedImage, GameObject> spawnedPrefabs = new Dictionary<ARTrackedImage, GameObject>();

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Lägg till nya objekt när en bild trackas
        foreach (var trackedImage in args.added)
        {
            var newPrefab = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedPrefabs[trackedImage] = newPrefab;
        }

        // Uppdatera positionen och rotationen för trackade bilder
        foreach (var trackedImage in args.updated)
        {
            if (spawnedPrefabs.TryGetValue(trackedImage, out GameObject spawnedPrefab))
            {
                spawnedPrefab.transform.position = trackedImage.transform.position;
                spawnedPrefab.transform.rotation = trackedImage.transform.rotation;
            }
        }

        // Ta bort objekt om bilden inte längre trackas
        foreach (var trackedImage in args.removed)
        {
            if (spawnedPrefabs.TryGetValue(trackedImage, out GameObject spawnedPrefab))
            {
                Destroy(spawnedPrefab);
                spawnedPrefabs.Remove(trackedImage);
            }
        }
    }
}
