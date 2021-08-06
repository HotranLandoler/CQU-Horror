using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private SkillData data;
    
    [SerializeField]
    private Animator unlockAnim;

    [SerializeField]
    private Image lineUI;

    public bool IsUnlocked { get; private set; }

    public event UnityAction<SkillData, SkillUI> Clicked;
    public event UnityAction<SkillData, SkillUI> MouseEntered;
    public event UnityAction MouseLeft;

    private Image image;

    private Color startColor;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {      
        startColor = image.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(data, this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsUnlocked) image.color = new Color(1, 1, 1);
        MouseEntered?.Invoke(data, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsUnlocked) image.color = startColor;
        MouseLeft?.Invoke();
    }

    public void Unlock()
    { 
        AudioManager.Instance.PlaySkillUnlockSound();
        SetUnlocked();
    }

    public void SetUnlocked()
    {
        IsUnlocked = true;
        unlockAnim.SetTrigger("Unlock");
        image.color = new Color(1, 1, 1);
        if (lineUI) lineUI.color = new Color(1, 1, 1);
    }
}
