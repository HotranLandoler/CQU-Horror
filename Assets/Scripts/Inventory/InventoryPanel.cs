using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private Animator _animator;
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private ItemSlot[] itemSlots = new ItemSlot[9];

    [SerializeField]
    private Color weaponTextColor;

    [SerializeField]
    private Text ItemNameText;

    [SerializeField]
    private Text ItemDescText;

    //public Sprite[] itemSprites;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
        foreach (var slot in itemSlots)
        {
            slot.OnMouse += ShowItemDesc;
            slot.OnMouseLeave += ClearItemDesc;
        }
    }

    /// <summary>
    /// 更新全部物品UI
    /// </summary>
    public void UpdateItems()
    {
        int i = 0;
        foreach (var pair in GameManager.Instance.inventory.ItemNums)
        {
            UpdateSlot(itemSlots[i], pair.Key, pair.Value);
            i++;
        }
        for (;i<Inventory.MaxItems;i++)
        {
            itemSlots[i].item = null;
            itemSlots[i].itemImage.enabled = false;
            itemSlots[i].itemImage.sprite = null;
            itemSlots[i].itemNumText.enabled = false;
            //itemSlots[i].itemNumText.color = weaponTextColor;
        }
    }

    /// <summary>
    /// 更新单个物品
    /// </summary>
    /// <param name="key"></param>
    public void UpdateItem(Item key)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.item.Equals(key))
            {
                UpdateSlot(slot, key, GameManager.Instance.inventory.ItemNums[key]);
                return;
            }
        }
    }

    public void Show()
    {
        _canvasGroup.blocksRaycasts = true;
        _animator.Play("Bag_Open");
    }

    public void Hide()
    {
        _canvasGroup.blocksRaycasts = false;
        _animator.Play("Bag_Close");
    }

    public void ClearItemDesc()
    {
        ItemNameText.text = string.Empty;
        ItemDescText.text = string.Empty;
    }

    private void ShowItemDesc(Item item)
    {
        if (item == null) return;
        //var item = GameManager.Instance.GetItemData(id);
        ItemNameText.text = item.Name;
        ItemDescText.text = item.Desc;
    }

    private void UpdateSlot(ItemSlot slot, Item key, int value)
    {
        slot.item = key;
        slot.itemImage.sprite = key.itemSprite; 
        slot.itemNumText.text = value.ToString();
        //武器蓝字显示子弹数
        if (key.itemType == ItemType.Weapon)
        {
            slot.itemNumText.color = weaponTextColor;
            var weapon = key as Weapon;
            if (weapon.weaponType == WeaponType.Gun)
            {
                //Debug.Log(GameManager.Instance.GetGunAmmoLoaded(weapon));
                slot.itemNumText.text = GameManager.Instance.inventory.GetGunAmmoLoaded(weapon).ToString();
            }
                
        }
        else
            slot.itemNumText.color = Color.white;
        //其他物品不显示数量
        if (key.itemType != ItemType.Other)
            slot.itemNumText.enabled = true;
        slot.itemImage.enabled = true;
    }

    
}
