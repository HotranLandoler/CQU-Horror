using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : UIPanel
{
    //private Animator _animator;
    //private CanvasGroup _canvasGroup;

    [SerializeField]
    private ItemSlot[] itemSlots = new ItemSlot[9];

    [SerializeField]
    private Color weaponTextColor;

    [SerializeField]
    private Text ItemNameText;

    [SerializeField]
    private Text ItemDescText;

    protected virtual Dictionary<Item, int> DataSource => GameManager.Instance.inventory.ItemNums;

    //protected virtual int Capacity => Inventory.MaxItems;

    //public Sprite[] itemSprites;

    //protected override void Awake()
    //{
    //    base.Awake();
    //}

    protected void Start()
    {
        //_canvasGroup = GetComponent<CanvasGroup>();
        //_animator = GetComponent<Animator>();
        foreach (var slot in itemSlots)
        {
            slot.OnMouse += ShowItemDesc;
            slot.OnMouseLeave += ClearItemDesc;
            slot.OnClick += OnItemClicked;
        }
    }

    /// <summary>
    /// 更新全部物品UI
    /// </summary>
    public void UpdateItems()
    {
        int i = 0;
        foreach (var pair in DataSource)
        {
            UpdateSlot(itemSlots[i], pair.Key, pair.Value);
            i++;
        }
        for (;i<itemSlots.Length;i++)
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
                UpdateSlot(slot, key, DataSource[key]);
                return;
            }
        }
    }

    //public void Show()
    //{
    //    _canvasGroup.blocksRaycasts = true;
    //    _animator.Play("Bag_Open");
    //}

    //public void Hide()
    //{
    //    _canvasGroup.blocksRaycasts = false;
    //    _animator.Play("Bag_Close");
    //}

    public void ClearItemDesc()
    {
        ItemNameText.text = string.Empty;
        if (ItemDescText) ItemDescText.text = string.Empty;
    }

    private void ShowItemDesc(Item item)
    {
        if (item == null) return;
        //var item = GameManager.Instance.GetItemData(id);
        ItemNameText.text = item.Name;
        if (ItemDescText) ItemDescText.text = item.Desc;
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
        //其他类型物品不显示数量
        slot.itemNumText.enabled = key.itemType != ItemType.Other;
        slot.itemImage.enabled = true;
    }

    protected virtual void OnItemClicked(ItemSlot slot) 
        => GameManager.Instance.UseItem(slot.item);

}
