using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private AudioSource ad;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected NavMeshAgent2D nav;
    protected Vector2 move;

    protected Collider2D collider2d;

    public Player target;

    [SerializeField]
    protected float hp = 50;
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (value <= 0)
                Die();
        }
    }

    [SerializeField]
    protected string flag;
    public bool IsDead = false;

   //所有敌人共享的数据，包括Damage()
    #region 
    public float attackRange = 2;

    [SerializeField]
    protected int moveSpeed = 5;

    protected float speedMod = 0.7f;

    protected float trueSpeed;

    [SerializeField]
    protected AudioClip attackSound;

    [SerializeField]
    protected AudioClip dieSound;

    [SerializeField]
    protected AudioClip stepSound;
    #endregion

    private void Awake()
    {
        ad = GetComponent<AudioSource>();
        collider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent2D>();
        if (GameManager.Instance.gameVariables.HasFlag(flag))
            Destroy(gameObject);
    }

    protected virtual void Start()
    {
        nav.speed = moveSpeed;

    }

    public abstract void Follow();

    public abstract void Attack();

    public void StopFollow(bool stop)
    {
        if (stop) nav.velocity = Vector2.zero;
        //move = Vector2.zero;
        nav.isStopped = stop;
        animator.SetBool("Move", false);
    }

    public abstract int Damage();

    public void SetDirection(Vector2 val)
    {
        move = Direction.NormalDir(val);
    }

    public virtual void Die()
    {
        if (dieSound)
        {
            Debug.Log("diesound");
            ad.PlayOneShot(dieSound);
            //AudioManager.Instance.PlaySound(dieSound);
        }
        GameManager.Instance.gameVariables.SetFlag(flag);
        StopFollow(true);
        IsDead = true;
        animator.SetTrigger("Die");
        collider2d.enabled = false;
        //transform.Translate(Vector3.right * 200); //#
        Destroy(gameObject, 2);
    }

    public void PlayAttackSound()
    {
        if (attackSound) ad.PlayOneShot(attackSound); //AudioManager.Instance.PlaySound(attackSound);
    }

    public void PlayFootStepSound()
    {
        if (stepSound) ad.PlayOneShot(stepSound);
    }
}
