using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DrivingSurfaceManager : MonoBehaviour
{
    public ARPlaneManager PlaneManager;
    public ARRaycastManager RaycastManager;
    public ARPlane LockedPlane;

    private bool planeLocked = false; // 🔒 Håller koll på om vi redan har låst ett plan

    private void Start()
    {
        PlaneManager = GetComponent<ARPlaneManager>();
        PlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (planeLocked) return; // 🛑 Stoppa om vi redan har låst ett plan

        foreach (var plane in args.added)
        {
            LockPlane(plane);
            planeLocked = true; // 🔒 Markera att vi har låst ett plan
            break; // 🚀 Avbryt loopen så att vi endast låser ett plan
        }
    }

    public void LockPlane(ARPlane keepPlane)
    {
        LockedPlane = keepPlane;

        foreach (var plane in PlaneManager.trackables)
        {
            if (plane != keepPlane)
            {
                plane.gameObject.SetActive(false); // ❌ Inaktivera andra plan
            }
        }

        PlaneManager.planesChanged -= OnPlanesChanged; // ⛔ Sluta lyssna på nya plan
    }

    private void Update()
    {
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }
    }
}
