using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flag))]
public class Enemy : MonoBehaviour
{
    public EnemyData data;

    public Player Target { get; set; }

    private Flag flag;

    [SerializeField]
    private EnemyTrigger[] enemyTriggers;

    [SerializeField]
    private EnemyAttack attackShape;

    //溶解特效
    private Material material;
    private float currDissolve = 1f;
    //音效
    private AudioSource ad;

    protected Rigidbody2D rb;

    //移动方向
    protected Vector2 move;

    protected Collider2D collider2d;

    //自动寻路
    public NavMeshAgent2D Nav { get; private set; }

    public Animator animator { get; private set; }

    /// <summary>
    /// 当前生命值
    /// </summary>
    private float hp;
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

    public bool IsDead { get; private set; }

    private void Awake()
    {
        flag = GetComponent<Flag>();
        if (flag.flag.Has())
        {
            Destroy(gameObject);
            return;
        }

        material = GetComponent<SpriteRenderer>().material;
        ad = GetComponent<AudioSource>();
        collider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Nav = GetComponent<NavMeshAgent2D>();
    }

    protected virtual void Start()
    {
        Hp = data.MaxHp;
        Nav.speed = data.SlowSpeed;

    }

    public virtual void Follow()
    {
        Nav.SetDestination(Target.transform.position);
        //move = (GameManager.Instance.player.transform.position - transform.position).normalized;
        move = Nav.velocity;
        animator.SetBool("Move", true);
        Vector3 scale;
        if (move.x < 0)
            scale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            scale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }

    //public void MoveToPos(Vector3 pos, float speed)
    //{
    //    nav.speed = speed;
    //}

    public void StopFollow(bool stop)
    {
        if (stop) Nav.velocity = Vector2.zero;
        //move = Vector2.zero;
        Nav.isStopped = stop;
        animator.SetBool("Move", false);
    }

    public virtual int Damage()
    {
        return data.LightDamage;
    }

    public void SetDirection(Vector2 val)
    {
        move = Direction.NormalDir(val);
    }

    public virtual void Die()
    {
        flag.flag.Set();
        //掉落奖励
        if (data.Gold > 0) Loot();
        if (data.DieSound)
        {
            //Debug.Log("diesound");
            ad.PlayOneShot(data.DieSound);
        }
        StopFollow(true);
        IsDead = true;
        animator.SetTrigger("Die");
        //IMPORTANT 手动触发敌人离开，停止减San
        collider2d.enabled = false;
        StartCoroutine(DoDissolveDeath());      
        //Destroy(gameObject, 2);
    }

    public void ShowAttackBox(int show)
    {
        if (show == 1)
            attackShape.gameObject.SetActive(true);
        else
            attackShape.gameObject.SetActive(false);
    }

    public void PlayAttackSound()
    {
        if (data.AttackSound) ad.PlayOneShot(data.AttackSound); //AudioManager.Instance.PlaySound(attackSound);
    }

    public void PlayFootStepSound()
    {
        if (data.StepSound) ad.PlayOneShot(data.StepSound);
    }

    public void PrepareLightAttack() =>
        attackShape.Damage = data.LightDamage;

    public void PrepareHeavyAttack() =>
        attackShape.Damage = data.HeavyDamage;

    //生成战利品
    private void Loot()
    {
        var gold = Instantiate(GameManager.Instance.goldPrefab, transform.position, Quaternion.identity)
            .GetComponent<LootGold>();
        gold.Initialize(data.Gold);
    }

    private IEnumerator DoDissolveDeath()
    {
        yield return new WaitForSeconds(1);
        while (currDissolve > 0)
        {
            currDissolve -= Time.deltaTime;
            material.SetFloat("_Fade", currDissolve);
            yield return null;
        }
        Destroy(gameObject);
    }
}
