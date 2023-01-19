using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private GameObject tilePrefab;

    Tile[,] m_allTiles;
    
    // Start is called before the first frame update
    void Start()
    {
        m_allTiles = new Tile[width, height];
        SetupTiles();
    }
    
    void SetupTiles()
    {
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++) 
            {
                GameObject tile = Instantiate (tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;




            }
        }
    }
}
