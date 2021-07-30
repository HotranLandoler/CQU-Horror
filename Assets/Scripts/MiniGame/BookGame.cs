using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookGame : MonoBehaviour
{
    [SerializeField]
    private GameFlag flag;

    [SerializeField]
    private Box503 box;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private UIPanel gamePanel;

    [SerializeField]
    private UIPanel booksPanel;

    [SerializeField]
    private Book[] bookUis;

    [SerializeField]
    private BookSlot[] bookSlots;

    [SerializeField]
    private AudioClip placeBookSound;

    private Book selectedBook = null;

    private readonly string answer = "0123";

    private System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(4);

    private void Awake()
    {
        if (flag.Has())
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(()=>Exit(false));
        for (int i = 0; i < bookUis.Length; i++)
        {
            bookUis[i].Clicked += SelectBook;
            bookSlots[i].SlotClicked += PlaceBook;
        }
    }

    private void SelectBook(Book book)
    {
        if (selectedBook == null)
        {
            selectedBook = book;
        }
        else 
        {
            selectedBook.Select(false);
            if (selectedBook != book)
                selectedBook = book;
            else selectedBook = null;
        }
    }

    private void PlaceBook(BookSlot slot)
    {
        if (!selectedBook && !slot.PlacedBook)
            return;
        if (slot.PlacedBook != null)
        {
            //�������һ�������û�
            slot.PlacedBook.gameObject.SetActive(true);
            //����ѡ��״̬
            slot.PlacedBook.Select(false);
        }
        if (selectedBook)
        {
            //�������һ���������
            slot.SetBook(selectedBook);
            selectedBook.gameObject.SetActive(false);
            selectedBook = null;
            AudioManager.Instance.PlaySound(placeBookSound);
            UpdateStatus();
        }
        else slot.SetBook(null);
    }

    //����Ƿ���
    private void UpdateStatus()
    {
        stringBuilder.Clear();
        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (bookSlots[i].PlacedBook != null)
                stringBuilder.Append(bookSlots[i].PlacedBook.bookID);
        }
        if (stringBuilder.Length == 4 && answer.Equals(stringBuilder.ToString()))
        {
            //���
            Exit(true);
            //�������ˣ�����
            if (!box.Unlock())
                ResetGame();
        }
    }

    private void ResetGame()
    {
        if (selectedBook)
        {
            selectedBook.Select(false);
            selectedBook = null;
        }
        for (int i = 0; i < bookSlots.Length; i++)
        {
            if (bookSlots[i].PlacedBook != null)
            {
                //�������һ�������û�
                bookSlots[i].PlacedBook.gameObject.SetActive(true);
                //����ѡ��״̬
                bookSlots[i].PlacedBook.Select(false);
                bookSlots[i].SetBook(null);
            }
        }
    }

    public void StartGame()
    {
        GameManager.Instance.SwitchGameMode(GameMode.Timeline);
        gamePanel.Show();
        booksPanel.Show();
    }

    private void Exit(bool win)
    {
        if (!win) ResetGame();
        gamePanel.Hide();
        booksPanel.Hide();
        GameManager.Instance.SwitchGameMode(GameMode.Gameplay);
    }
}
