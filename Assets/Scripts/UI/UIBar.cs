using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBar : MonoBehaviour
{
    protected Image val;

    [SerializeField]
    protected Text text;

    [SerializeField]
    private Image valEffect;

    protected virtual void Awake()
    {
        val = GetComponent<Image>();
    }

    //// Start is called before the first frame update
    //protected virtual void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (valEffect.fillAmount > val.fillAmount)
        {
            valEffect.fillAmount -= 0.002f;
        }
        else if (valEffect.fillAmount < val.fillAmount)
            valEffect.fillAmount = val.fillAmount;
    }

    protected virtual void UpdateValue(int value)
    {
        text.text = value.ToString();
    }
}
