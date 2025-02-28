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

        // Skala upp det låsta planet
        ExpandLockedPlane(10f);
    }

    private void Update()
    {
        // Om planet är subsumed by (om det förlorar sitt ursprungliga plan), uppdatera till det nya planet
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }

    }

    public void ExpandLockedPlane(float newSize = 10f)
    {
        if (LockedPlane == null) return;

        // Försök att ändra storleken på planet genom att justera dess MeshRenderer
        var planeMesh = LockedPlane.GetComponent<MeshRenderer>();
        if (planeMesh != null)
        {
            planeMesh.transform.localScale = new Vector3(newSize, 1, newSize); // Skala upp
        }
    }
}
