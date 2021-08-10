using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //����������Ʒ
    AddHp,
    AddSan,
    Bullet,
    //������Ʒ
    Weapon,
    Other
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int Id;

    public Sprite itemSprite;

    public string Name;

    [TextArea(2,4)]
    public string Desc;

    //public Sprite ItemSprite;
    public ItemType itemType;

    public int val;

    public AudioClip pickSound;

    public AudioClip useSound;
}
