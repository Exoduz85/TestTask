using Helper;
using UnityEngine;

namespace Coloring
{
	public class FacingColorUpdater : MonoBehaviour
	{
		public ComputeShader ColorCompute;
		public Transform Target;
		
		MeshFilter meshFilter;
		ComputeBuffer colorBuffer;
		ComputeBuffer vertexBuffer;
		Vector3[] vertices;
		Color[] colors;
		int computeKernel;
		
		const string kernelName = "CSColorByFacing";
		const int maxThreads = 64;

		void Start()
		{
			meshFilter = GetComponent<MeshFilter>();
			var mesh = meshFilter.mesh;

			vertices = mesh.vertices;
			colors = new Color[vertices.Length];

			vertexBuffer = new(vertices.Length, sizeof(float) * 3);
			colorBuffer = new(vertices.Length, sizeof(float) * 4);

			vertexBuffer.SetData(vertices);
			colorBuffer.SetData(colors);

			computeKernel = ColorCompute.FindKernel(kernelName);
			ColorCompute.SetInt(ShaderPropIDs.ColorByFacing.VertexCountPropID, vertices.Length);
			ColorCompute.SetBuffer(computeKernel, ShaderPropIDs.VerticesPropID, vertexBuffer);
			ColorCompute.SetBuffer(computeKernel, ShaderPropIDs.ColorByFacing.ColorsPropID, colorBuffer);
		}

		void Update()
		{
			var toTarget = (Target.position - transform.position).normalized;
			var forward = transform.forward;

			ColorCompute.SetVector(ShaderPropIDs.ColorByFacing.ToTargetPropID, toTarget);
			ColorCompute.SetVector(ShaderPropIDs.ColorByFacing.ObjectForwardPropID, forward);
			
			ColorCompute.Dispatch(computeKernel, Mathf.CeilToInt(vertices.Length / (float)maxThreads), 1, 1);

			colorBuffer.GetData(colors);

			var mesh = meshFilter.mesh;
			mesh.colors = colors;
		}

		void OnDestroy()
		{
			vertexBuffer?.Release();
			colorBuffer?.Release();
		}
	}
}