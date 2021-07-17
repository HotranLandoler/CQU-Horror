using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flag", menuName = "GameFlag")]
public class GameFlag : ScriptableObject
{
	public string id;
	public GameFlag()
	{
		id = System.Guid.NewGuid().ToString();
	}

	public void Set()
	{
		GameManager.Instance.gameVariables.SetFlag(id);
	}

	public bool Has()
	{
		return GameManager.Instance.gameVariables.HasFlag(id);
	}

	private void InitId()
	{
		id = System.Guid.NewGuid().ToString();
	}

	public static GameFlag Create()
	{
		var flag = CreateInstance<GameFlag>();
		flag.InitId();
		return flag;
	}
}
