using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "ScriptableObjects/ScriptableObjectGameInfo", order = 1)]
public class SOgameInfo : ScriptableObject
{
    public int gridX = 16, gridY = 16;

    public bool multiplayer = false;
    
}
