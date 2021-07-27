using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IMPORTANT 包含GameFlag的组件，确保放在被依赖脚本的上方
/// </summary>
public class Flag : MonoBehaviour
{
	public GameFlag flag;

	[ContextMenu("GenerateFlag")]
	public void GenerateFlag()
	{
		flag = GameFlag.Create();
		//flag = ScriptableObject.CreateInstance<GameFlag>();
		//destroyFlag = System.Guid.NewGuid().ToString();
	}
}
