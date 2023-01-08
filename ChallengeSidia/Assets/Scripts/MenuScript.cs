using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject main;
    public GameObject play;
    public GameObject options;

    public Text tx, ty;

    public SOgameInfo gi;

    public void Play()
    {
        main.SetActive(false);
        play.SetActive(true);
    }

    public void StartPlay()
    {
        int x = 16, y = 16;
        if (tx.text != "")
        {
            x = int.Parse(tx.text);
            if (x < 4) x = 4;
            if (x > 32) x = 32;
        }
        if (ty.text != "")
        {
            y = int.Parse(ty.text);
            if (y < 4) y = 4;
            if (y > 32) y = 32;
        }

        gi.gridX = x;
        gi.gridY = y;
        gi.multiplayer = true;

        SceneManager.LoadScene("GameScene");
    }

    public void Back()
    {
        main.SetActive(true);
        play.SetActive(false);
        options.SetActive(false);
    }

    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
