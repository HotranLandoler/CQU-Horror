using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBlock : InteractObjects
{
	[SerializeField]
	private GameFlag flag;

	[SerializeField]
	private GameObject block;
	private Animator blockAnim;
	private Collider2D blockCollider;

	////打开后的外形
	//[SerializeField]
	//private Sprite openedSprite;

	[SerializeField]
	private Item key;

	[SerializeField]
	private string[] nokeyDialog;

	[SerializeField]
	private string[] keyDialog;

	[SerializeField]
	private AudioClip keySound;

	[SerializeField]
	private AudioClip nokeySound;

	public UnityEvent Unlocked;

	private SpriteRenderer spRenderer;

	protected override void Awake()
	{
		base.Awake();
		blockCollider = block.GetComponent<Collider2D>();
		if (flag.Has())
		{
			//关闭碰撞
			blockCollider.enabled = false;
			//打开后的外形
			//block.GetComponent<SpriteRenderer>().sprite = openedSprite;
			Destroy(this);
			return;
		}
		blockAnim = block.GetComponent<Animator>();
		spRenderer = GetComponent<SpriteRenderer>();
	}

	public override void Interact()
	{
		if (GameManager.Instance.inventory.HasItem(key) > 0)
		{
			flag.Set();
			if (keySound) AudioManager.Instance.PlaySound(keySound);
			blockAnim.SetTrigger("Unlock");
			Unlocked?.Invoke();
			GameManager.Instance.StartDialogue(keyDialog);
			//TODO 灭火器不消耗
			if (key.Id != 26)
				GameManager.Instance.inventory.RemoveItem(key);
			blockCollider.enabled = false;	
			Destroy(tipAnimator.gameObject);
			spRenderer.enabled = false;
			//TODO 关闭交互
			GameManager.Instance.player.interactObjs.Remove(this);
			Destroy(this);
		}
		else
		{
			GameManager.Instance.StartDialogue(nokeyDialog);
			if (nokeySound) AudioManager.Instance.PlaySound(nokeySound);
		}
	}
}
