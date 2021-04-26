using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ChunkGenerator : MonoBehaviour
{
    // private NoiseGeneration nsTerrain;
    public GameObject floor;
    public GameObject parentOfAllTheChunks;
    public GameObject algaePrefab;

    public float terrainAmpl;
    public float algaeAmpl;
    public float terrainScale;
    public float algaeScale;
    public float algaeThreshold;

    private int n;
    private float step;

    void Start()
    {
        // nsTerrain = GetComponent<NoiseGeneration>();
        n = (int) Math.Sqrt(289);
        step = 10f / (n - 1);
    }

    public GameObject GenerateChunk(float x, float z)
    {
        GameObject chunkParent = new GameObject("Chunk(" + x + "," + z + ")");
        chunkParent.transform.parent = parentOfAllTheChunks.transform;
        chunkParent.transform.position = Vector3.forward * (x * 10) + Vector3.right * (z * 10);
        GameObject algaeParent = new GameObject("Algae Parent");
        algaeParent.transform.parent = chunkParent.transform;

        GenerateFloor(x, z, chunkParent, algaeParent);

        return chunkParent;
    }

    private void GenerateFloor(float x, float z, GameObject chunkParent, GameObject algaeParent)
    {
        GameObject theFloorHere = Instantiate(floor, Vector3.forward * (x * 10) + Vector3.right * (z * 10),
            Quaternion.Euler(0, 0, 0), chunkParent.transform);
        theFloorHere.transform.localScale = new Vector3(1, 1, 1);
        Mesh floorMesh = theFloorHere.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = floorMesh.vertices;
        Matrix4x4 localToWorld = theFloorHere.transform.localToWorldMatrix;

        for (int i = 0; i < n * n; i++)
        {
            Vector3 world_v = localToWorld.MultiplyPoint3x4(vertices[i]);
            float cx = world_v.x;
            float cz = world_v.z;
            // float val = nsTerrain.GetStupidity(cx, cy);
            float cy = Mathf.PerlinNoise(cx * terrainScale, cz * terrainScale) * terrainAmpl;
            // Debug.Log("Stupidity at " + cx + " " + cy + " is " + val);
            //float val = (cx + cz) / 10;
            vertices[i].y = cy;
            
            if (i % 50 == 0) GenerateAlgae(cx, cy, cz, algaeParent);
        }

        floorMesh.vertices = vertices;
    }

    private void GenerateAlgae(float x, float y, float z, GameObject algaeParent)
    {
        float val = Mathf.PerlinNoise(x * algaeScale, z * algaeScale) * algaeAmpl;
        val = (float) Math.Pow(val, .2);
        if (val >= algaeThreshold + Random.value * .05)
        {
            float cy = Mathf.PerlinNoise(x * algaeScale, z * algaeScale) * algaeAmpl;
            GameObject algae = Instantiate(algaePrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0),
                algaeParent.transform);
        }
    }
}
