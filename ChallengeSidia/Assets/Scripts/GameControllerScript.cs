using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public SOgameInfo gi;
    public BoardCreatorScript bc;

    public GameObject playerPrefab;
    public GameObject player1, player2;

    public GameObject[,] board;

    public int turn = 1;
    public int actions = 3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(turn%2 == 1)
        {
            HighlightTiles(player1);
        }
    }

    public IEnumerator Spawn()
    {
        player1 = Instantiate(playerPrefab, board[1, 1].transform.position, Quaternion.identity);
        player1.GetComponent<PlayerScript>().myTile = board[1, 1].GetComponent<TileScript>();
        player1.GetComponent<PlayerScript>().myTile.myObj = player1;
        yield return null;
    }

    void HighlightTiles(GameObject plr)
    {
        int x, y;
        x = plr.GetComponent<PlayerScript>().myTile.x;
        y = plr.GetComponent<PlayerScript>().myTile.y;

        if (x > 0)
        {
            if (board[x - 1, y].GetComponent<TileScript>().myObj == null)
            {
                board[x - 1, y].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x - 1, y].GetComponent<TileScript>().movable = true;
            }
            else if (!board[x - 1, y].GetComponent<TileScript>().myObj.name.StartsWith("Player"))
            {
                board[x - 1, y].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x - 1, y].GetComponent<TileScript>().movable = true;
            }
        }
        if (x < gi.boardX)
        {
            if (board[x + 1, y].GetComponent<TileScript>().myObj == null)
            {
                board[x + 1, y].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x + 1, y].GetComponent<TileScript>().movable = true;
            }                
            else if (!board[x + 1, y].GetComponent<TileScript>().myObj.name.StartsWith("Player"))
            {
                board[x + 1, y].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x + 1, y].GetComponent<TileScript>().movable = true;
            }                
        }
        if (y > 0)
        {
            if (board[x, y-1].GetComponent<TileScript>().myObj == null)
            {
                board[x, y - 1].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x, y - 1].GetComponent<TileScript>().movable = true;
            }                
            else if (!board[x, y - 1].GetComponent<TileScript>().myObj.name.StartsWith("Player"))
            {
                board[x, y - 1].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x, y - 1].GetComponent<TileScript>().movable = true;
            }                
        }
        if (y < gi.boardY)
        {
            if (board[x, y+1].GetComponent<TileScript>().myObj == null)
            {
                board[x, y + 1].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x, y + 1].GetComponent<TileScript>().movable = true;
            }                
            else if (!board[x, y + 1].GetComponent<TileScript>().myObj.name.StartsWith("Player"))
            {
                board[x, y + 1].GetComponent<Renderer>().material = bc.matTiles[2];
                board[x, y + 1].GetComponent<TileScript>().movable = true;
            }                
        }
    }

    public void TryMove(int x, int y)
    {
        if(board[x,y].GetComponent<TileScript>().movable)
        {
            if (turn % 2 == 1)
            {
                UnHighlightTiles(player1);
                player1.transform.position = board[x, y].transform.position;
                player1.GetComponent<PlayerScript>().myTile.myObj = null;
                player1.GetComponent<PlayerScript>().myTile = board[x,y].GetComponent<TileScript>();
            }
        }
        
    }

    void UnHighlightTiles(GameObject plr)
    {
        int x, y;
        x = plr.GetComponent<PlayerScript>().myTile.x;
        y = plr.GetComponent<PlayerScript>().myTile.y;

        if (x > 0)
        {            
            board[x - 1, y].GetComponent<Renderer>().material = bc.matTiles[(x + y + 1) % 2];
            board[x - 1, y].GetComponent<TileScript>().movable = false;
        }
        if (x < gi.boardX)
        {
            board[x + 1, y].GetComponent<Renderer>().material = bc.matTiles[(x + y + 1) % 2];
            board[x + 1, y].GetComponent<TileScript>().movable = false;
        }
        if (y > 0)
        {            
            board[x, y - 1].GetComponent<Renderer>().material = bc.matTiles[(x + y + 1) % 2];
            board[x, y - 1].GetComponent<TileScript>().movable = false;
        }
        if (y < gi.boardY)
        {            
            board[x, y + 1].GetComponent<Renderer>().material = bc.matTiles[(x + y + 1) % 2];
            board[x, y + 1].GetComponent<TileScript>().movable = false;
        }
    }

}
