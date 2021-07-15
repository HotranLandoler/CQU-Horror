using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmDialog : UIPanel
{
    [SerializeField]
    private Button confirmButton;

    [SerializeField]
    private Button cancelButton;

    public event UnityAction Confirmed;

    public event UnityAction Canceled;

    private void Start()
    {
        confirmButton.onClick.AddListener(() => Confirmed?.Invoke());
        cancelButton.onClick.AddListener(() => Canceled?.Invoke());
    }
}
