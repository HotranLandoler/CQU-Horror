using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Book : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int bookID;

    public Sprite BookSprite => bookImage.sprite;

    public string BookName => bookNameText.text;

    public event UnityAction<Book> Clicked;

    [SerializeField]
    private Text bookNameText;

    private Color startColor;

    private RectTransform rect;

    private Image bookImage;

    private bool selected = false;

    //选中后上升高度
    private readonly float selectedY = 80;

    private Vector2 targetPos;

    public void OnPointerClick(PointerEventData eventData)
    {
        Select(!selected);
        Clicked?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bookImage.color = new Color(1, 1, 1);
        bookNameText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bookImage.color = startColor;
        bookNameText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        bookImage = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        startColor = bookImage.color;
        targetPos = rect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != rect.anchoredPosition)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, 0.1f);
        }
    }

    public void Select(bool active)
    {
        targetPos = new Vector3(rect.anchoredPosition.x, active ? selectedY : 0);
        selected = active;
    }
}
