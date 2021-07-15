using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyInfo : ScriptableObject
{
    public float Hp;

    public int Damage;

    public float AttackRange = 2;

    public int MoveSpeed = 5;

    public float SpeedMod = 0.7f;

    public AudioClip attackSound;

    public AudioClip dieSound;
}
