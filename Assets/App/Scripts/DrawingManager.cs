using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Draft360
{

#if UNITY_EDITOR
	// Set up touch input propagation while using Instant Preview in the editor.
	using Input = GoogleARCore.InstantPreviewInput;
#endif

	public class DrawingManager : MonoBehaviour
    {
		public static DrawingManager Instance;

		[SerializeField] GameObject curvedPointPrefab;
		[SerializeField] GameObject curvedLineContainerPrefab;
		[SerializeField] GameObject camera;

		[Space(7)]
		[SerializeField] float drawingDistance;
		[SerializeField] Material drawingMaterial;
		
		private List<GameObject> points = new List<GameObject>();
		private bool touchedDown;
		private Transform lineParent;

		private bool canDraw;

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
			canDraw = false;
		}

		void Update()
		{
			if (!canDraw)
				return;

			if (Input.GetMouseButton(0) || Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					int id = touch.fingerId;
					if (EventSystem.current.IsPointerOverGameObject(id))
					{
						// ui touched
						return;
					}
				}

				if (!touchedDown)
				{
					GameObject container = Instantiate(curvedLineContainerPrefab, FrameManager.Instance.GetCurrentFrame().transform);
					lineParent = container.transform;
					lineParent.GetComponent<CurvedLineRenderer>().SetMaterial(drawingMaterial);
				}

				touchedDown = true;

//				Ray ray = new Ray();

//#if UNITY_EDITOR

//				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//#endif

				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				var rayEnd = ray.GetPoint(drawingDistance);

				//Debug.Log("Touched");

				//Vector3 camPos = camera.transform.position;

				Vector3 camDirection = camera.transform.forward;
				Quaternion camRotation = camera.transform.rotation;
				float spawnDistance = 0;

				//Debug.Log("Touched" + rayEnd.x + " " + rayEnd.y + " " + rayEnd.z);

				Vector3 spawnPos = rayEnd + (camDirection * spawnDistance);
				GameObject cur = Instantiate(curvedPointPrefab, spawnPos, camRotation);
				//cur.transform.SetParent(FrameManager.Instance.GetCurrentFrame().transform);
				cur.transform.SetParent(lineParent);


				// Original Code
				//Debug.Log("Touched");				
				//Vector3 camPos = camera.transform.position;				
				//Vector3 camDirection = camera.transform.forward;
				//Quaternion camRotation = camera.transform.rotation;
				//float spawnDistance = 2;				
				//Debug.Log("Touched" + camPos.x + " " + camPos.y + " " + camPos.z);				
				//Vector3 spawnPos = camPos + (camDirection * spawnDistance);
				//GameObject cur = Instantiate(gameObject, spawnPos, camRotation);
				//cur.transform.SetParent(this.transform);
			}
			else
			{
				if (touchedDown)
					touchedDown = false;
			}
			
		}

		public void ToggleDrawing(bool _toggle)
		{
			canDraw = _toggle;
		}

		public void ChangeColor(Color _colorToChange)
		{
			Material mat = new Material(Shader.Find("Unlit/Color"));
			mat.color = _colorToChange;

			drawingMaterial = mat;

			//drawingMaterial.color = _colorToChange;
		}
	}
}