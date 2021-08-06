using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkills
{
    public HashSet<SkillData> Skills { get; private set; }

    /// <summary>
    /// 装弹时间修正
    /// </summary>
    public float ReloadTimeMod { get; private set; } = 1f;

    /// <summary>
    /// 要害伤害加成
    /// </summary>
    public float CriticDamageMod { get; private set; } = 1f;

    /// <summary>
    /// 近战攻击增加理智值
    /// </summary>
    public int MeleeAtkAddSanity { get; private set; } = 0;

    /// <summary>
    /// 近战攻击伤害修正
    /// </summary>
    public float MeleeDamageMod { get; private set; } = 1f;

    /// <summary>
    /// 战斗中降低San时间间隔修正
    /// </summary>
    public float SanityDropIntervalMod { get; private set; } = 1f;

    /// <summary>
    /// San值回复速度修正
    /// </summary>
    public float SanityRecoverMod { get; private set; } = 1f;

    /// <summary>
    /// 生命值低于50%时获得治疗量修正
    /// </summary>
    public float LowHpHealMod { get; private set; } = 1f;

    /// <summary>
    /// 吃零食效果修正
    /// </summary>
    public float SnackEffectMod { get; private set; } = 1f;

    /// <summary>
    /// 跑步速度加成
    /// </summary>
    public float RunSpeedMod { get; private set; } = 1f;

    /// <summary>
    /// 奔跑时每秒减少San
    /// </summary>
    public int RunSanityDrop { get; private set; } = 0;

    //private GameManager gm = GameManager.Instance;

    public PlayerSkills()
    {
        Skills = new HashSet<SkillData>();
    }

    public PlayerSkills(IEnumerable<SkillData> skills)
    {
        Skills = new HashSet<SkillData>(skills);
        foreach (var skill in skills)
        {
            ActivateSkill(skill);
        }
    }

    public void AddSkill(SkillData skill)
    { 
        if (!Skills.Add(skill))
        {
            Debug.LogError($"重复添加技能 {skill.SkillName}");
            return;
        }
        ActivateSkill(skill);
    }

    public bool HasSkill(SkillData skill)
    {
        return Skills.Contains(skill);
    }

    /// <summary>
    /// 清除所有技能
    /// </summary>
    public void ClearSkills()
    {
        foreach (var skill in Skills)
        {
            DeactivateSkill(skill);
        }
        Skills.Clear();
    }

    /// <summary>
    /// 获取当前拥有技能ID列表
    /// </summary>
    /// <returns></returns>
    public int[] GetSkillsArray()
    {
        var items = from n in Skills select n.SkillID;
        return items.ToArray();
    }

    /// <summary>
    /// 激活技能
    /// </summary>
    /// <param name="skill"></param>
    private void ActivateSkill(SkillData skill)
    {
        switch (skill.SkillID)
        {
            case 0: //快速换弹
                {
                    ReloadTimeMod -= 0.3f;
                    break;
                }
            case 1: //精准打击
                {
                    CriticDamageMod += 0.2f;
                    break;
                }
            case 2: //战士信条
                {
                    MeleeAtkAddSanity += 1;
                    break;
                }
            case 3: //搏击者
                {
                    MeleeDamageMod += 0.4f;
                    break;
                }
            case 4: //临危不惧
                {
                    SanityDropIntervalMod += 0.2f;
                    break;
                }
            case 5: //乐观主义
                {
                    SanityRecoverMod += 0.2f;
                    break;
                }
            case 6: //急救
                {
                    LowHpHealMod += 0.5f;
                    break;
                }
            case 7: //美食家
                {
                    SnackEffectMod += 0.5f;
                    break;
                }
            case 8: //狂躁症
                {
                    RunSpeedMod += 0.3f;
                    RunSanityDrop += 1;
                    break;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 取消技能效果
    /// </summary>
    /// <param name="skill"></param>
    private void DeactivateSkill(SkillData skill)
    {
        switch (skill.SkillID)
        {
            case 0: //快速换弹
                {
                    ReloadTimeMod += 0.3f;
                    break;
                }
            case 1: //精准打击
                {
                    CriticDamageMod -= 0.2f;
                    break;
                }
            case 2: //战士信条
                {
                    MeleeAtkAddSanity -= 1;
                    break;
                }
            case 3: //搏击者
                {
                    MeleeDamageMod -= 0.4f;
                    break;
                }
            case 4: //临危不惧
                {
                    SanityDropIntervalMod -= 0.2f;
                    break;
                }
            case 5: //乐观主义
                {
                    SanityRecoverMod -= 0.2f;
                    break;
                }
            case 6: //急救
                {
                    LowHpHealMod -= 0.5f;
                    break;
                }
            case 7: //美食家
                {
                    SnackEffectMod -= 0.5f;
                    break;
                }
            case 8: //狂躁症
                {
                    RunSpeedMod -= 0.3f;
                    RunSanityDrop -= 1;
                    break;
                }
            default:
                break;
        }
    }

}
