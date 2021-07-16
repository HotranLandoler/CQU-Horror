using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlag : ScriptableObject
{
	public string id;
	public GameFlag()
	{
		id = System.Guid.NewGuid().ToString();
	}
}
