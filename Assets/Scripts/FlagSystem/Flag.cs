using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IMPORTANT ����GameFlag�������ȷ�����ڱ������ű����Ϸ�
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
