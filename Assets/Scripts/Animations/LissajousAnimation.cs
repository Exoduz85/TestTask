using UnityEngine;

namespace Animations
{
	public class LissajousAnimation : MonoBehaviour
	{
		public Vector3 amplitude = new(1f, 1f, 1f);
		public Vector3 frequency = new(1f, 2f, 3f);
		public Vector2 phase = new(0f, Mathf.PI / 2f);
		public float speed = 1f;
		public Vector3 center = Vector3.zero;
		public bool Randomize;

		float t;
		
		void Start()
		{
			if (Randomize)
			{
				frequency = new(Random.Range(1.5f, 3.5f), Random.Range(1.5f, 3.5f), Random.Range(1.5f, 3.5f));
				phase = new(Random.Range(0.1f, Mathf.PI * 1.3f), Random.Range(0.1f, Mathf.PI * 1.3f));
				amplitude = new(Random.Range(1.5f, 3.5f), Random.Range(1.5f, 3.5f), Random.Range(1.5f, 3.5f));
			}
		}

		void Update()
		{
			t += Time.deltaTime * speed;

			var x = amplitude.x * Mathf.Sin(frequency.x * t + phase.x);
			var y = amplitude.y * Mathf.Sin(frequency.y * t);
			var z = amplitude.z * Mathf.Sin(frequency.z * t + phase.y);

			transform.position = center + new Vector3(x, y, z);
		}
	}
}