using UnityEngine.XR.ARFoundation;
using UnityEngine;

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
        if (planeLocked || LockedPlane != null) return; // Om ett plan redan är låst, gör inget

        // Vi letar efter det första planet som läggs till
        foreach (var plane in args.added)
        {
            LockPlane(plane); // Lås det första planet som upptäcks
            planeLocked = true; // Markera att vi har låst ett plan
            break; // Avbryt loopen för att låsa bara ett plan
        }
    }

    public void LockPlane(ARPlane keepPlane)
    {
        if (LockedPlane != null) return; // Om det redan finns ett låst plan, gör inget

        LockedPlane = keepPlane;

        // Inaktivera andra plan så att vi bara har ett aktivt plan
        foreach (var plane in PlaneManager.trackables)
        {
            if (plane != keepPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }

        // Sluta lyssna på nya planer efter att ha låst ett plan
        PlaneManager.planesChanged -= OnPlanesChanged;
        PlaneManager.planesChanged += DisabledNewPlanes;

        // Skala upp det låsta planet
        ExpandLockedPlane(10f);
        // Hämta AR-kamerans position
        Transform cameraTransform = Camera.main.transform;

        // Flytta planet under kameran (ändra Y-värdet)
        Vector3 newPosition = LockedPlane.transform.position;

        // Sätt planet att vara en viss höjd under kameran (justera 10.0f för att få rätt avstånd)
        newPosition.y = cameraTransform.position.y - 10.0f;
        newPosition.z = cameraTransform.position.z + 15.0f; // Håll planet 10 meter under kameran

        // Uppdatera planet till den nya positionen
        LockedPlane.transform.position = newPosition;
    }

    private void DisabledNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach(var LockedPlane in args.added)
        {
            LockedPlane.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        // Om planet är subsumed by (om det förlorar sitt ursprungliga plan), uppdatera till det nya planet
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }

        if (LockedPlane != null)
        {
            // Hämta AR-kamerans position
            Transform cameraTransform = Camera.main.transform;

            // Flytta planet under kameran (ändra Y-värdet)
            Vector3 newPosition = LockedPlane.transform.position;
            newPosition.y = cameraTransform.position.y - 10.0f; // Håll planet 10 meter under kameran
            newPosition.z = cameraTransform.position.z + 15.0f; // Håll planet 10 meter under kameran

            // Uppdatera planet till den nya positionen
            LockedPlane.transform.position = newPosition;
        }
    }

    public void ExpandLockedPlane(float scaleFactor = 10f)
{
    if (LockedPlane == null) return;

    // Skala upp planet visuellt
    LockedPlane.transform.localScale = Vector3.one * scaleFactor;
}

}
