using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [Header("Controller Objs Reference")]
    public SOgameInfo gi;
    public GuiController ui;

    [Header("Board Variables")]
    public GameObject prefTile;
    int gridX, gridY;
    public Material[] matTiles;
    
    public TileScript[,] board;

    [Header("PowerUp Variables")]
    public GameObject[] powerUps;
    int maxPowerUp;
    int powerUpCount;

    [Header("Players")]
    public GameObject prefPlayer;
    public GameObject player1, player2;

    [Header("Game Variables")]
    public int turn = 1;
    public int actions = 3;
    bool battleIsHappening = false;

    [Header("Audio Sources")]
    public AudioEffectsPlayer[] audios;


    // Start is called before the first frame update
    void Start()
    {
        gridX = gi.gridX;
        gridY = gi.gridY;
        maxPowerUp = gridX * gridY - 2;

        board = new TileScript[gridX, gridY];

        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridY; z++)
            {
                GameObject t = Instantiate(prefTile, new Vector3(x, 0, z), Quaternion.identity);
                t.transform.parent = gameObject.transform;
                t.GetComponent<Renderer>().material = matTiles[(x + z) % 2];
                t.GetComponent<TileScript>().x = x;
                t.GetComponent<TileScript>().y = z;
                board[x, z] = t.GetComponent<TileScript>();
            }
        }

        StartCoroutine("SpawnPlayers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnPlayers()
    {
        player1 = Instantiate(prefPlayer, board[1, 1].transform.position, Quaternion.identity);
        player1.GetComponent<PlayerScript>().playerNum = 1;
        player1.GetComponent<PlayerScript>().myTile = board[1, 1];
        player1.GetComponent<PlayerScript>().myTile.myObj = player1;

        Camera.main.GetComponent<CameraFollowScript>().target = player1.transform;

        player2 = Instantiate(prefPlayer, board[gridX - 2, gridY - 2].transform.position, Quaternion.identity);
        player2.GetComponent<PlayerScript>().playerNum = 2;
        player2.GetComponent<PlayerScript>().myTile = board[gridX - 2, gridY - 2];
        player2.GetComponent<PlayerScript>().myTile.myObj = player2;

        StartCoroutine("SpawnPowerUps");

        HighlightTiles(player1);

        yield return null;
    }

    IEnumerator SpawnPowerUps()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                if(board[x,y].myObj == null)
                {
                    int r = Random.Range(0, powerUps.Length);
                    Vector3 pos = board[x, y].transform.position;
                    GameObject p = Instantiate(powerUps[r], pos + new Vector3(0, 0.3f, 0), Quaternion.identity);
                    board[x, y].myObj = p;
                    powerUpCount++;
                }
            }
        }
        yield return null;
    }

    void HighlightTiles(GameObject plr)
    {
        int x, y;
        x = plr.GetComponent<PlayerScript>().myTile.x;
        y = plr.GetComponent<PlayerScript>().myTile.y;

        if (x > 0)
        {
            if (board[x - 1, y].myObj == null)
            {
                board[x - 1, y].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x - 1, y].movable = true;
            }
            else if (!board[x - 1, y].myObj.name.StartsWith("Player"))
            {
                board[x - 1, y].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x - 1, y].movable = true;
            }
        }
        if (x < gridX-1)
        {
            if (board[x + 1, y].myObj == null)
            {
                board[x + 1, y].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x + 1, y].movable = true;
            }                
            else if (!board[x + 1, y].myObj.name.StartsWith("Player"))
            {
                board[x + 1, y].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x + 1, y].movable = true;
            }                
        }
        if (y > 0)
        {
            if (board[x, y-1].myObj == null)
            {
                board[x, y - 1].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x, y - 1].movable = true;
            }                
            else if (!board[x, y - 1].myObj.name.StartsWith("Player"))
            {
                board[x, y - 1].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x, y - 1].movable = true;
            }                
        }
        if (y < gridY-1)
        {
            if (board[x, y+1].myObj == null)
            {
                board[x, y + 1].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x, y + 1].movable = true;
            }                
            else if (!board[x, y + 1].myObj.name.StartsWith("Player"))
            {
                board[x, y + 1].gameObject.GetComponent<Renderer>().material = matTiles[2];
                board[x, y + 1].movable = true;
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
            board[x - 1, y].gameObject.GetComponent<Renderer>().material = matTiles[(x + y + 1) % 2];
            board[x - 1, y].movable = false;
        }
        if (x < gridX - 1)
        {
            board[x + 1, y].gameObject.GetComponent<Renderer>().material = matTiles[(x + y + 1) % 2];
            board[x + 1, y].movable = false;
        }
        if (y > 0)
        {
            board[x, y - 1].gameObject.GetComponent<Renderer>().material = matTiles[(x + y + 1) % 2];
            board[x, y - 1].movable = false;
        }
        if (y < gridY - 1)
        {
            board[x, y + 1].gameObject.GetComponent<Renderer>().material = matTiles[(x + y + 1) % 2];
            board[x, y + 1].movable = false;
        }
    }

    public void TryMove(int x, int y)
    {
        if(board[x,y].movable)
        {
            if (turn % 2 == 1)
            {
                audios[0].PlayAudio(0);

                UnHighlightTiles(player1);
                player1.transform.position = board[x, y].gameObject.transform.position;
                player1.GetComponent<PlayerScript>().myTile.myObj = null;
                player1.GetComponent<PlayerScript>().myTile = board[x,y];
                CollectPowerUps(player1.GetComponent<PlayerScript>());

                if (LookForBattle())
                {
                    StartCoroutine("Battle");
                    battleIsHappening = true;
                }
                    

                actions--;
                ui.SetActions(actions);

                if (actions == 0 && !battleIsHappening)
                {
                    StartCoroutine("EndTurn");
                }
                else if (!battleIsHappening)
                {
                    HighlightTiles(player1);
                }
            }
            else if (turn % 2 == 0)
            {
                audios[0].PlayAudio(0);

                UnHighlightTiles(player2);
                player2.transform.position = board[x, y].gameObject.transform.position;
                player2.GetComponent<PlayerScript>().myTile.myObj = null;
                player2.GetComponent<PlayerScript>().myTile = board[x, y];
                CollectPowerUps(player2.GetComponent<PlayerScript>());

                if (LookForBattle())
                {
                    StartCoroutine("Battle");
                    battleIsHappening = true;
                }

                actions--;
                ui.SetActions(actions);

                if (actions == 0 && !battleIsHappening)
                {
                    StartCoroutine("EndTurn");
                }
                else if(!battleIsHappening)
                {
                    HighlightTiles(player2);
                }
            }
        }
        
    }

    void CollectPowerUps(PlayerScript plr)
    {
        if(plr.myTile.myObj != null)
        {

            switch (plr.myTile.myObj.GetComponent<PowerUpScript>().effect)
            {
                case "hp s":
                    audios[0].PlayAudio(2);
                    plr.hp++;
                    ui.SetHp(plr.playerNum, plr.hp);
                    break;

                case "atk s":
                    audios[0].PlayAudio(1);
                    plr.bonusAtk++;
                    break;

                case "spd s":
                    audios[0].PlayAudio(3);
                    actions++;
                    break;

                case "d8":
                    audios[0].PlayAudio(1);
                    plr.d8s++;
                    if (plr.d8s > 4)
                        plr.d8s = 4;
                    break;

                case "def adv":
                    audios[0].PlayAudio(1);
                    plr.defAdvantage = true;
                    break;

                case "atk adv":
                    audios[0].PlayAudio(1);
                    plr.atkAdvantage = true;
                    break;

                default:
                    break;
            }

            Destroy(plr.myTile.myObj);
            powerUpCount--;

            if (powerUpCount < (maxPowerUp / 10))
                StartCoroutine("SpawnPowerUps");
        }

        plr.myTile.myObj = plr.gameObject;
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1);

        turn++;
        actions = 3;
        ui.SetTurn(2 - (turn % 2));
        ui.SetActions(actions);

        if(turn%2 == 1)
        {
            player2.GetComponent<PlayerScript>().ClearPowerUps();

            Camera.main.GetComponent<CameraFollowScript>().target = player1.transform;
            Camera.main.GetComponent<CameraFollowScript>().auto = true;

            player1.GetComponent<PlayerScript>().attacked = false;
            ui.p1Atk.gameObject.SetActive(true);
            ui.p2Atk.gameObject.SetActive(false);
            HighlightTiles(player1);
        }
        else
        {
            player1.GetComponent<PlayerScript>().ClearPowerUps();

            Camera.main.GetComponent<CameraFollowScript>().target = player2.transform;
            Camera.main.GetComponent<CameraFollowScript>().auto = true;

            player2.GetComponent<PlayerScript>().attacked = false;
            ui.p2Atk.gameObject.SetActive(true);
            ui.p1Atk.gameObject.SetActive(false);
            HighlightTiles(player2);
        }

        yield return null;
    }

    bool LookForBattle()
    {
        bool combatAlreadyHappend;

        if (turn % 2 == 1)
            combatAlreadyHappend = player1.GetComponent<PlayerScript>().attacked;
        else
            combatAlreadyHappend = player2.GetComponent<PlayerScript>().attacked;

        if (!combatAlreadyHappend)
        {
            int x1 = player1.GetComponent<PlayerScript>().myTile.x;
            int y1 = player1.GetComponent<PlayerScript>().myTile.y;
            int x2 = player2.GetComponent<PlayerScript>().myTile.x;
            int y2 = player2.GetComponent<PlayerScript>().myTile.y;

            if (x1 == x2)
            {
                if (y1 == y2 + 1 || y1 == y2 - 1)
                    return true;
            }
            if (y1 == y2)
            {
                if (x1 == x2 + 1 || x1 == x2 - 1)
                    return true;
            }
            if (x1 == x2 + 1)
            {
                if (y1 == y2 + 1 || y1 == y2 - 1)
                    return true;
            }
            if (x1 == x2 - 1)
            {
                if (y1 == y2 + 1 || y1 == y2 - 1)
                    return true;
            }
        }

        return false;
    }

    IEnumerator Battle()
    {
        audios[1].PlayAudio(0);

        yield return new WaitForSeconds(1);        

        if (turn % 2 == 1)
            player1.GetComponent<PlayerScript>().attacked = true;
        else
            player2.GetComponent<PlayerScript>().attacked = true;

        ui.StartBattle(2 - turn%2);

        yield return new WaitForSeconds(1);

        audios[1].PlayAudio(1);
        ui.RollDices();

        yield return new WaitForSeconds(3);

        ui.ShowResults();

        yield return new WaitForSeconds(4);

        ui.EndBattle();

        if(player1.GetComponent<PlayerScript>().hp <= 0)
        {
            ui.PlayerWon(2);
            audios[1].PlayAudio(4);
        }            
        else if(player2.GetComponent<PlayerScript>().hp <= 0)
        {
            ui.PlayerWon(1);
            audios[1].PlayAudio(4);
        }
            

        battleIsHappening = false;
        if (actions == 0)
            StartCoroutine("EndTurn");
        else
        {
            if (turn % 2 == 1)
                HighlightTiles(player1);
            else
                HighlightTiles(player2);
        }

        yield return null;
    }

    public void DealDamage(PlayerScript winner, PlayerScript loser)
    {
        loser.hp -= winner.atk + winner.bonusAtk;
        ui.SetHp(loser.playerNum, loser.hp);
    }

}
