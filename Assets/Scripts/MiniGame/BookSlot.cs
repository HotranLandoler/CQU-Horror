using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BookSlot : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Book PlacedBook { get; private set; }

    public event UnityAction<BookSlot> SlotClicked;

    [SerializeField]
    private Sprite defaultSprite;

    [SerializeField]
    private Image cursor;

    [SerializeField]
    private Text bookNameText;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 设置槽位放的书
    /// </summary>
    /// <param name="book"></param>
    public void SetBook(Book book)
    {
        PlacedBook = book;
        if (book != null)
        {
            image.sprite = book.BookSprite;
            bookNameText.text = book.BookName;
        }
        else
        {
            image.sprite = defaultSprite;
            bookNameText.text = string.Empty;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.gameObject.SetActive(true);
        bookNameText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.gameObject.SetActive(false);
        bookNameText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SlotClicked?.Invoke(this);
    }
}
