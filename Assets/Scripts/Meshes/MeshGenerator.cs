using Helper;
using UnityEngine;

namespace Meshes
{
	public static class MeshGenerator
	{
		const string kernelSphereName = "CSCreateSphere";
		const string kernelConeName = "CSCreateCone";
		const int maxSphereThreadGroupCount = 8;
		const int maxConeThreadGroupCount = 64;

		public static Mesh CreateSphere(ComputeShader meshCompute, float sphereRadius, int resolution)
		{
			var vertCount = resolution * resolution;
			var triCount = (resolution - 1) * (resolution - 1) * 6;

			var vertexBuffer = new ComputeBuffer(vertCount, sizeof(float) * 3);
			var triangleBuffer = new ComputeBuffer(triCount, sizeof(int));

			ComputeSphere(ref meshCompute, ref vertexBuffer, ref triangleBuffer, resolution, sphereRadius);
			
			return GenerateMeshFromData(ref vertexBuffer, ref triangleBuffer, vertCount, triCount);
		}

		public static Mesh CreateSphereWithNose(ComputeShader meshCompute, float sphereRadius, float coneBaseRadius, float coneLength, int sphereResolution, int coneResolution)
		{
			var sphereVertexCount = sphereResolution * sphereResolution;
	        var sphereTriCount = (sphereResolution - 1) * (sphereResolution - 1) * 6;

			var coneVertexCount = coneResolution + 1;
	        var coneTriCount = coneResolution * 3;

	        var totalVertexCount = sphereVertexCount + coneVertexCount;
	        var totalTriCount = sphereTriCount + coneTriCount;

	        var vertexBuffer = new ComputeBuffer(totalVertexCount, sizeof(float) * 3);
	        var triangleBuffer = new ComputeBuffer(totalTriCount, sizeof(int));

			ComputeSphere(ref meshCompute, ref vertexBuffer, ref triangleBuffer, sphereResolution, sphereRadius);
			ComputeCone(ref meshCompute, ref vertexBuffer, ref triangleBuffer, coneResolution, sphereVertexCount, sphereTriCount, coneBaseRadius, coneLength);

			return GenerateMeshFromData(ref vertexBuffer, ref triangleBuffer, totalVertexCount, totalTriCount);
		}
		
		static void ComputeSphere(ref ComputeShader meshCompute, ref ComputeBuffer vertexBuffer, ref ComputeBuffer triangleBuffer, int sphereResolution, float sphereRadius)
		{
			var kernel = meshCompute.FindKernel(kernelSphereName);
			meshCompute.SetInt(ShaderPropIDs.MeshGeneration.SphereResolutionPropID, sphereResolution);
			meshCompute.SetInt(ShaderPropIDs.MeshGeneration.SphereVertOffsetPropID, 0);
			meshCompute.SetInt(ShaderPropIDs.MeshGeneration.SphereTriOffsetPropID, 0);
			meshCompute.SetFloat(ShaderPropIDs.MeshGeneration.SphereRadiusPropID, sphereRadius);
			meshCompute.SetBuffer(kernel, ShaderPropIDs.VerticesPropID, vertexBuffer);
			meshCompute.SetBuffer(kernel, ShaderPropIDs.TrianglesPropID, triangleBuffer);

			var groupCountX = Mathf.CeilToInt((float)sphereResolution / maxSphereThreadGroupCount);
			var groupCountY = Mathf.CeilToInt((float)sphereResolution / maxSphereThreadGroupCount);
			
			meshCompute.Dispatch(kernel, groupCountX, groupCountY, 1);
		}

		static void ComputeCone(ref ComputeShader meshCompute, ref ComputeBuffer vertexBuffer, ref ComputeBuffer triangleBuffer, int coneResolution, int sphereVertexCount, int sphereTriCount, float coneBaseRadius, float coneLength)
		{
			var kernelCone = meshCompute.FindKernel(kernelConeName);
			meshCompute.SetInt(ShaderPropIDs.MeshGeneration.ConeResolutionPropID, coneResolution);
			meshCompute.SetInt(ShaderPropIDs.MeshGeneration.ConeVertOffsetPropID, sphereVertexCount);
			meshCompute.SetInt(ShaderPropIDs.MeshGeneration.ConeTriOffsetPropID, sphereTriCount);
			meshCompute.SetFloat(ShaderPropIDs.MeshGeneration.ConeBaseRadiusPropID, coneBaseRadius);
			meshCompute.SetFloat(ShaderPropIDs.MeshGeneration.ConeLengthPropID, coneLength);
			meshCompute.SetBuffer(kernelCone, ShaderPropIDs.VerticesPropID, vertexBuffer);
			meshCompute.SetBuffer(kernelCone, ShaderPropIDs.TrianglesPropID, triangleBuffer);
			
			var groupCount = Mathf.CeilToInt((float)coneResolution / maxConeThreadGroupCount);
			
			meshCompute.Dispatch(kernelCone, groupCount, 1, 1);
		}

		static Mesh GenerateMeshFromData(ref ComputeBuffer vertexBuffer, ref ComputeBuffer triangleBuffer, int vertCount, int triCount)
		{
			var vertices = new Vector3[vertCount];
			var triangles = new int[triCount];
			vertexBuffer.GetData(vertices);
			triangleBuffer.GetData(triangles);

			vertexBuffer.Release();
			triangleBuffer.Release();

			var mesh = new Mesh
			{
				indexFormat = UnityEngine.Rendering.IndexFormat.UInt32,
				vertices = vertices,
				triangles = triangles
			};

			mesh.RecalculateNormals();

			return mesh;
		}
	}
}