using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : InteractObjects
{
	[SerializeField]
	private GameFlag flag;

	[SerializeField]
	private GameObject block;
	private Animator blockAnim;
	private Collider2D blockCollider;

	//打开后的外形
	[SerializeField]
	private Sprite openedSprite;

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

	void Awake()
	{
		blockCollider = block.GetComponent<Collider2D>();
		if (flag.Has())
		{
			//关闭碰撞
			blockCollider.enabled = false;
			//打开后的外形
			block.GetComponent<SpriteRenderer>().sprite = openedSprite;
			Destroy(this);
			return;
		}
		blockAnim = block.GetComponent<Animator>();
	}

	public override void Interact()
	{
		if (GameManager.Instance.inventory.HasItem(key) > 0)
		{
			flag.Set();
			GameManager.Instance.StartDialogue(keyDialog);
			if (keySound) AudioManager.Instance.PlaySound(keySound);
			GameManager.Instance.inventory.RemoveItem(key);
			blockCollider.enabled = false;
			blockAnim.SetTrigger("Unlock");
		}
		else
		{
			GameManager.Instance.StartDialogue(nokeyDialog);
			if (nokeySound) AudioManager.Instance.PlaySound(nokeySound);
		}
	}
}
