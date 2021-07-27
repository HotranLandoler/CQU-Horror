using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanBar : UIBar
{
    //public GameManager gm;
    protected override void Awake()
    {
        base.Awake();
        //gm = GameManager.Instance; //#
        GameManager.Instance.SanChanged += UpdateValue;
    }

    protected override void UpdateValue(int value)
    {
        //Debug.Log("change");
        base.UpdateValue(value);
        val.fillAmount = ((float)value / Game.MaxSan);
    }

    private void OnDestroy()
    {
        GameManager.Instance.SanChanged -= UpdateValue;
    }
}
