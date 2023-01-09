using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameControllerScript gc;
    PlayerScript myPlr;

    // Start is called before the first frame update
    void Start()
    {
        myPlr = gameObject.GetComponent<PlayerScript>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Step()
    {
        yield return new WaitForSeconds(1);

        while(gc.actions > 0)
        {
            if (gc.battleIsHappening)
                yield return new WaitForSeconds(8);

            RandomMove();
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(0.5f);

        if(!gc.battleIsHappening)
            gc.StartCoroutine("EndTurn");

        yield return null;
    }

    void Move()
    {
        /*
        List<Vector2> validMoves = new List<Vector2>();
        validMoves = GetValidMoves();
        Vector2 chosenPos;

        if(myPlr.hp < 5 && myPlr.hp < gc.player1.GetComponent<PlayerScript>().hp)
        {
            for (int i = 0; i < validMoves.Count; i++)
            {
                if (myPlr.defAdvantage)
                {
                    if(gc.board[Mathf.RoundToInt(validMoves[i].x), Mathf.RoundToInt(validMoves[i].y)].myObj != null)
                    {
                        if (gc.board[Mathf.RoundToInt(validMoves[i].x), Mathf.RoundToInt(validMoves[i].y)].myObj.name.StartsWith("HP"))
                        {
                            chosenPos = validMoves[i];
                        }
                    }
                }
            }
        }
        */
    }

    void RandomMove()
    {
        List<Vector2> validMoves = new List<Vector2>();
        validMoves = GetValidMoves();

        int r = Random.Range(0, validMoves.Count);

        int x, y;
        x = Mathf.RoundToInt(validMoves[r].x);
        y = Mathf.RoundToInt(validMoves[r].y);

        gc.audios[0].PlayAudio(0);
        transform.position = gc.board[x, y].gameObject.transform.position;
        myPlr.myTile.myObj = null;
        myPlr.myTile = gc.board[x, y];
        gc.CollectPowerUps(myPlr);

        if (gc.LookForBattle())
        {
            gc.StartCoroutine("Battle");
            gc.battleIsHappening = true;
        }

        gc.actions--;
        gc.ui.SetActions(gc.actions);

    }

    List<Vector2> GetValidMoves()
    {
        List<Vector2> valids = new List<Vector2>();

        int x, y;
        x = myPlr.myTile.x;
        y = myPlr.myTile.y;

        if(x > 0)
        {
            if(gc.board[x-1,y].myObj == null)
            {
                valids.Add(new Vector2(x - 1, y));
            }
            else if (!gc.board[x - 1, y].myObj.name.StartsWith("Player"))
            {
                valids.Add(new Vector2(x - 1, y));
            }
        }
        if(x < gc.gi.gridX-1)
        {
            if (gc.board[x + 1, y].myObj == null)
            {
                valids.Add(new Vector2(x + 1, y));
            }
            else if (!gc.board[x + 1, y].myObj.name.StartsWith("Player"))
            {
                valids.Add(new Vector2(x + 1, y));
            }
        }
        if (y > 0)
        {
            if (gc.board[x, y-1].myObj == null)
            {
                valids.Add(new Vector2(x, y-1));
            }
            else if (!gc.board[x, y-1].myObj.name.StartsWith("Player"))
            {
                valids.Add(new Vector2(x, y-1));
            }
        }
        if (y < gc.gi.gridY-1)
        {
            if (gc.board[x, y + 1].myObj == null)
            {
                valids.Add(new Vector2(x, y + 1));
            }
            else if (!gc.board[x, y + 1].myObj.name.StartsWith("Player"))
            {
                valids.Add(new Vector2(x, y + 1));
            }
        }

        return valids;
    }
}
