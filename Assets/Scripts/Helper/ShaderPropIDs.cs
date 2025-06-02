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

	}
}