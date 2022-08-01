using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public float mapSize = 10; //The number is the amount of chunks on each side of the square map (e.g. 2 would be 2 by 2 chunks)

    public float ChunkSize = 16;

    public GameObject block; //This will later be a custom "Block" data type

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld(mapSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateWorld(float worldSize) 
    {
        for (float x = 1f; x < worldSize; x++) 
        {
            for (float z = 1f; z < worldSize; z++) 
            {
                GenerateChunk(x, z, ChunkSize);
            }
        }
    }

    void GenerateChunk(float chunkX, float chunkZ, float chunkSize) 
    {
        Debug.Log("Generating Chunk");
        for (float x = 1f; x <= chunkSize; x++) 
        {
            for (float z = 1f; z <= chunkSize; z++) 
            {
                Instantiate(block, new Vector3(x + (chunkX * ChunkSize), Mathf.PerlinNoise((x + (chunkX * ChunkSize)) / ChunkSize, (z + (chunkZ * ChunkSize)) / ChunkSize) * ChunkSize, z + (chunkZ * ChunkSize)), block.transform.rotation);
            }
        }
        Debug.Log("Done");
    }
}
