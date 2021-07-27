using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flag))]
public class PickableItem : InteractObjects
{
    [SerializeField]
    private Item itemGet;

    //不同难度下数量
    [SerializeField]
    protected int[] numInModes = new int[] { 1,1,1 };

    protected Flag flag;
    //[SerializeField]
    //private GameFlag id;

    protected override void Awake()
    {
        base.Awake();
        flag = GetComponent<Flag>();
    }

    protected void Start()
    {        
        if (flag.flag.Has())
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
        flag.flag.Set();
        Destroy(gameObject);
    }

    //[ContextMenu("Set Flag")]
    //private void GenerateId()
    //{
    //    id = System.Guid.NewGuid().ToString();
    //}
}
