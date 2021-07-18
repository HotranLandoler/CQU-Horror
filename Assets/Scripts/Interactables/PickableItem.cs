using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flag))]
public class PickableItem : InteractObjects
{
    [SerializeField]
    private Item itemGet;

    //��ͬ�Ѷ�������
    [SerializeField]
    private int[] numInModes = new int[] { 1,1,1 };

    private Flag flag;
    //[SerializeField]
    //private GameFlag id;

    private void Awake()
    {
        flag = GetComponent<Flag>();
        if (flag.flag.Has())
            Destroy(gameObject);
    }

    public override void Interact()
    {
        if (!GameManager.Instance.inventory.AddItem(itemGet, numInModes[(int)Game.difficulty]))
        {
            //���ʧ��
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
