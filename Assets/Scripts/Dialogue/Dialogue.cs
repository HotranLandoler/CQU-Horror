using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JulienFoucher;
using Lean.Localization;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class Dialogue : UIPanel
{
    //[SerializeField]
    //private TextOverTime _textTyper;

    [SerializeField]
    private TextOverTime _typer;

    //[SerializeField]
    //private LocalizeStringEvent _localizer;

    [SerializeField]
    private GameObject dialogArrow;

    public void SetText(string phrase)
    {
        _typer.StartEffect(phrase);
        //_localizer.TranslationName = phraseName;
        //_localizer.UpdateLocalization();
        //_textTyper.SetTargetText(_localizer.FallbackText);
        //_textTyper.StartEffect();
    }

    //public void SetText(LocalizedString phrase)
    //{
    //    _localizer.StringReference = phrase;
    //    //_localizer.TranslationName = phraseName;
    //    //_localizer.UpdateLocalization();
    //    //_textTyper.SetTargetText(_localizer.FallbackText);
    //    //_textTyper.StartEffect();
    //}

    public void SetTypeEndCallback(UnityAction action)
    {
        _typer.m_EventOnEnd.AddListener(action);
    }

    //public void ToggleDialogArrow(bool active)
    //{
    //    dialogArrow.SetActive(active);
    //}
}
