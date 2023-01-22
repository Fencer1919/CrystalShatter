using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Board : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    // One grid, one tile prefab
    [SerializeField] private GameObject tilePrefab;

    // Two dimension array created with [,]
    Tile [,] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing allTiles array and instantiating the tiles
        allTiles = new Tile [width, height];
        SetupTiles();
    }
    
    void SetupTiles()
    {
        // Nested for loops
        // After creating the first column, this for makes it go to second row and instantiate the second column
        for (int i = 0; i < width; i++)
        {
            // This creates, instantiates the tile prefab for the whole column first
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate (tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;

                // Going to be researched for comments
                tile.name = "Tile (" + i + "," + j + ")";

                allTiles[i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform;


            }
        }
    }
}