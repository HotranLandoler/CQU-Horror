using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Inventory
{
	/// <summary>
	/// 物品栏格数
	/// </summary>
	public static readonly int MaxItems = 12;

	/// <summary>
	/// 货币物品id
	/// </summary>
	public static readonly int GoldId = 27;

	public Dictionary<Item, int> ItemNums { get; private set; }

	private Dictionary<Weapon, int> gunAmmos = new Dictionary<Weapon, int>();


	public Inventory(SaveData data)
	{
		ItemNums = new Dictionary<Item, int>();
		if (data == null) return;
		for (int i = 0; i < data.itemNums.Length; i++)
		{
			if (data.itemNums[i] > 0)
				ItemNums.Add(GetItemData(i), data.itemNums[i]);
			if (data.gunAmmos[i] > 0)
				gunAmmos.Add(GetItemData(i) as Weapon, data.gunAmmos[i]);
		}
	}

	private Item GetItemData(int id)
	{
		return GameManager.Instance.ItemData[id];
	}

	public int HasGold() => HasItem(GoldId);

	/// <summary>
	/// 检查是否持有某物品
	/// </summary>
	/// <param name="id"></param>
	/// <returns>持有数量</returns>
	public int HasItem(int id)
	{
		var Item = GetItemData(id);
		return HasItem(Item);
	}

	/// <summary>
	/// 检查是否持有某物品
	/// </summary>
	/// <param name="item"></param>
	/// <returns>持有数量</returns>
	public int HasItem(Item item)
	{
		if (item == null || !ItemNums.ContainsKey(item))
		{
			return 0;
		}
		return ItemNums[item];
	}

	public int GetItemNum(int id)
	{
		foreach (var pair in ItemNums)
		{
			if (pair.Key.Id == id)
				return pair.Value;
		}
		return 0;
	}

	public bool AddItem(int id, int num = 1)
	{
		return AddItem(GetItemData(id), num);
	}

	/// <summary>
	/// 添加物品
	/// </summary>
	/// <param name="item"></param>
	/// <param name="num">数目</param>
	/// <returns>成功添加</returns>
	public bool AddItem(Item item, int num = 1)
	{
		//是否有空余格子
		if (ItemNums.Count >= MaxItems)
        {
			//没有空余格子，是否已有该物品（可堆叠）
			if (!ItemNums.ContainsKey(item))
				return false; //背包已满
		}			
		AudioManager.Instance.PlayPickItemSound(item);
		if (ItemNums.ContainsKey(item))
			ItemNums[item] += num;
		else
			ItemNums.Add(item, num);
		if (item.itemType == ItemType.Weapon)
		{
			var weapon = item as Weapon;
			if (weapon.weaponType == WeaponType.Gun)
			{
				if (!gunAmmos.ContainsKey(weapon))
					gunAmmos.Add(weapon, weapon.MaxAmmo);
			}
		}
		UIManager.Instance.ShowGetItemTip(item, num);
		UIManager.Instance.UpdateInventory();
		return true;
	}

	public void RemoveGold(int num) => RemoveItem(GetItemData(GoldId), num);

	/// <summary>
	/// 减少指定物品数目
	/// </summary>
	/// <param name="item"></param>
	public void RemoveItem(Item item, int num = 1)
	{
		if (ItemNums[item] < num)
			Debug.LogError("Not enough item to remove");
		ItemNums[item] -= num;
		if (ItemNums[item] == 0)
			if (item.itemType != ItemType.Weapon)
				ItemNums.Remove(item);
		UIManager.Instance.UpdateInventory();
	}

	public void RemoveItemAll(Item item)
	{
		if (!ItemNums.ContainsKey(item))
			Debug.LogError("No such item");
		ItemNums.Remove(item);
		UIManager.Instance.UpdateInventory();
	}

	public int GetGunAmmoLoaded(Weapon gun)
	{
		if (!gunAmmos.ContainsKey(gun))
			Debug.LogError("No such gun");
		return gunAmmos[gun];
	}

	public void SetGunAmmoLoaded(Weapon gun, int val)
	{
		if (!gunAmmos.ContainsKey(gun))
			Debug.LogError("No such gun");
		gunAmmos[gun] += val;
		UIManager.Instance.UpdateAmmo(gun);
	}

	/// <summary>
	/// 保存物品数据到数组
	/// </summary>
	/// <returns>数组，长度为item数据库大小，[id]=id对应item的数量</returns>
	public int[] SaveItems() 
	{
		int[] itemNums = new int[GameManager.Instance.ItemData.Length];
		foreach (var pair in ItemNums)
		{
			itemNums[pair.Key.Id] = pair.Value;
		}
		return itemNums;
	}

	/// <summary>
	/// 保存枪上弹数到数组
	/// </summary>
	/// <returns>数组，长度为item数据库大小，[id]=id对应weapon的膛内子弹数</returns>
	public int[] SaveGunAmmo()
    {
		int[] ammos = new int[GameManager.Instance.ItemData.Length];
		foreach (var pair in gunAmmos)
		{
			ammos[pair.Key.Id] = pair.Value;
		}
		return ammos;
	}
}
