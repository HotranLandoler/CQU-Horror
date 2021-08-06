using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShop : InteractObjects
{
    [SerializeField]
    private SkillLearn skillLearnPanel;

    [TextArea(1,4)]
    [SerializeField]
    private string[] dialogOnClosed;

    private void Start()
    {
        skillLearnPanel.Closed += OnShopClosed;
    }
    public override void Interact()
    {
        skillLearnPanel.Open();
    }

    //public void OpenShop()
    //{
    //    skillLearnPanel.Open();  
    //}

    private void OnShopClosed()
    {
        GameManager.Instance.StartDialogue(dialogOnClosed);
    }

    
}
