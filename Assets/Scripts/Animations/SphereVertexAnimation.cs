using Helper;
using UnityEngine;

namespace Animations
{
	public class SphereVertexAnimation : MonoBehaviour
	{
		public ComputeShader VertexAnimationCompute;
        public Mesh AnimatedMesh;
		
        public float Frequency = 1.4f;
        public float Amplitude = 0.25f;
        public int SphereResolution;

        ComputeBuffer vertexBuffer;
        ComputeBuffer originalVertexBuffer;
        ComputeBuffer normalBuffer;

        int sphereVertexCount;

        Vector3[] originalVertices;
        Vector3[] normals;

		const string kernelName = "CSSphereVertexAnimation";
		const int maxThreads = 64;

        void Start()
        {
            AnimatedMesh = GetComponent<MeshFilter>().mesh;
            var allVertices = AnimatedMesh.vertices;
            normals = AnimatedMesh.normals;
            originalVertices = (Vector3[])AnimatedMesh.vertices.Clone();

            sphereVertexCount = SphereResolution * SphereResolution;

            vertexBuffer = new(allVertices.Length, sizeof(float) * 3);
            vertexBuffer.SetData(allVertices);

            originalVertexBuffer = new(allVertices.Length, sizeof(float) * 3);
            originalVertexBuffer.SetData(originalVertices);

            normalBuffer = new(normals.Length, sizeof(float) * 3);
            normalBuffer.SetData(normals);
        }

        void Update()
        {
            var kernel = VertexAnimationCompute.FindKernel(kernelName);

			VertexAnimationCompute.SetBuffer(kernel, ShaderPropIDs.VerticesPropID, vertexBuffer);
			VertexAnimationCompute.SetBuffer(kernel, ShaderPropIDs.VertexAnimation.OriginalVerticesPropID, originalVertexBuffer);
			VertexAnimationCompute.SetBuffer(kernel, ShaderPropIDs.VertexAnimation.NormalsPropID, normalBuffer);
			VertexAnimationCompute.SetInt(ShaderPropIDs.VertexAnimation.VertexCountPropID, originalVertices.Length);
			VertexAnimationCompute.SetInt(ShaderPropIDs.VertexAnimation.SphereVertexCountPropID, sphereVertexCount);
			VertexAnimationCompute.SetFloat(ShaderPropIDs.VertexAnimation.TimePropID, Time.time);
			VertexAnimationCompute.SetFloat(ShaderPropIDs.VertexAnimation.FrequencyPropID, Frequency);
			VertexAnimationCompute.SetFloat(ShaderPropIDs.VertexAnimation.AmplitudePropID, Amplitude);

            var threadGroups = Mathf.CeilToInt(sphereVertexCount / (float)maxThreads);
			VertexAnimationCompute.Dispatch(kernel, threadGroups, 1, 1);

            var updatedVertices = new Vector3[originalVertices.Length];
            vertexBuffer.GetData(updatedVertices);
			
            AnimatedMesh.vertices = updatedVertices;
            AnimatedMesh.RecalculateNormals();
            AnimatedMesh.RecalculateBounds();
        }

        void OnDestroy()
        {
            vertexBuffer?.Release();
            originalVertexBuffer?.Release();
            normalBuffer?.Release();
        }
	}
}