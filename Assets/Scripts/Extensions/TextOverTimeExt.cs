using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JulienFoucher;

public static class TextOverTimeExt
{
    public static string GetCurrText(this TextOverTime typer)
    {
        return typer.m_TargetText;
    }
}
