using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Locator
{
    private static readonly Dictionary<Type, object>
        Services = new Dictionary<Type, object>(5);

    //public static UIManager Ui { get; set; }

    public static void Register<T>(object serviceInstance)
    {
        Services[typeof(T)] = serviceInstance;
    }

    public static T Get<T>()
    {
        return (T)Services[typeof(T)];
    }

    public static void Reset()
    {
        Services.Clear();
    }
}
