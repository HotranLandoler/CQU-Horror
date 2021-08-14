using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour
{
    private static InputActionRebindingExtensions.RebindingOperation rebindOpr;
    
    private Button button;

    [SerializeField]
    private Text keyText;

    [SerializeField]
    private Image bindAnim;

    [SerializeField]
    private InputActionReference keyRef;


    // Start is called before the first frame update
    void Start()
    {
        bindAnim.gameObject.SetActive(false);
        button = GetComponent<Button>();
        //TODO 实现绑定键位
        //button.onClick.AddListener(StartRebinding);
        if (keyRef == null) return;
        SetBindText();
    }

    private void StartRebinding()
    {
        if (rebindOpr != null)
            rebindOpr.Cancel();
        ToggleBindingAnim(true);
        rebindOpr = keyRef.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnCancel(op => OnRebindComplete())
            .OnComplete(op => OnRebindComplete())
            .Start();
    }

    private void OnRebindComplete()
    {
        rebindOpr.Dispose();
        SetBindText();
        ToggleBindingAnim(false);
    }

    private void SetBindText()
    {
        int index = 0;
        //int index = keyRef.action.GetBindingIndexForControl(keyRef.action.controls[0]);
        keyText.text = InputControlPath.ToHumanReadableString(keyRef.action.bindings[index].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    private void Save()
    {
        //string rebinds = InputManager.Instance.Controls.GamePlay.
    }

    private void ToggleBindingAnim(bool active)
    {
        bindAnim.gameObject.SetActive(active);
        button.gameObject.SetActive(!active);
    }
}
