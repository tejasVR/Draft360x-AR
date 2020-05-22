using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft360
{

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = GoogleARCore.InstantPreviewInput;
#endif

    public class DrawingManager : MonoBehaviour
    {
		[SerializeField] GameObject curvedPointPrefab;
		[SerializeField] GameObject curvedLineContainerPrefab;
		[SerializeField] GameObject camera;
		
		
		private List<GameObject> points = new List<GameObject>();
		private bool touchedDown;
		private Transform lineParent;

		private bool canDraw;

		private void Start()
		{
			canDraw = false;
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButton(0) || Input.touchCount > 0)
			{
				if (!touchedDown)
				{
					GameObject container = Instantiate(curvedLineContainerPrefab, FrameManager.Instance.GetCurrentFrame().transform);
					lineParent = container.transform;
				}

				touchedDown = true;

				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				var rayEnd = ray.GetPoint(2F);

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

		public void ToggleDrawing()
		{
			canDraw = !canDraw;
		}
	}
}