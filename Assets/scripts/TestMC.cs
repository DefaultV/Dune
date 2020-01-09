using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMC : MonoBehaviour {
    private void Start() {
        // Create Vector3 vertices
        Vector3[] verts = new Vector3[] {
            new Vector3(0,0,0), // 0
            new Vector3(1,0,0), // 1 XYZ
            new Vector3(0,0,1), // 2
            new Vector3(1,0,1), // 3
        };

        int[] tris = new int[] {
            1, //BL
            2, //TL
            0, //BR
            3,
            2,
            1,
        };


        Mesh mesh = new Mesh();

        mesh.vertices = verts;
        mesh.triangles = tris;

        // Set up game object with mesh;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
    }
}