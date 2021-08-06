using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPanel : MonoBehaviour, IWindow
{
    private enum MotionType
    {
        Fade,
        Down,
        Left,
        Right,
        Up
    }

    [SerializeField]
    private MotionType motionType = MotionType.Fade;

    //public event UnityAction Closed;

    private Animator anim;

    private CanvasGroup cg;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        cg = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        
        switch (motionType)
        {
            case MotionType.Fade:
                //anim.Play("UI_FadeIn");
                anim.SetBool("FadeIn", true);
                break;
            case MotionType.Down:
                anim.SetBool("FadeIn_Down", true);
                break;
            case MotionType.Left:
                anim.Play("UI_FadeIn_Left");
                break;
            case MotionType.Right:
                anim.SetBool("FadeIn_Right", true);
                break;
            case MotionType.Up:
                anim.Play("UI_FadeIn_Up");
                break;
            default:
                break;
        }
        cg.blocksRaycasts = true;
    }

    public void Hide()
    {
        UnityEngine.Assertions.Assert.IsNotNull(anim);
        switch (motionType)
        {
            case MotionType.Fade:
                //anim.Play("UI_FadeOut");
                anim.SetBool("FadeIn", false);
                break;
            case MotionType.Down:
                anim.SetBool("FadeIn_Down", false);
                break;
            case MotionType.Left:
                anim.Play("UI_FadeOut_Left");
                break;
            case MotionType.Right:
                anim.SetBool("FadeIn_Right", false);
                break;
            case MotionType.Up:
                anim.Play("UI_FadeOut_Up");
                break;
            default:
                break;
        }
        cg.blocksRaycasts = false;
    }

    public virtual void Open() => Show();

    public virtual void Close() => Hide();
}
