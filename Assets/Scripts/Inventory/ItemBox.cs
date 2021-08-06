using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemBox : InventoryPanel
{
    //[SerializeField]
    //private ItemSlot[] itemSlots;

    //private void Start()
    //{
    //    foreach (var slot in itemSlots)
    //    {
    //        slot.OnMouse += ShowItemDesc;
    //        slot.OnMouseLeave += ClearItemDesc;
    //    }
    //}
    [SerializeField]
    private Button exitButton;

    public event UnityAction Closed;

    protected override void Awake()
    {
        base.Awake();
        exitButton.onClick.AddListener(() =>
        {
            UIManager.Instance.RemoveWindow();
            Close();
        });
    }

    public override void Open()
    {
        //GameManager.Instance.SwitchGameMode(GameMode.Timeline);
        GameManager.Instance.ToggleItemBox(true);
        UIManager.Instance.AddWindow(this);
        base.Open();
    }

    public override void Close()
    {
        base.Close();
        //GameManager.Instance.SwitchGameMode(GameMode.Gameplay);
        GameManager.Instance.ToggleItemBox(false);
        Closed?.Invoke();
    }

    protected override Dictionary<Item, int> DataSource => GameManager.Instance.inventory.BoxItems;
    protected override void OnItemClicked(ItemSlot slot)
        => GameManager.Instance.inventory.TakeItemFromBox(slot.item);
}
