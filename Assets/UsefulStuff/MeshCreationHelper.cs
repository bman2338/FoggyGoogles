using System;
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
public static class MeshCreationHelper
{
	///------------------------------------------------------------
	/// <summary>
	/// Create a new mesh with one of oldMesh's submesh
	/// </summary>
	///--------------~~~~------------------------------------------------
	public static Mesh CreateMesh(Mesh oldMesh, int subIndex)
	{
		Mesh newMesh = new Mesh();
 
		List<int> triangles = new List<int>();
		triangles.AddRange(oldMesh.GetTriangles(subIndex)); // the triangles of the sub mesh
 
		List<Vector3> newVertices = new List<Vector3>();
		List<Vector2> newUvs = new List<Vector2>();
 
		// Mark's method. 
		Dictionary<int, int> oldToNewIndices = new Dictionary<int, int>();
		int newIndex = 0;
 
		// Collect the vertices and uvs
		for (int i = 0; i < oldMesh.vertices.Length; i++)
		{
			if (triangles.Contains(i))
			{
				newVertices.Add(oldMesh.vertices[i]);
				newUvs.Add(oldMesh.uv[i]);
				oldToNewIndices.Add(i, newIndex);
				++newIndex;
			}
		}
 
		int[] newTriangles = new int[triangles.Count];
 
		// Collect the new triangles indecies
		for (int i = 0; i < newTriangles.Length; i++)
		{
			newTriangles[i] = oldToNewIndices[triangles[i]];
		}
		// Assemble the new mesh with the new vertices/uv/triangles.
		newMesh.vertices = newVertices.ToArray();
		newMesh.uv = newUvs.ToArray();
		newMesh.triangles = newTriangles;
 
		// Re-calculate bounds and normals for the renderer.
		newMesh.RecalculateBounds();
		newMesh.RecalculateNormals();
 
		return newMesh;
	}
}
}

