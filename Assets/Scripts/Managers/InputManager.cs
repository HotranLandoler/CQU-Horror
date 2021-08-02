using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	/// <summary>
	/// ¼üÎ»ÏµÍ³
	/// </summary>
	public static GameControls Controls;

    private void Awake()
    {
		Controls = new GameControls();
    }

    private void OnEnable()
	{
		Controls.GamePlay.Accept.started += OnAcceptPressed;
		Controls.GamePlay.Bag.started += Bag;
		Controls.GamePlay.Menu.started += Menu;
		Controls.Enable();
	}

	private void OnDisable()
	{
		Controls.Disable();
	}

	private void OnAcceptPressed(InputAction.CallbackContext ctx)
		=> GameManager.Instance.OnAccept();


	private void Bag(InputAction.CallbackContext ctx)
		=> GameManager.Instance.ToggleBag();

	private void Menu(InputAction.CallbackContext ctx)
		=> GameManager.Instance.OnEscapePressed();

	//public void AddInteractObject(UnityAction action)
 //   {
	//	Controls.GamePlay.Accept.started += OnAcceptPressed;
	//}
}
