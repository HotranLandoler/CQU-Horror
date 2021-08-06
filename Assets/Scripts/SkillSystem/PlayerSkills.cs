using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkills
{
    public HashSet<SkillData> Skills { get; private set; }

    /// <summary>
    /// װ��ʱ������
    /// </summary>
    public float ReloadTimeMod { get; private set; } = 1f;

    /// <summary>
    /// Ҫ���˺��ӳ�
    /// </summary>
    public float CriticDamageMod { get; private set; } = 1f;

    /// <summary>
    /// ��ս������������ֵ
    /// </summary>
    public int MeleeAtkAddSanity { get; private set; } = 0;

    /// <summary>
    /// ��ս�����˺�����
    /// </summary>
    public float MeleeDamageMod { get; private set; } = 1f;

    /// <summary>
    /// ս���н���Sanʱ��������
    /// </summary>
    public float SanityDropIntervalMod { get; private set; } = 1f;

    /// <summary>
    /// Sanֵ�ظ��ٶ�����
    /// </summary>
    public float SanityRecoverMod { get; private set; } = 1f;

    /// <summary>
    /// ����ֵ����50%ʱ�������������
    /// </summary>
    public float LowHpHealMod { get; private set; } = 1f;

    /// <summary>
    /// ����ʳЧ������
    /// </summary>
    public float SnackEffectMod { get; private set; } = 1f;

    /// <summary>
    /// �ܲ��ٶȼӳ�
    /// </summary>
    public float RunSpeedMod { get; private set; } = 1f;

    /// <summary>
    /// ����ʱÿ�����San
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
            Debug.LogError($"�ظ���Ӽ��� {skill.SkillName}");
            return;
        }
        ActivateSkill(skill);
    }

    public bool HasSkill(SkillData skill)
    {
        return Skills.Contains(skill);
    }

    /// <summary>
    /// ������м���
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
    /// ��ȡ��ǰӵ�м���ID�б�
    /// </summary>
    /// <returns></returns>
    public int[] GetSkillsArray()
    {
        var items = from n in Skills select n.SkillID;
        return items.ToArray();
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="skill"></param>
    private void ActivateSkill(SkillData skill)
    {
        switch (skill.SkillID)
        {
            case 0: //���ٻ���
                {
                    ReloadTimeMod -= 0.3f;
                    break;
                }
            case 1: //��׼���
                {
                    CriticDamageMod += 0.2f;
                    break;
                }
            case 2: //սʿ����
                {
                    MeleeAtkAddSanity += 1;
                    break;
                }
            case 3: //������
                {
                    MeleeDamageMod += 0.4f;
                    break;
                }
            case 4: //��Σ����
                {
                    SanityDropIntervalMod += 0.2f;
                    break;
                }
            case 5: //�ֹ�����
                {
                    SanityRecoverMod += 0.2f;
                    break;
                }
            case 6: //����
                {
                    LowHpHealMod += 0.5f;
                    break;
                }
            case 7: //��ʳ��
                {
                    SnackEffectMod += 0.5f;
                    break;
                }
            case 8: //����֢
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
    /// ȡ������Ч��
    /// </summary>
    /// <param name="skill"></param>
    private void DeactivateSkill(SkillData skill)
    {
        switch (skill.SkillID)
        {
            case 0: //���ٻ���
                {
                    ReloadTimeMod += 0.3f;
                    break;
                }
            case 1: //��׼���
                {
                    CriticDamageMod -= 0.2f;
                    break;
                }
            case 2: //սʿ����
                {
                    MeleeAtkAddSanity -= 1;
                    break;
                }
            case 3: //������
                {
                    MeleeDamageMod -= 0.4f;
                    break;
                }
            case 4: //��Σ����
                {
                    SanityDropIntervalMod -= 0.2f;
                    break;
                }
            case 5: //�ֹ�����
                {
                    SanityRecoverMod -= 0.2f;
                    break;
                }
            case 6: //����
                {
                    LowHpHealMod -= 0.5f;
                    break;
                }
            case 7: //��ʳ��
                {
                    SnackEffectMod -= 0.5f;
                    break;
                }
            case 8: //����֢
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
