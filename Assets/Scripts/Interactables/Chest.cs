using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Flag))]
public class Chest : MonoBehaviour, IInteractable
{
	public static readonly string openedKey = "Entry_2";

	[SerializeField]
	private Item itemGet;

	[SerializeField]
	private int[] numInModes = new int[3] { 1,1,1 };

	[SerializeField]
	private AudioClip openSound;
	
	//--------交互提示
	[SerializeField]
	private Animator tipAnim;

	[SerializeField]
	private Animator pointAnim;

	private bool opened = false;
	
	private Flag flag;

	private SpriteResolver spResolver;

	private Animator animator;

	void Awake()
	{
		spResolver = GetComponent<SpriteResolver>();
		animator = GetComponent<Animator>();
		flag = GetComponent<Flag>();
		if (flag.Has())
		{
			animator.enabled = false;
			spResolver.SetCategoryAndLabel(spResolver.GetCategory(), openedKey);
			opened = true;
			//移除互动提示
			Destroy(this);
			return;
		}
	}

	public void Interact()
    {
		if (!opened)
		{
			opened = true;
			StartCoroutine(GetItem());
		}
	}

	private IEnumerator GetItem()
	{
		AudioManager.Instance.PlaySound(openSound);
		animator.SetTrigger("Open");
		yield return new WaitForSeconds(0.3f);
		//if (opened) return;
		if (!GameManager.Instance.inventory.AddItem(itemGet, numInModes[(int)Game.difficulty]))
		{
			//添加失败
			animator.SetTrigger("Close");
			GameManager.Instance.StartDialogue(Game.gameStrings.BagFull);
			opened = false;
			yield break;
		}
		flag.Set();
		pointAnim.gameObject.SetActive(false);
		Destroy(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			var player = collision.GetComponent<Player>();
			if (!player.interactObjs.Contains(this))
				player.interactObjs.Add(this);
			pointAnim.SetBool("FadeIn", true);
			tipAnim.SetBool("MovFadeIn", true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<Player>().interactObjs.Remove(this);
			pointAnim.SetBool("FadeIn", false);
			tipAnim.SetBool("MovFadeIn", false);
		}
	}
}
