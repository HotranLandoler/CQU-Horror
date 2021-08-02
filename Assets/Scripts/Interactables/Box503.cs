using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box503 : InteractObjects
{
    [SerializeField]
    private GameFlag flag;

    [SerializeField]
    private SpriteRenderer box;

    [SerializeField]
    private Item[] books;

    [SerializeField]
    private Item itemGet;

    [SerializeField]
    private BookGame puzzle;

    //û��
    [SerializeField]
    private string[] noBookDialog;

    //�鲻���ı�
    [SerializeField]
    private string[] moreBookDialog;

    [SerializeField]
    private string[] enoughBookDialog;

    [SerializeField]
    private string[] unlockedDialog;

    [SerializeField]
    private string[] bagFullDialog;

    [SerializeField]
    private Sprite openedSprite;

    protected override void Awake()
    {
        if (flag.Has())
        {
            box.sprite = openedSprite;
            Destroy(this);
            return;
        }
        base.Awake();
    }

    public override void Interact()
    {
        bool hasAnyBook = false;
        for (int i = 0; i < books.Length; i++)
        {
            if (GameManager.Instance.inventory.HasItem(books[i]) <= 0)
            {
                if (hasAnyBook)
                {
                    //�鲻��
                    GameManager.Instance.StartDialogue(moreBookDialog);
                    return;
                }
            }
            else hasAnyBook = true;
        }
        if (!hasAnyBook)
        {
            //û��
            GameManager.Instance.StartDialogue(noBookDialog);
            return;
        }
        GameManager.Instance.StartDialogue(enoughBookDialog);
        GameManager.Instance.DialogueEnded += StartPuzzle;
    }

    /// <summary>
    /// �����������Ʒ
    /// </summary>
    /// <returns>�Ƿ�ɹ��õ���Ʒ��������������</returns>
    public bool Unlock()
    {
        if (GameManager.Instance.inventory.AddItem(itemGet))
        {
            flag.Set();
            Deactivate();
            box.sprite = openedSprite;
            Destroy(this);
            GameManager.Instance.StartDialogue(unlockedDialog);
            //�����ı���
            foreach (var item in books)
            {
                GameManager.Instance.inventory.RemoveItem(item);
            }
            return true;
        }
        GameManager.Instance.StartDialogue(Game.gameStrings.BagFull);
        return false;
    }

    private void StartPuzzle()
    {
        GameManager.Instance.DialogueEnded -= StartPuzzle;
        puzzle.StartGame();
    }
}
