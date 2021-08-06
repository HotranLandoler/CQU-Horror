using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillLearn : UIPanel
{
    public event UnityAction Closed;

    [SerializeField]
    private SkillUI[] skillUis;

    [SerializeField]
    private Text skillNameText;

    [SerializeField]
    private Text skillDescText;

    [SerializeField]
    private Text playerGoldText;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private GameObject cost;

    [SerializeField]
    private Text costText;

    public Color warningColor;

    [SerializeField]
    private Text unlockedText;

    private int playerGold;



    // Start is called before the first frame update
    void Start()
    {
        ClearSkillDesc();
        foreach (var skill in GameManager.Instance.playerSkills.Skills)
        {
            skillUis[skill.SkillID].SetUnlocked();
        }
        foreach (var skillUi in skillUis)
        {
            skillUi.MouseEntered += ShowSkillDesc;
            skillUi.MouseLeft += ClearSkillDesc;
            skillUi.Clicked += LearnSkill;
        }
        closeButton.onClick.AddListener(() =>
        {
            UIManager.Instance.RemoveWindow();
            Close();
        });
        UpdatePlayerGold();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Instance.SwitchGameMode(GameMode.Timeline);
        UIManager.Instance.AddWindow(this);
        UpdatePlayerGold();
    }

    public override void Close()
    {
        base.Close();       
        GameManager.Instance.SwitchGameMode(GameMode.Gameplay);
        Closed?.Invoke();
    }


    private void UpdatePlayerGold()
    {
        playerGold = GameManager.Instance.inventory.HasGold();
        playerGoldText.text = playerGold.ToString();
    }

    private void ShowSkillDesc(SkillData data, SkillUI ui)
    {
        skillNameText.text = data.SkillName;
        skillDescText.text = data.SkillDesc;
        if (ui.IsUnlocked)
        {
            unlockedText.gameObject.SetActive(true);
        }
        else 
        {
            costText.text = data.Cost.ToString();
            if (data.Cost > playerGold)
                costText.color = warningColor;
            else costText.color = new Color(1, 1, 1);
            cost.SetActive(true);
        }
    }

    private void ClearSkillDesc()
    {
        skillNameText.text = string.Empty;
        skillDescText.text = string.Empty;
        unlockedText.gameObject.SetActive(false);
        cost.SetActive(false);
    }

    private void LearnSkill(SkillData data, SkillUI ui)
    {
        //检查条件
        if (ui.IsUnlocked) return;
        if (data.preSkills != null)
        {
            foreach (var skill in data.preSkills)
            {
                if (!GameManager.Instance.playerSkills.HasSkill(skill))
                {
                    //前置技能未学
                    OnLearnFailed();
                    return;
                }
            }
        }
        if (data.Cost > playerGold)
        {
            //金钱不足
            Debug.Log("No enough cash!");
            OnLearnFailed();
            return;
        }
        //学习技能
        ui.Unlock();
        Debug.Log($"学会了 {data.SkillName}!");
        if (data.Cost > 0)
        {
            GameManager.Instance.inventory.RemoveGold(data.Cost);
            UpdatePlayerGold();
        }   
        GameManager.Instance.playerSkills.AddSkill(data);
    }

    private void OnLearnFailed()
    {
        AudioManager.Instance.PlayLearnSkillFailedSound();
    }
}
