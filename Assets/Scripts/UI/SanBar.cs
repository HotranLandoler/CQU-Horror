using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanBar : UIBar
{
    [SerializeField]
    private Animator bkEffectAnim;

    //public GameManager gm;
    protected override void Awake()
    {
        base.Awake();
        //gm = GameManager.Instance; //#
        GameManager.Instance.SanChanged += UpdateValue;
        GameManager.Instance.PanicStarted += OnPanicStart;
        GameManager.Instance.PanicEnded += OnPanicEnd;
    }

    private void OnPanicStart()
        => bkEffectAnim.SetBool("Effect", true);

    private void OnPanicEnd()
        => bkEffectAnim.SetBool("Effect", false);

    protected override void UpdateValue(int value)
    {
        //Debug.Log("change");
        base.UpdateValue(value);
        val.fillAmount = ((float)value / Game.MaxSan);
    }

    private void OnDestroy()
    {
        GameManager.Instance.SanChanged -= UpdateValue;
        GameManager.Instance.PanicStarted -= OnPanicStart;
        GameManager.Instance.PanicEnded -= OnPanicEnd;
    }
}
