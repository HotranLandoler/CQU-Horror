using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class GameOptions : MonoBehaviour
{
    [SerializeField]
    private Button applyButton;

    //[SerializeField]
    //private Toggle cnToggle;

    //[SerializeField]
    //private Toggle enToggle;

    // Start is called before the first frame update
    void Awake()
    {
        //string lang = LeanLocalization.GetFirstCurrentLanguage();
        //Debug.Log(lang);
        //if (lang.Equals("English"))
        //{
        //    enToggle.isOn = true;
        //}
        //else
        //    cnToggle.isOn = true;
        //cnToggle.onValueChanged.AddListener(LangOptionChanged);
        //enToggle.onValueChanged.AddListener(LangOptionChanged);
        applyButton.onClick.AddListener(Back);
    }

    //private void LangOptionChanged(bool active)
    //{
    //    if (active)
    //    {
    //        if (!cnToggle.isOn)
    //            LeanLocalization.SetCurrentLanguageAll("English");
    //        else
    //            LeanLocalization.SetCurrentLanguageAll("Chinese");
    //    }
    //}

    private void Back()
    {
        UIManager.Instance.ToggleOptionsInGame(false);
    }
}
