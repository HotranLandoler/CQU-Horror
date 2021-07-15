using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTip : UIPanel
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Text itemNameText;

    [SerializeField]
    private Text itemNumText;

    public void SetTip(Item item, int num = 1)
    {
        itemImage.sprite = item.itemSprite;
        itemNameText.text = item.Name;
        itemNumText.text = num.ToString();
    }
}
