using Animations;
using Coloring;
using Meshes;
using Transformation;
using UnityEngine;

namespace Spawning
{
	public class ObjectSpawner : MonoBehaviour
	{
		public ComputeShader MeshCompute;
		public ComputeShader ColorCompute;
		public ComputeShader VertexAnimationCompute;
		public GameObject ObjectA;
		public GameObject ObjectB;
		public int SphereResolution = 64;
		public int ConeResolution = 32;
		public float SphereRadius = 1f;
		public float ConeBaseRadius = 0.5f;
		public float ConeLength = 0.5f;

		const string colorShaderName = "Unlit/Color";
		const string vertexColorShaderName = "Unlit/VertexColor";
		
		void Awake()
		{
			ObjectA = CreateObject("Object A", new(-10, 0, 0), Color.red, MeshType.SphereWithNose, vertexColorShaderName);
			ObjectB = CreateObject("Object B", new(10, 0, 0), Color.green, MeshType.Sphere, colorShaderName);
			
			AddLissajousAnimation(ObjectA);
			AddLissajousAnimation(ObjectB, true);
			
			AddRotateTowardsTarget(ObjectA, ObjectB);
			
			AddFacingColorUpdater(ObjectA, ObjectB);
			
			AddSphereVertexAnimation(ObjectA);
		}

		GameObject CreateObject(string objectName, Vector3 startingPosition, Color objectColor, MeshType meshType, string shaderName)
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

			meshRenderer.material = new (Shader.Find(shaderName))
			{
				color = objectColor
			};

			return obj;
		}

		void AddLissajousAnimation(GameObject obj, bool randomize = false)
		{
			var lissajousAnimA = obj.AddComponent<LissajousAnimation>();
			lissajousAnimA.center = obj.transform.position;
			lissajousAnimA.Randomize = randomize;
		}
		
		void AddRotateTowardsTarget(GameObject rotateObject, GameObject target)
		{
			var rotateTowards = rotateObject.AddComponent<RotateTowardsTarget>();
			rotateTowards.target = target.transform;
		}
		
		void AddFacingColorUpdater(GameObject toBeColored, GameObject facingTarget)
		{
			var colorUpdater = toBeColored.AddComponent<FacingColorUpdater>();
			colorUpdater.Target = facingTarget.transform;
			colorUpdater.ColorCompute = ColorCompute;
		}

		void AddSphereVertexAnimation(GameObject animObject)
		{
			var vertexAnimation = animObject.AddComponent<SphereVertexAnimation>();
			vertexAnimation.VertexAnimationCompute = VertexAnimationCompute;
			vertexAnimation.SphereResolution = SphereResolution;
		}
	}
}