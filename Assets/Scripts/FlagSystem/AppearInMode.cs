using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInMode : MonoBehaviour
{
	[SerializeField]
	private bool[] appearInModes = new bool[] { true, true, true };

	void Awake()
	{
		if (!appearInModes[(int)Game.difficulty])
			Destroy(gameObject);
	}
}
