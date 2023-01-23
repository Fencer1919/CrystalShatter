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

    [SerializeField] private int borderSize;

    // One grid, one tile prefab
    public GameObject tilePrefab;
    // Crystals, game pieces
    public GameObject[] gamePiecePrefabs;

    // Two dimension array created with [,] for all tiles
    Tile[,] allTiles;
    // Two dimension array for all crystals, all game pieces
    GamePiece[,] allGamePieces;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing allTiles array and instantiating the tiles
        allTiles = new Tile [width, height];
        allGamePieces = new GamePiece[width, height];

        SetupTiles();
        SetupCamera();
        FillRandom();
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

                // Naming the objects when we instantiate it
                tile.name = "Tile (" + i + "," + j + ")";
                // Storing the tiles in the two dimensional array
                allTiles[i, j] = tile.GetComponent<Tile>();
                // Parenting tiles, grids to the board
                tile.transform.parent = transform;
                // We get the actual tile component, we call init and pass the x value, y value and the reference to the board
                allTiles[i, j].Init(i, j, this);
            }
        }
    }

    void SetupCamera()
    {
        // We have to (width - 1 / 2) and (height - 1 / 2) to properly adjust the camera. -10f is default Z
        Camera.main.transform.position = new Vector3((float)(width - 1) / 2f, (float)(height - 1) / 2f, -10f);
        // Aspect Ratio (Screen Width / Screen Height)
        float aspectRatio = (float) Screen.width / (float) Screen.height;

        float verticalSize = ((float)height / 2f + (float)borderSize);

        float horizontalSize = ((float)width / 2f + (float)borderSize) / aspectRatio;

        if ( verticalSize > horizontalSize)
        {
            Camera.main.orthographicSize = verticalSize;
        }
        else
        {
            Camera.main.orthographicSize = horizontalSize;
        }
        
    }

    GameObject GetRandomGamePiece()
    {
        int randomIndex = Random.Range(0, gamePiecePrefabs.Length);

        //if (gamePiecePrefabs[randomIndex] == null)
        //{
        //    Debug.Warning("BOARD: " + randomIndex + "does not contain a valid GamePiece Prefab!");
        //}

        return gamePiecePrefabs[randomIndex];
    }

    void PlaceGamePiece(GamePiece gamePiece, int x, int y)
    {
        //if (gamePiece == null)
        //{
        //    Debug.Warning("BOARD: Invalid GamePiece!");
        //    return;
        //}

        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.transform.rotation = Quaternion.identity;
        gamePiece.SetCoordinate(x, y);
    }

    void FillRandom()
    {
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++) 
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity) as GameObject;

                if (randomPiece != null) 
                {
                    PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);
                }
            }
        }
    }
}