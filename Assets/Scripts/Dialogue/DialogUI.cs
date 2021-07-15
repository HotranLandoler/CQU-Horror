using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    private Image image;

    private Text text;

    private Color color;

    private float maxAlpha;

    private float targetAlpha;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        text.gameObject.SetActive(false);
        maxAlpha = image.color.a;
        color = image.color;
        color.a = 0;
        image.color = color;

    }

    // Update is called once per frame
    void Update()
    {
        if (image.color.a != targetAlpha)
        {
            color = image.color;
            color.a = Mathf.Lerp(image.color.a, targetAlpha, 0.1f);
            image.color = color;
        }
    }

    public void Fade()
    {
        text.gameObject.SetActive(false);
        targetAlpha = 0f;
    }

    public void Show()
    {
        text.gameObject.SetActive(true);
        targetAlpha = maxAlpha;
    }
}
