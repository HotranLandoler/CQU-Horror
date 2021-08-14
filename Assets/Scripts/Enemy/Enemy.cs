using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Flag))]
public class Enemy : MonoBehaviour, IActor
{
    public EnemyData data;

    public Player Target { get; set; }

    private Flag flag;

    [SerializeField]
    private EnemyTrigger[] enemyTriggers;

    [SerializeField]
    private EnemyAttack[] attackShape;

    //溶解特效
    private Material material;
    private float currDissolve = 1f;
    //音效
    private AudioSource ad;

    protected Rigidbody2D rb;

    //移动方向
    [SerializeField]
    protected Vector2 move;
    public Vector2 Move
    {
        get => move;
        set
        {
            if (move != value)
                DirChanged?.Invoke(value);
            move = value;
        }
    }

    public bool IsMoving => Nav.velocity != Vector2.zero;

    public event UnityAction<Vector2> DirChanged;
    
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

    public float[] SpecialTimer { get; private set; } = new float[2];

    public UnityEvent OnDeath;

    public void ResetTimer(int i) => SpecialTimer[i] = data.SpecialCd[i];

    public Vector2 Dir => move;

    private void Awake()
    {
        flag = GetComponent<Flag>();
        if (flag.flag && flag.flag.Has())
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
        foreach (var trigger in enemyTriggers)
        {
            trigger.DamageTaken += TakeDamage;
        }
        PrepareLightAttack();
    }

    private void Update()
    {
        if (SpecialTimer[0] > 0)
            SpecialTimer[0] -= Time.deltaTime;
        if (SpecialTimer[1] > 0)
            SpecialTimer[1] -= Time.deltaTime;
    }

    private void TakeDamage(float damage)
    {
        Hp -= damage;
        if (!Target) Target = GameManager.Instance.player;
    }

    public virtual void Follow()
    {
        Nav.SetDestination(Target.transform.position);
        //move = (GameManager.Instance.player.transform.position - transform.position).normalized;
        if (Nav.velocity != Vector2.zero) Move = Nav.velocity.normalized;
        animator.SetBool("Move", true);
        //Vector3 scale;
        //if (move.x < 0)
        //    scale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        //else
        //    scale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        //transform.localScale = scale;
    }

    public virtual void Attack()
    {
        //animator.SetTrigger("Attack");
    }

    //public void MoveToPos(Vector3 pos, float speed)
    //{
    //    nav.speed = speed;
    //}

    public void StopFollow(bool stop)
    {
        if (stop)
        {
            Nav.velocity = Vector2.zero;
            Nav.ResetPath();
        }
        //move = Vector2.zero;
        
        Nav.isStopped = stop;
        //animator.SetBool("Move", false);
    }

    public virtual int Damage()
    {
        return data.LightDamage;
    }

    public void SetDirection(Vector2 val)
    {
        Move = Direction.NormalDir(val);
    }

    public virtual void Die()
    {
        if (IsDead) return;
        if (flag.flag) flag.flag.Set();
        StopFollow(true);
        IsDead = true;
        foreach (var shape in attackShape)
            shape.gameObject.SetActive(false);
        //IMPORTANT 手动触发敌人离开，停止减San
        collider2d.enabled = false;
        foreach (var trigger in enemyTriggers)
        {
            trigger.gameObject.SetActive(false);
        }
        //掉落奖励
        if (data.Gold > 0) Loot();
        if (data.DieSound)
        {
            //Debug.Log("diesound");
            ad.PlayOneShot(data.DieSound);
        }
        animator.SetTrigger("Die");      
        StartCoroutine(DoDissolveDeath());
        OnDeath?.Invoke();
        //Destroy(gameObject, 2);
    }

    public void ShowAttackBox()
    {
        attackShape[0].gameObject.SetActive(true);
    }

    public void ShowAttackBoxByDir(int dir)
    {
        if (dir == Direction.NormalDirInt(move))
            attackShape[dir].gameObject.SetActive(true);
    }

    public void HideAttackBox()
    {
        foreach (var item in attackShape)
        {
            item.gameObject.SetActive(false);
        }
        //attackShapes[dir].gameObject.SetActive(false);
    }

    public void PlayAttackSound()
    {
        if (data.AttackSound) ad.PlayOneShot(data.AttackSound); //AudioManager.Instance.PlaySound(attackSound);
    }

    public void PlayFootStepSound()
    {
        if (data.StepSound) ad.PlayOneShot(data.StepSound);
    }

    public void PrepareLightAttack()
    {
        foreach (var shape in attackShape)
        {
            shape.Damage = data.LightDamage;
        }
    }

    public void PrepareHeavyAttack()
    {
        foreach (var shape in attackShape)
        {
            shape.Damage = data.HeavyDamage;
        }
    }

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
