/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ARPlane CurrentPlane;

    // Start is called before the first frame update
    private void Start()
    {
        Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        // Hitta mitten av skärmen
        var screenCenter = new Vector3(0.5f, 0.5f);
        var screenPosition = Camera.main.ViewportToScreenPoint(screenCenter);

        // Utför raycast för att hitta en yta
        var hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;

        if (hits.Count > 0)
        {
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = lockedPlane == null
                ? hits[0]
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }

        if (hit.HasValue)
        {
            CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
            transform.position = hit.Value.pose.position; // Flytta retikeln till ytan
        }

        // 🟢 Håll Child-objektet alltid framför kameran i rätt riktning
        float distance = 2.0f; // Justera om nödvändigt
        Child.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;

        // 🟢 Se till att retikeln alltid är vänd framåt
        Child.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);

        // Se till att retikeln alltid syns
        Child.SetActive(true);
    }


}
