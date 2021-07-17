using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InteractObjects
{
    [SerializeField]
    private Item itemGet;

    //不同难度下数量
    [SerializeField]
    private int[] numInModes = new int[] { 1,1,1 };

    [SerializeField]
    private string id;

    private void Awake()
    {
        if (GameManager.Instance.gameVariables.HasFlag(id))
            Destroy(gameObject);
    }

    public override void Interact()
    {
        if (!GameManager.Instance.inventory.AddItem(itemGet, numInModes[(int)Game.difficulty]))
        {
            //添加失败
            GameManager.Instance.StartDialogue(Game.gameStrings.BagFull);
            return;
        }
        GameManager.Instance.gameVariables.SetFlag(id);
        Destroy(gameObject);
    }

    [ContextMenu("Set Flag")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
