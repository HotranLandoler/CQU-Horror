using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
	public GameFlag flag;

	[ContextMenu("GenerateFlag")]
	private void GenerateFlag()
	{
		flag = ScriptableObject.CreateInstance<GameFlag>();
		//destroyFlag = System.Guid.NewGuid().ToString();
	}

	public void Set()
	{
		GameManager.Instance.gameVariables.SetFlag(flag);
	}

	public bool Has()
    {
		return GameManager.Instance.gameVariables.HasFlag(flag);
    }
}
