using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器实体
/// </summary>
public abstract class WeaponObject : MonoBehaviour
{
    public Weapon data;

    [SerializeField]
    private GameObject weaponObj;
    
    protected SpriteRenderer _spriteRenderer;

    protected AudioSource _audioSource;

    protected Animator _animator;

    //private bool fliped;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = weaponObj.GetComponent<SpriteRenderer>();
        _animator = weaponObj.GetComponent<Animator>();
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

    public void FlipSprite(bool flip)
    {
        _animator.SetFloat("Flip", flip ? 1 : 0);
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
