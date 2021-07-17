using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���λ���ʱ�ĶԻ�
/// </summary>
public class DebutDialog : MonoBehaviour
{
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
		GameManager.Instance.DialogueEnded -= OnDialogEnded;
		DialogEnded?.Invoke();
	}
}
