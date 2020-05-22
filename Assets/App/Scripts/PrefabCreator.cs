//-----------------------------------------------------------------------
// <copyright file="PawnManipulator.cs" company="Google">
//
// Copyright 2019 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace Draft360
{
    using GoogleARCore;
    using GoogleARCore.Examples.ObjectManipulation;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Controls the placement of objects via a tap gesture.
    /// </summary>
    public class PrefabCreator : Manipulator
    {

        public static PrefabCreator Instance;

        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject ARPrefab;

        /// <summary>
        /// Manipulator prefab to attach placed objects to.
        /// </summary>
        public GameObject ManipulatorPrefab;

        private int prefabCounter;

        private bool canPlaceObjects;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            canPlaceObjects = true;
        }

        /// <summary>
        /// Returns true if the manipulation can be started for the given gesture.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        /// <returns>True if the manipulation can be started.</returns>
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (gesture.TargetObject == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Function called when the manipulation is ended.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.WasCancelled)
            {
                return;
            }

            // If gesture is targeting an existing object we are done.
            if (gesture.TargetObject != null)
            {
                return;
            }

            if (!canPlaceObjects)
                return;

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds;

            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    // ui touched
                    return;
                }
            }

            if (Frame.Raycast(gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    // Instantiate game object at the hit pose.
                    var gameObject = Instantiate(ARPrefab, hit.Pose.position, hit.Pose.rotation);
                    
                    if (!gameObject.activeInHierarchy)
                        gameObject.SetActive(true);

                    // Increase prefab counter
                    //prefabCounter++;                  

                    // Instantiate manipulator.
                    var manipulator =
                        Instantiate(ManipulatorPrefab, hit.Pose.position, hit.Pose.rotation);

                    // Make game object a child of the manipulator.
                    gameObject.transform.parent = manipulator.transform;

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of
                    // the physical world evolves.
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // Make manipulator a child of the anchor.
                    manipulator.transform.parent = anchor.transform;

                    // Add prefab to current frame
                    FrameManager.Instance.AddToCurrentFrame(anchor.gameObject);

                    // Select the placed object.
                    manipulator.GetComponent<Manipulator>().Select();
                }
            }
        }

        public void SetARPrefab(GameObject prefab)
        {
            ARPrefab = prefab;
        }

        public void TogglePlaceObject()
        {
            canPlaceObjects = !canPlaceObjects;
        }
    }
}
