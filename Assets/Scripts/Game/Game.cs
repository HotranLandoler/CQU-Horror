using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static Difficulty difficulty = Difficulty.Normal;

    public static GameString gameStrings = new GameString();

    public static readonly int MaxHp_Easy = 150;

    public static readonly int MaxHp_Norm = 100;

    public static readonly int MaxHp_Hard = 60;

    public static readonly int MaxSan = 100;

    /// <summary>
    /// 重伤对应Hp百分比
    /// </summary>
    public static readonly float SevereHpPercent = 0.4f;

    /// <summary>
    /// 游戏难度
    /// </summary>
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
    }
}
