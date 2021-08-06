using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Text tipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tipText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tipText.gameObject.SetActive(false);
    }
}
