using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables
{
    //private Dictionary<string, bool> gameBools = new Dictionary<string, bool>();

    //public bool GetBool(string name)
    //{
    //    if (gameBools.ContainsKey(name))
    //    {
    //        return gameBools[name];
    //    }
    //    return false;
    //}

    //public void SetBool(string name, bool val)
    //{
    //    if (gameBools.ContainsKey(name))
    //    {
    //        gameBools[name] = val;
    //    }
    //    else
    //    {
    //        gameBools.Add(name, val);
    //    }
    //}

    private HashSet<string> gameFlags = new HashSet<string>();

    public GameVariables()
    {
        gameFlags = new HashSet<string>();
    }

    public GameVariables(string[] flags)
    {
        gameFlags = new HashSet<string>(flags);
    }

    //public void LoadFromArray(string[] flags)
    //{
    //    gameFlags = new HashSet<string>(flags);
    //}


    public bool HasFlag(string name)
    {
        if (gameFlags.Contains(name))
        {
            return true;
        }
        return false;
    }

    public bool HasFlag(GameFlag flag)
    {
        return HasFlag(flag.id);
    }

    public void SetFlag(string name)
    {
        if (!gameFlags.Contains(name))
        {
            gameFlags.Add(name);
        }
        else
            Debug.LogError($"Already has flag {name}");
    }

    public void SetFlag(GameFlag flag)
    {
        SetFlag(flag.id);
    }

    public string[] GetFlagArray()
    {
        string[] flags = new string[gameFlags.Count];
        gameFlags.CopyTo(flags);
        return flags;
    }
}
