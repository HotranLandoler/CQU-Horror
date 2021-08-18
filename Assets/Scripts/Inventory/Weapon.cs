using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Gun,
    Melee,
    Magic
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : Item
{
    public WeaponType weaponType;

    public int BulletId;

    public int MaxAmmo;

    public float ShootInterval = 1;

    public float Damage = 1;

    public int BulletsPerShot = 1;

    public AudioClip Sound;

    public GameObject Prefab;
}
