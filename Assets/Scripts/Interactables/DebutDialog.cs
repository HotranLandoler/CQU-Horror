using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 初次互动时的对话
/// </summary>
public class DebutDialog : MonoBehaviour
{
	[TextArea(3,4)]
    [SerializeField]
    private string[] debutDialog;

	[SerializeField]
	private GameFlag flag;

	public event UnityAction DialogEnded;

	public void StartDialog()
	{
		if (!flag.Has())
		{
			GameManager.Instance.StartDialogue(debutDialog);
			GameManager.Instance.DialogueEnded += OnDialogEnded;
		}
		else
		{
			DialogEnded?.Invoke();
		}
	}

	private void OnDialogEnded()
	{
		flag.Set();
		GameManager.Instance.DialogueEnded -= OnDialogEnded;
		DialogEnded?.Invoke();
	}
}
