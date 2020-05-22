using GoogleARCore.Examples.ObjectManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft360
{
    public class CustomManipulation : MonoBehaviour
    {
        private static CustomManipulation s_Instance = null;

        private DragGestureRecognizer m_DragGestureRecognizer = new DragGestureRecognizer();

        private PinchGestureRecognizer m_PinchGestureRecognizer = new PinchGestureRecognizer();

        private TwoFingerDragGestureRecognizer m_TwoFingerDragGestureRecognizer =
            new TwoFingerDragGestureRecognizer();

        private TapGestureRecognizer m_TapGestureRecognizer = new TapGestureRecognizer();

        private TwistGestureRecognizer m_TwistGestureRecognizer = new TwistGestureRecognizer();

        private bool canManipulate;

        /// <summary>
        /// Gets the ManipulationSystem instance.
        /// </summary>
        public static CustomManipulation Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    var manipulationSystems = FindObjectsOfType<CustomManipulation>();
                    if (manipulationSystems.Length > 0)
                    {
                        s_Instance = manipulationSystems[0];
                    }
                    else
                    {
                        Debug.LogError("No instance of ManipulationSystem exists in the scene.");
                    }
                }

                return s_Instance;
            }
        }

        /// <summary>
        /// Gets the Drag gesture recognizer.
        /// </summary>
        public DragGestureRecognizer DragGestureRecognizer
        {
            get
            {
                return m_DragGestureRecognizer;
            }
        }

        /// <summary>
        /// Gets the Pinch gesture recognizer.
        /// </summary>
        public PinchGestureRecognizer PinchGestureRecognizer
        {
            get
            {
                return m_PinchGestureRecognizer;
            }
        }

        /// <summary>
        /// Gets the two finger drag gesture recognizer.
        /// </summary>
        public TwoFingerDragGestureRecognizer TwoFingerDragGestureRecognizer
        {
            get
            {
                return m_TwoFingerDragGestureRecognizer;
            }
        }

        /// <summary>
        /// Gets the Tap gesture recognizer.
        /// </summary>
        public TapGestureRecognizer TapGestureRecognizer
        {
            get
            {
                return m_TapGestureRecognizer;
            }
        }

        /// <summary>
        /// Gets the Twist gesture recognizer.
        /// </summary>
        public TwistGestureRecognizer TwistGestureRecognizer
        {
            get
            {
                return m_TwistGestureRecognizer;
            }
        }

        /// <summary>
        /// Gets the current selected object.
        /// </summary>
        public GameObject SelectedObject { get; private set; }

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            if (Instance != this)
            {
                Debug.LogWarning("Multiple instances of ManipulationSystem detected in the scene." +
                                 " Only one instance can exist at a time. The duplicate instances" +
                                 " will be destroyed.");
                DestroyImmediate(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            canManipulate = false;
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {
            if (canManipulate)
            {
                DragGestureRecognizer.Update();
                PinchGestureRecognizer.Update();
                TwoFingerDragGestureRecognizer.Update();
                TapGestureRecognizer.Update();
                TwistGestureRecognizer.Update();
            }        
        }

        public void ToggleManipulation()
        {
            canManipulate = !canManipulate;
        }


        /// <summary>
        /// Deselects the selected object if any.
        /// </summary>
        internal void Deselect()
        {
            SelectedObject = null;
        }

        /// <summary>
        /// Select an object.
        /// </summary>
        /// <param name="target">The object to select.</param>
        internal void Select(GameObject target)
        {
            if (SelectedObject == target)
            {
                return;
            }

            Deselect();
            SelectedObject = target;
        }
    }
}

