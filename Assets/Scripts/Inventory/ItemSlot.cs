using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image slotImage;
    private float startAlpha;
    private float highlightAlpha = 0.8f;

    
    public Item item;

    public Image itemImage;

    public Text itemNumText;

    public event UnityAction<ItemSlot> OnClick;
    public event UnityAction<Item> OnMouse;
    public event UnityAction OnMouseLeave;

    void Awake()
    {
        slotImage = GetComponent<Image>();
        startAlpha = slotImage.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, highlightAlpha);
        OnMouse?.Invoke(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, startAlpha);
        OnMouseLeave?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item == null) return;
        OnClick?.Invoke(this);
        //GameManager.Instance.UseItem(item);
    }
}
