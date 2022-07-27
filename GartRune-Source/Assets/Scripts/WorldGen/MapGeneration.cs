using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public float mapSize = 1; //The number is the amount of chunks on each side of the square map (e.g. 2 would be 2 by 2 chunks)

    public float ChunkSize = 16;

    public GameObject block; //This will later be a custom "Block" data type

    // Start is called before the first frame update
    void Start()
    {
        GenerateChunk();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateChunk() 
    {
        Debug.Log("Generating Chunk");
        for (float x = 1; x < ChunkSize; x++) 
        {
            for (float z = 1; z < ChunkSize; z++) 
            {
                Instantiate(block, new Vector3(x, Mathf.PerlinNoise(x, z), z), block.transform.rotation);
            }
        }
        Debug.Log("Done");
    }
}
