using Meshes;
using UnityEngine;

namespace Spawning
{
	public class ObjectSpawner : MonoBehaviour
	{
		public ComputeShader MeshCompute;
		public GameObject ObjectA;
		public GameObject ObjectB;
		public int SphereResolution = 64;
		public int ConeResolution = 32;
		public float SphereRadius = 1f;
		public float ConeBaseRadius = 0.5f;
		public float ConeLength = 0.5f;

		void Awake()
		{
			ObjectA = CreateObject("Object A", new Vector3(-5, 0, 0), Color.red, MeshType.SphereWithNose);
			
			ObjectA.transform.rotation = Quaternion.Euler(0, 90, 0);
			
			ObjectB = CreateObject("Object B", new Vector3(5, 0, 0), Color.green, MeshType.Sphere);
		}

		GameObject CreateObject(string objectName, Vector3 startingPosition, Color objectColor, MeshType meshType)
		{
			var obj = new GameObject(objectName)
			{
				transform = { position = startingPosition }
			};
			
			var meshFilter = obj.AddComponent<MeshFilter>();
			var meshRenderer = obj.AddComponent<MeshRenderer>();

			meshFilter.mesh = meshType switch
			{
				MeshType.Sphere => MeshGenerator.CreateSphere(MeshCompute, SphereRadius, SphereResolution),
				MeshType.SphereWithNose => MeshGenerator.CreateSphereWithNose(MeshCompute, SphereRadius, ConeBaseRadius, ConeLength, SphereResolution, ConeResolution),
				_ => null
			};

			meshRenderer.material = new (Shader.Find("Unlit/Color"))
			{
				color = objectColor
			};

			return obj;
		}
	}
}