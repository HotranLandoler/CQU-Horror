using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //����������Ʒ
    Supply,
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

    public float val;

    public bool IsFood;

    public Effect[] Effects;

    public AudioClip pickSound;

    public AudioClip useSound;

    public GameFlag[] Usages;
}
