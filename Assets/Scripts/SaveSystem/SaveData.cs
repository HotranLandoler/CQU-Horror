using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int sceneId;

    public string sceneName;

    public Game.Difficulty difficulty;

    public DateTime dateTime;

    public string[] gameFlags;

    public int[] skills;

    public int[] itemNums;

    public int[] gunAmmos;

    public int[] boxItems;

    public int Hp;

    public int Sanity;

    public int EquipId;

    public float playerPosX;

    public float playerPosY;

    public int playerDir;
}
