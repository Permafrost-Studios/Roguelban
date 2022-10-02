using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public float mapSize = 10; //The number is the amount of chunks on each side of the square map (e.g. 2 would be 2 by 2 chunks)

    public float ChunkSize = 16;

    public GameObject block; //This will later be a custom "Block" data type

    private float currentSeed;

    public Vector3[] WorldVertices; //This is all temporary
    private int WorldVerticeID = 0;
    public int[] WorldTriangles;
    private int WorldTriangleID = 0;

    public Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        WorldVertices = new Vector3[(int)((ChunkSize + 1) * (ChunkSize + 1) * (mapSize * mapSize))];
        WorldTriangles = new int[WorldVertices.Length * 6];
        currentSeed = GenerateSeed(0, 1000000);
        GenerateWorld(mapSize);

        Debug.Log(WorldVertices);
        mesh.vertices = WorldVertices;
        mesh.triangles = WorldTriangles;
    }

    void GenerateWorld(float worldSize) 
    {
        for (float x = 0f; x < worldSize; x++) 
        {
            for (float z = 0f; z < worldSize; z++) 
            {
                GenerateChunk(x, z, ChunkSize);
            }
        }
    }

    void GenerateChunk(float chunkX, float chunkZ, float chunkSize) 
    {
        Mesh mesh = new Mesh();
        Vector3[] Vertices = new Vector3[(int)((chunkSize + 1) * (chunkSize + 1))];
        int[] Triangles = new int[Vertices.Length * 6];

        int trianglesID = 0;
        int verticeID = 0;
        Debug.Log("Generating Chunk");
        for (float x = 0f; x < chunkSize; x++) 
        {
            for (float z = 0f; z < chunkSize; z++) 
            {
                // Instantiate(block, new Vector3(x + (chunkX * ChunkSize), Mathf.PerlinNoise((x + (chunkX * ChunkSize) + currentSeed) / ChunkSize, (z + (chunkZ * ChunkSize) + currentSeed) / ChunkSize) * ChunkSize, z + (chunkZ * ChunkSize)), block.transform.rotation);
                Vertices[verticeID] = new Vector3(x + (chunkX * ChunkSize), Mathf.PerlinNoise((x + (chunkX * ChunkSize) + currentSeed) / ChunkSize, (z + (chunkZ * ChunkSize) + currentSeed) / ChunkSize) * ChunkSize, z + (chunkZ * ChunkSize));
                WorldVertices[WorldVerticeID] = Vertices[verticeID];
                verticeID++;
                WorldVerticeID++;

                //Creates the triangles of connecting the points of one vertex square
                // Triangles[trianglesID] = (int)(z + (chunkZ * ChunkSize));
                // Triangles[trianglesID + 1] = (int)(z + (chunkZ * ChunkSize) + 1);
                // Triangles[trianglesID + 2] = (int)(z + (chunkZ * ChunkSize) + chunkSize + 1);
                // Triangles[trianglesID + 3] = (int)(z + (chunkZ * ChunkSize) + chunkSize + 1);
                // Triangles[trianglesID + 4] = (int)(z + (chunkZ * ChunkSize) + chunkSize);
                // Triangles[trianglesID + 5] = (int)(z + (chunkZ * ChunkSize));

                WorldTriangles[WorldTriangleID] = (int)(WorldVerticeID);
                WorldTriangles[WorldTriangleID + 1] = (int)(WorldVerticeID + chunkSize);
                WorldTriangles[WorldTriangleID + 2] = (int)(WorldVerticeID + chunkSize + 1);
                WorldTriangles[WorldTriangleID + 3] = (int)(WorldVerticeID + chunkSize + 1);
                WorldTriangles[WorldTriangleID + 4] = (int)(WorldVerticeID + 1);
                WorldTriangles[WorldTriangleID + 5] = (int)(WorldVerticeID);
                trianglesID += 6;
                WorldTriangleID += 6;
            }
        }
        Debug.Log("Done");
    }

    float GenerateSeed(float min, float max) 
    {
        return Random.Range(min, max);
        //Allow for more specific fine tuning if desired
    }

    void OnDrawGizmos() {
        if (WorldVertices != null) 
        {
            for (int i = 0; i < WorldVertices.Length; i++) 
            {
                Gizmos.DrawSphere(WorldVertices[i], .1f);
            }
        } else 
        {
            Debug.Log("No vertices...");
        }
    }
}
