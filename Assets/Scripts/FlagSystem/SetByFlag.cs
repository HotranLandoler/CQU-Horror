using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetByFlag : MonoBehaviour
{
    [SerializeField]
    private GameFlag flag;

    public UnityEvent FlagSet;

    private void Awake()
    {
        if (flag.Has())
        {
            FlagSet?.Invoke();
        }
    }
}
