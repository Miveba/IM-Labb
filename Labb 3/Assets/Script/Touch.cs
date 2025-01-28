using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Touch : MonoBehaviour
{
    public float force = 100f;
    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetMouseButtonDown(0)) // Vänster musknapp
            {
                // Skapa en ray från muspositionen
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Håll en träffinformation
                RaycastHit hit;

                // Utför raycasten
                if (Physics.Raycast(ray, out hit))
                {
                    // Logga träffobjektet
                    Debug.Log("Hit object: " + hit.collider.name);

                    // Exempel: Rita ett streck från rayens start till träffpunkten
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);

                  Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                    if(rb != null )
                    {
                        rb.AddForce(ray.direction * force); 
                    }
                }
            }
     
    }
        

}
