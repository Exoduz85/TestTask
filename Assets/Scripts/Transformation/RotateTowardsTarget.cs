using UnityEngine;

namespace Transformation
{
	public class RotateTowardsTarget : MonoBehaviour
	{
		public Transform target;
		public float angularSpeed = 33f;

		void Update()
		{
			if (target == null)
				return;

			var direction = (target.position - transform.position).normalized;

			if (direction.sqrMagnitude < 0.0001f)
				return;

			var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
		}
	}
}