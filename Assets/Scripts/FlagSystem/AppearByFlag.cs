using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearByFlag : MonoBehaviour
{
	[SerializeField]
	private GameFlag appearFlag;

	void Awake()
	{
		if (!appearFlag.Has())
			gameObject.SetActive(false);
	}

	public void Set()
	{
		// Set Flag..
		appearFlag.Set();
		gameObject.SetActive(true);
	}
}
