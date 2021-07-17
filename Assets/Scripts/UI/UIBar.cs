using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Image val;

    [SerializeField]
    protected Text text;

    [SerializeField]
    private Image valEffect;

    [SerializeField]
    private Text tipText;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        tipText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tipText.gameObject.SetActive(false);
    } 
}
