using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ʒ����Ϣ
/// </summary>
public struct ItemMessage
{
    public Item itemData;

    public int itemNum;

    public ItemMessage(Item item, int num = 1)
    {
        itemData = item;
        itemNum = num;
    }
}
