using UnityEngine;

namespace Helper
{
	public static class ShaderPropIDs
	{
		public static readonly int VerticesPropID = Shader.PropertyToID("vertices");
		public static readonly int TrianglesPropID = Shader.PropertyToID("triangles");

		public static class MeshGeneration
		{
			public static readonly int SphereResolutionPropID = Shader.PropertyToID("sphereResolution");
			public static readonly int SphereVertOffsetPropID = Shader.PropertyToID("sphereVertexOffset");
			public static readonly int SphereTriOffsetPropID = Shader.PropertyToID("sphereTriangleOffset");
			public static readonly int SphereRadiusPropID = Shader.PropertyToID("sphereRadius");
			
			public static readonly int ConeResolutionPropID = Shader.PropertyToID("coneResolution");
			public static readonly int ConeVertOffsetPropID = Shader.PropertyToID("coneVertexOffset");
			public static readonly int ConeTriOffsetPropID = Shader.PropertyToID("coneTriangleOffset");
			public static readonly int ConeBaseRadiusPropID = Shader.PropertyToID("coneBaseRadius");
			public static readonly int ConeLengthPropID = Shader.PropertyToID("coneLength");
		}

		public static class ColorByFacing
		{
			public static readonly int VertexCountPropID = Shader.PropertyToID("vertexCount");
			public static readonly int ColorsPropID = Shader.PropertyToID("colors");
			public static readonly int ToTargetPropID = Shader.PropertyToID("toTarget");
			public static readonly int ObjectForwardPropID = Shader.PropertyToID("objectForward");
		}

		public static class VertexAnimation
		{
			public static readonly int OriginalVerticesPropID = Shader.PropertyToID("originalVertices");
			public static readonly int NormalsPropID = Shader.PropertyToID("normals");
			public static readonly int VertexCountPropID = Shader.PropertyToID("vertexCount");
			public static readonly int SphereVertexOffsetPropID = Shader.PropertyToID("sphereVertexOffset");
			public static readonly int SphereVertexCountPropID = Shader.PropertyToID("sphereVertexCount");
			public static readonly int TimePropID = Shader.PropertyToID("time");
			public static readonly int FrequencyPropID = Shader.PropertyToID("frequency");
			public static readonly int AmplitudePropID = Shader.PropertyToID("amplitude");
		}
	}
}