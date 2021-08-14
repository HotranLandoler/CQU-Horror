using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʵ��
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
    /// �л�������ʾ
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
    /// ��������
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// ������׼��귽��
    /// </summary>
    /// <param name="mousePos"></param>
    public abstract void UpdateAim(Vector3 mousePos);

    /// <summary>
    /// ���������ʾ����ֵ
    /// </summary>
    /// <returns></returns>
    public abstract int GetVal();
}
