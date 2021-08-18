using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Effect
{
    public enum Type
    {
        AddHp,
        AddSan,
        Special,
    }

    public Type type;

    public float value;
}
