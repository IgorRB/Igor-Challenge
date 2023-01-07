using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreatorScript : MonoBehaviour
{
    public SOgameInfo gi;
    public GameControllerScript gc;

    int boardX, boardY;

    public Material[] matTiles;
    public GameObject tile;

    public GameObject[,] board;

    // Start is called before the first frame update
    void Start()
    {
        boardX = gi.boardX;
        boardY = gi.boardY;

        board = new GameObject[boardX, boardY];

        for (int x = 0; x < boardX; x++)
        {
            for (int z = 0; z < boardY; z++)
            {
                GameObject t = Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity);
                t.transform.parent = gameObject.transform;
                t.GetComponent<Renderer>().material = matTiles[(x + z) % 2];
                t.GetComponent<TileScript>().x = x;
                t.GetComponent<TileScript>().y = z;
                board[x, z] = t;
            }
        }

        gc.board = board;
        gc.StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
