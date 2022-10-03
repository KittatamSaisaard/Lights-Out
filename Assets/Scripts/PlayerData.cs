using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Stores data about the player
public class PlayerData
{
    public int level;

    public PlayerData(int p_level)
    {
        level = p_level;
    }
}
