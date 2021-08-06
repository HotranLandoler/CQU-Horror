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
    /// ���Ѳк���
    /// </summary>
    public int Cost;
    /// <summary>
    /// ǰ�ü���
    /// </summary>
    public SkillData[] preSkills;
}
