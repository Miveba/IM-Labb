using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;

public class ImageRecognitionBehaviour : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
        }
         // Uppdatera positionen för alla spårade bilder
            foreach (var trackedImage in args.updated)
            {
                // Hämta det objekt som redan är spawnat i ARTrackedImageManager
                GameObject trackedObject = trackedImage.gameObject;

                // Uppdatera position och rotation
                trackedObject.transform.position = trackedImage.transform.position;
                trackedObject.transform.rotation = trackedImage.transform.rotation;
            }
        
    }

}
