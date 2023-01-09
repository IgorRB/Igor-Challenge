using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiController : MonoBehaviour
{
    [Header("Controller Objs Reference")]
    public GameControllerScript gc;

    [Header("Base Gui")]
    public Text p1Hp;
    public Text p2Hp, p1Atk, p2Atk, turn, actions, endGameText;
    public GameObject endGamePanel;

    [Header("Battle Gui")]
    public GameObject battlePanel;
    public GameObject resultPanel;
    public Text[] results1, results2;
    public Text battleResultText;

    [Header("Player 1 Battle Variables")]
    public Text firstStrike1;
    public Text atkPower1;
    public GameObject[] p1d6s;
    public GameObject[] p1d8s;

    [Header("Player 2 Battle Variables")]
    public Text firstStrike2;
    public Text atkPower2;
    public GameObject[] p2d6s;
    public GameObject[] p2d8s;

    List<int> p1DiceValues;
    List<int> p2DiceValues;

    private void Start()
    {
        ClearDiceValues();
        battlePanel.SetActive(false);
        endGamePanel.SetActive(false);
    }


    public void SetHp(int plr, int value)
    {
        if(plr == 1)
        {
            p1Hp.text = ("Health Points: " + value.ToString());
        }
        else
        {
            p2Hp.text = ("Health Points: " + value.ToString());
        }
    }

    public void SetActions(int value)
    {
        actions.text = ("Actions: " + value);
    }

    public void SetTurn(int value)
    {
        turn.text = ("Player " + value + "'s turn");
    }

    public void StartBattle(int attackingPlayer)
    {
        p1Atk.gameObject.SetActive(false);
        p2Atk.gameObject.SetActive(false);

        battlePanel.SetActive(true);
        resultPanel.SetActive(false);

        if( attackingPlayer == 1)
        {
            firstStrike1.gameObject.SetActive(true);
            firstStrike2.gameObject.SetActive(false);
        }
        else
        {
            firstStrike2.gameObject.SetActive(true);
            firstStrike1.gameObject.SetActive(false);
        }

        atkPower1.text = ("Attack Power: " + (gc.player1.GetComponent<PlayerScript>().atk + gc.player1.GetComponent<PlayerScript>().bonusAtk));
        atkPower2.text = ("Attack Power: " + (gc.player2.GetComponent<PlayerScript>().atk + gc.player2.GetComponent<PlayerScript>().bonusAtk));

        HideDices();
    }

    void HideDices()
    {
        for (int i = 0; i < 5; i++)
        {
            p1d6s[i].SetActive(false);
            p2d6s[i].SetActive(false);
            p1d8s[i].SetActive(false);
            p2d8s[i].SetActive(false);
        }
    }

    public void RollDices()
    {
        PlayerScript p1, p2;
        p1 = gc.player1.GetComponent<PlayerScript>();
        p2 = gc.player2.GetComponent<PlayerScript>();

        ClearDiceValues();

        for (int i = 0; i < p1.d8s; i++)
        {
            p1d8s[i].SetActive(true);
            p1DiceValues.Add(Random.Range(1, 9));
            p1d8s[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = p1DiceValues[i].ToString();
        }
        for (int i = 0; i < 4; i++)
        {
            if (!p1d8s[i].activeInHierarchy)
            {
                p1d6s[i].SetActive(true);
                p1DiceValues.Add(Random.Range(1, 7));
                p1d6s[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = p1DiceValues[i].ToString();
            }
        }
        if (firstStrike1.isActiveAndEnabled)
        {
            if (p1.atkAdvantage)
            {
                p1d6s[4].SetActive(true);
                p1DiceValues.Add(Random.Range(1, 7));
                p1d6s[4].transform.GetChild(0).gameObject.GetComponent<Text>().text = p1DiceValues[4].ToString();
            }
        }
        else
        {
            if (p1.defAdvantage)
            {
                p1d8s[4].SetActive(true);
                p1DiceValues.Add(Random.Range(1, 9));
                p1d8s[4].transform.GetChild(0).gameObject.GetComponent<Text>().text = p1DiceValues[4].ToString();
                p1.defAdvantage = false;
                p1.myShield.SetActive(false);
            }
        }


        for (int i = 0; i < p2.d8s; i++)
        {
            p2d8s[i].SetActive(true);
            p2DiceValues.Add(Random.Range(1, 9));
            p2d8s[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = p2DiceValues[i].ToString();
        }
        for (int i = 0; i < 4; i++)
        {
            if (!p2d8s[i].activeInHierarchy)
            {
                p2d6s[i].SetActive(true);
                p2DiceValues.Add(Random.Range(1, 7));
                p2d6s[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = p2DiceValues[i].ToString();
            }
                
        }
        if (firstStrike2.isActiveAndEnabled)
        {
            if (p2.atkAdvantage)
            {
                p2d6s[4].SetActive(true);
                p2DiceValues.Add(Random.Range(1, 7));
                p2d6s[4].transform.GetChild(0).gameObject.GetComponent<Text>().text = p2DiceValues[4].ToString();
            }                
        }
        else
        {
            if (p2.defAdvantage)
            {
                p2d8s[4].SetActive(true);
                p2DiceValues.Add(Random.Range(1, 9));
                p2d8s[4].transform.GetChild(0).gameObject.GetComponent<Text>().text = p2DiceValues[4].ToString();
                p2.defAdvantage = false;
                p2.myShield.SetActive(false);
            }
        }
    }

    public void ShowResults()
    {
        p1DiceValues.Sort();
        p1DiceValues.Reverse();
        p2DiceValues.Sort();
        p2DiceValues.Reverse();

        int p1wins = 0, p2wins = 0;

        resultPanel.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            results1[i].text = p1DiceValues[i].ToString();
            results2[i].text = p2DiceValues[i].ToString();
            results1[i].gameObject.SetActive(true);
            results2[i].gameObject.SetActive(true);

            if(firstStrike1.isActiveAndEnabled)
            {
                if (p1DiceValues[i] >= p2DiceValues[i])
                    p1wins++;
                else
                    p2wins++;
            }
            else
            {
                if (p2DiceValues[i] >= p1DiceValues[i])
                    p2wins++;
                else
                    p1wins++;
            }
        }

        if (firstStrike1.isActiveAndEnabled)
        {
            if(p1wins >= p2wins)
            {
                battleResultText.text = "Player 1 won!";
                gc.audios[1].PlayAudio(3);
                Instantiate(gc.particles[0], gc.player2.transform.position, Quaternion.identity);
                gc.DealDamage(gc.player1.GetComponent<PlayerScript>(), gc.player2.GetComponent<PlayerScript>());
            }
            else
            {
                battleResultText.text = "Player 2 won!";
                Instantiate(gc.particles[0], gc.player1.transform.position, Quaternion.identity);
                gc.audios[1].PlayAudio(2);
                gc.DealDamage(gc.player2.GetComponent<PlayerScript>(), gc.player1.GetComponent<PlayerScript>());
            }
        }
        else
        {
            if (p2wins >= p1wins)
            {
                battleResultText.text = "Player 2 won!";
                Instantiate(gc.particles[0], gc.player1.transform.position, Quaternion.identity);
                gc.audios[1].PlayAudio(2);
                gc.DealDamage(gc.player2.GetComponent<PlayerScript>(), gc.player1.GetComponent<PlayerScript>());
            }
            else
            {
                battleResultText.text = "Player 1 won!";
                Instantiate(gc.particles[0], gc.player2.transform.position, Quaternion.identity);
                gc.audios[1].PlayAudio(3);
                gc.DealDamage(gc.player1.GetComponent<PlayerScript>(), gc.player2.GetComponent<PlayerScript>());
            }
        }

    }

    void ClearDiceValues()
    {
        p1DiceValues = new List<int>();
        p2DiceValues = new List<int>();
    }

    public void EndBattle()
    {
        battlePanel.SetActive(false);
    }

    public void PlayerWon(int n)
    {
        endGamePanel.SetActive(true);
        endGameText.text = ("Player " + n + " won!");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reload()
    {
        SceneManager.LoadScene("GameScene");
    }
}
