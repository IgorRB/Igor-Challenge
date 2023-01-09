using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool isAi = false;

    public int playerNum = 1;
    public TileScript myTile;

    public int hp = 10, atk = 2, bonusAtk = 0, d8s = 0;
    public bool atkAdvantage = false, defAdvantage = false;

    public bool attacked = false;

    public GameObject myShield;

    // Start is called before the first frame update
    void Start()
    {
        myShield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearPowerUps()
    {
        bonusAtk = 0;
        d8s = 0;
        atkAdvantage = false;
    }
}
