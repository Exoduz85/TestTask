using Animations;
using Spawning;
using UnityEngine;

namespace Controllers
{
	public class SimpleMouseAttractor : MonoBehaviour
	{
		public GameObject ObjectA;
		public GameObject ObjectB;

		public float attractorSpeed = 8.0f;
		public float distanceFromCamera = 20.0f;

		Camera mainCamera;
		LissajousAnimation objectAnimationA;
		LissajousAnimation objectAnimationB;

		void Start()
		{
			mainCamera = Camera.main;
			var objectSpawner = GetComponent<ObjectSpawner>();
			
			ObjectA = objectSpawner.ObjectA;
			ObjectB = objectSpawner.ObjectB;
			
			objectAnimationA = ObjectA.GetComponent<LissajousAnimation>();
			objectAnimationB = ObjectB.GetComponent<LissajousAnimation>();
		}

		void Update()
		{
			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				objectAnimationA.Pause = false;
				objectAnimationB.Pause = false;
			}

			if (Input.GetMouseButton(1))
			{
				objectAnimationA.Pause = true;
				MoveObjectTowardsMouse(ObjectA);
			}

			if (Input.GetMouseButton(0))
			{
				objectAnimationB.Pause = true;
				MoveObjectTowardsMouse(ObjectB);
			}
		}

		void MoveObjectTowardsMouse(GameObject targetObject)
		{
			var mouseScreenPosition = Input.mousePosition;
			mouseScreenPosition.z = distanceFromCamera;
			
			var targetPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
			targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPosition, Time.deltaTime * attractorSpeed);
		}
	}
}