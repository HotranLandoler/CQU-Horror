using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public int SkillID;
    public Sprite SkillSprite;
    public string SkillName;
    [TextArea(1,5)]
    public string SkillDesc;
    /// <summary>
    /// 花费残骸数
    /// </summary>
    public int Cost;
    /// <summary>
    /// 前置技能
    /// </summary>
    public SkillData[] preSkills;
}
