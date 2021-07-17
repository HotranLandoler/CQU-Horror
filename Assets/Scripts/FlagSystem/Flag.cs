using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
	public GameFlag flag;

	[ContextMenu("GenerateFlag")]
	private void GenerateFlag()
	{
		flag = GameFlag.Create();
		//flag = ScriptableObject.CreateInstance<GameFlag>();
		//destroyFlag = System.Guid.NewGuid().ToString();
	}
}
