using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static Difficulty difficulty = Difficulty.Normal;

    public static GameString gameStrings = new GameString();

    /// <summary>
    /// ��Ϸ�Ѷ�
    /// </summary>
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
    }
}
