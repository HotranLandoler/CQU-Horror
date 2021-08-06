using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Animator anim;

    private CanvasGroup cg;

    protected void Awake()
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
                anim.Play("UI_FadeIn_Down");
                break;
            case MotionType.Left:
                anim.Play("UI_FadeIn_Left");
                break;
            case MotionType.Right:
                anim.Play("UI_FadeIn_Right");
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
                anim.Play("UI_FadeOut_Down");
                break;
            case MotionType.Left:
                anim.Play("UI_FadeOut_Left");
                break;
            case MotionType.Right:
                anim.Play("UI_FadeOut_Right");
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
