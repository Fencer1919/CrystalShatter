using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    Board _board;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This Init function and the Init on the Board.cs script code makes the tiles show the x and y coordinates
    // We can see the x and y on Unity editor and change them once we press play and press to any tile
    public void Init(int x, int y, Board board)
    {
        xIndex = x;
        yIndex = y;
        _board = board;
    }
}
