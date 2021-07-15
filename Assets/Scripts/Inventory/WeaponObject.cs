using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器实体
/// </summary>
public abstract class WeaponObject : MonoBehaviour
{
    public Weapon data;

    protected SpriteRenderer _spriteRenderer;

    protected AudioSource _audioSource;

    protected Animator _animator;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public virtual void Setup(Weapon weapon)
    {
        data = weapon;
    }

    /// <summary>
    /// 切换武器显示
    /// </summary>
    /// <param name="show"></param>
    public void Show(bool show)
    {
        _spriteRenderer.enabled = show;
    }

    protected void FlipSprite(bool flip)
    {
        _spriteRenderer.flipY = flip;
    }

    /// <summary>
    /// 武器攻击
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// 武器瞄准鼠标方向
    /// </summary>
    /// <param name="mousePos"></param>
    public abstract void UpdateAim(Vector3 mousePos);

    /// <summary>
    /// 武器面板显示的数值
    /// </summary>
    /// <returns></returns>
    public abstract int GetVal();
}
