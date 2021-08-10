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
    private EnemyAttack attackShape;

    //�ܽ���Ч
    private Material material;
    private float currDissolve = 1f;
    //��Ч
    private AudioSource ad;

    protected Rigidbody2D rb;

    //�ƶ�����
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

    //�Զ�Ѱ·
    public NavMeshAgent2D Nav { get; private set; }

    public Animator animator { get; private set; }


    /// <summary>
    /// ��ǰ����ֵ
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

    public Vector2 Dir => move;

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
        foreach (var trigger in enemyTriggers)
        {
            trigger.DamageTaken += TakeDamage;
        }
        PrepareLightAttack();
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
        flag.flag.Set();
        StopFollow(true);
        IsDead = true;
        attackShape.gameObject.SetActive(false);
        //IMPORTANT �ֶ����������뿪��ֹͣ��San
        collider2d.enabled = false;
        foreach (var trigger in enemyTriggers)
        {
            trigger.gameObject.SetActive(false);
        }
        //���佱��
        if (data.Gold > 0) Loot();
        if (data.DieSound)
        {
            //Debug.Log("diesound");
            ad.PlayOneShot(data.DieSound);
        }
        animator.SetTrigger("Die");      
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

    //����ս��Ʒ
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
