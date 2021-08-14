using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    [SerializeField]
    private float hp;

    [Tooltip("伤害 轻-重")]
    [SerializeField]
    private int[] damage = new int[2];

    [Tooltip("攻击范围 近-远")]
    [SerializeField]
    private float[] attackRangeSqr = new float[2];

    [Tooltip("移动速度 慢-中-快")]
    [SerializeField]
    private float[] moveSpeed = new float[] { 5, 5, 5 };

    [Tooltip("掉落残骸数")]
    [SerializeField]
    private int gold;

    /// <summary>
    /// 斜向移动减速
    /// </summary>
    public float diagSpeedMod = 0.7f;

    /// <summary>
    /// 特殊行动CD
    /// </summary>
    public float[] SpecialCd = new float[] { 5, 12 };

    /// <summary>
    /// 普通攻击间隔
    /// </summary>
    public float AttackInterval = 1f;

    [Header("Sounds")]
    /// <summary>
    /// 脚步声音
    /// </summary>
    public AudioClip StepSound;

    /// <summary>
    /// 攻击声音
    /// </summary>
    public AudioClip AttackSound;

    /// <summary>
    /// 特殊行动声音
    /// </summary>
    public AudioClip[] SpecialSounds;

    /// <summary>
    /// 死亡声音
    /// </summary>
    public AudioClip DieSound;

    /// <summary>
    /// 获取最大生命值
    /// </summary>
    public float MaxHp => hp;

    /// <summary>
    /// 轻攻击伤害
    /// </summary>
    public int LightDamage => damage[0];

    /// <summary>
    /// 重攻击伤害
    /// </summary>
    public int HeavyDamage => damage[1];

    /// <summary>
    /// 近攻击距离
    /// </summary>
    public float ShortAtkRangeSqr => attackRangeSqr[0];

    /// <summary>
    /// 远攻击距离
    /// </summary>
    public float LongAtkRangeSqr => attackRangeSqr[1];

    /// <summary>
    /// 慢移速
    /// </summary>
    public float SlowSpeed => moveSpeed[0];

    /// <summary>
    /// 中移速
    /// </summary>
    public float NormSpeed => moveSpeed[1];

    /// <summary>
    /// 快移速
    /// </summary>
    public float FastSpeed => moveSpeed[2];

    /// <summary>
    /// 掉落货币数
    /// </summary>
    public int Gold => gold;
}
