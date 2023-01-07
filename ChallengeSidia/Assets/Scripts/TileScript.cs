using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject myObj;
    public int x, y;
    public bool movable = false;
    //public TileScript neighborNorth, neighborSouth, neighborEast, neighborWest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");

        gc.GetComponent<GameControllerScript>().TryMove(x, y);
    }
}
