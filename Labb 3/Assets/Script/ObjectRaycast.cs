using System;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastForward : MonoBehaviour
{
    public Transform startObject;
    public float rayDistance = 5f;

    void Update()
    {
        Vector3 direction = startObject.forward;
        Ray ray = new Ray(startObject.position, direction);

        Debug.DrawLine(startObject.position, startObject.position + direction * rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Raycast träffade" + hit.collider.gameObject.name);
        }
    }
}
