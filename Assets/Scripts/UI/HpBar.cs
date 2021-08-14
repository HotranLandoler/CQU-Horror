using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : UIBar
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        //Debug.Log(GameManager.Instance == null);
        GameManager.Instance.HpChanged += UpdateValue;
    }

    protected override void UpdateValue(int value)
    {
        base.UpdateValue(value);
        val.fillAmount = ((float)value / GameManager.Instance.MaxHp);
    }

    private void OnDestroy()
    {
        GameManager.Instance.HpChanged -= UpdateValue;
    }
}
