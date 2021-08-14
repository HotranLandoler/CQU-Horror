using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    /// <summary>
    /// �������ֵ
    /// </summary>
    [SerializeField]
    private float hp;

    [Tooltip("�˺� ��-��")]
    [SerializeField]
    private int[] damage = new int[2];

    [Tooltip("������Χ ��-Զ")]
    [SerializeField]
    private float[] attackRangeSqr = new float[2];

    [Tooltip("�ƶ��ٶ� ��-��-��")]
    [SerializeField]
    private float[] moveSpeed = new float[] { 5, 5, 5 };

    [Tooltip("����к���")]
    [SerializeField]
    private int gold;

    /// <summary>
    /// б���ƶ�����
    /// </summary>
    public float diagSpeedMod = 0.7f;

    /// <summary>
    /// �����ж�CD
    /// </summary>
    public float[] SpecialCd = new float[] { 5, 12 };

    /// <summary>
    /// ��ͨ�������
    /// </summary>
    public float AttackInterval = 1f;

    [Header("Sounds")]
    /// <summary>
    /// �Ų�����
    /// </summary>
    public AudioClip StepSound;

    /// <summary>
    /// ��������
    /// </summary>
    public AudioClip AttackSound;

    /// <summary>
    /// �����ж�����
    /// </summary>
    public AudioClip[] SpecialSounds;

    /// <summary>
    /// ��������
    /// </summary>
    public AudioClip DieSound;

    /// <summary>
    /// ��ȡ�������ֵ
    /// </summary>
    public float MaxHp => hp;

    /// <summary>
    /// �ṥ���˺�
    /// </summary>
    public int LightDamage => damage[0];

    /// <summary>
    /// �ع����˺�
    /// </summary>
    public int HeavyDamage => damage[1];

    /// <summary>
    /// ����������
    /// </summary>
    public float ShortAtkRangeSqr => attackRangeSqr[0];

    /// <summary>
    /// Զ��������
    /// </summary>
    public float LongAtkRangeSqr => attackRangeSqr[1];

    /// <summary>
    /// ������
    /// </summary>
    public float SlowSpeed => moveSpeed[0];

    /// <summary>
    /// ������
    /// </summary>
    public float NormSpeed => moveSpeed[1];

    /// <summary>
    /// ������
    /// </summary>
    public float FastSpeed => moveSpeed[2];

    /// <summary>
    /// ���������
    /// </summary>
    public int Gold => gold;
}
