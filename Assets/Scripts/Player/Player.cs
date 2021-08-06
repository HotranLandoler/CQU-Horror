using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public PlayerState state = PlayerState.Walk;

    /// <summary>
    /// 移动向量
    /// </summary>
    public Vector2 move;

    /// <summary>
    /// 面向
    /// </summary>
    public Vector2 direction = Vector2.zero;

    /// <summary>
    /// 正在瞄准, Set by controller
    /// </summary>
    public bool IsAiming = false;

    /// <summary>
    /// 正在跑步
    /// </summary>
    public bool IsRunning = false;


    private bool _isReloading = false;
    public bool IsReloading
    {
        get
        {
            return _isReloading;
        }
        set
        {
            if (_isReloading == false && value == true)
            {
                StartReloading?.Invoke();
                reloadTimer = ReloadTime;
            }
            if (_isReloading == true && value == false)
            {
                StopReloading?.Invoke();
                reloadTimer = 0;
            }
            _isReloading = value;   
            //Reload?.Invoke();
        }
    }

    public bool IsDead = false;

    private readonly float reloadTime = 1;

    public float ReloadTime => reloadTime * GameManager.Instance.playerSkills.ReloadTimeMod;

    public float reloadTimer = 0;

    public event UnityAction StartReloading;

    public event UnityAction StopReloading;

    /// <summary>
    /// 更新面向，Invoked by controller
    /// </summary>
    [System.NonSerialized]
    public UnityAction DirChanged;

    /// <summary>
    /// 更新Move
    /// </summary>
    [System.NonSerialized]
    public UnityAction MoveChanged;


    //[System.NonSerialized]
    //public UnityAction WeaponFired;

    /// <summary>
    /// 更新面向，Invoked by controller
    /// </summary>
    [System.NonSerialized]
    public UnityAction RunChanged;

    [System.NonSerialized]
    public UnityAction DamageTaken;

    [System.NonSerialized]
    public UnityAction StopAction;

    public UnityAction StopAim;

    [System.NonSerialized]
    public UnityAction OnDeath;

    public WeaponObject Equip;

    public List<IInteractable> interactObjs = new List<IInteractable>(3);

    //[SerializeField]
    //private CapsuleCollider2D damageTrigger;

    // Start is called before the first frame update
    void Start()
    {
        DirChanged?.Invoke();
        //damageTrigger = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsReloading)
        {
            if (reloadTimer > 0)
                reloadTimer -= Time.deltaTime;
            else
            {
                StopReload(true);
            }       
        }      
    }

    public void Reload()
    {
        if (IsReloading == true)
            return;
        if (Equip == null)
            return;
        if (Equip.data.weaponType != WeaponType.Gun)
            return;
        var gun = Equip as Gun;
        var needAmmo = gun.data.MaxAmmo - GameManager.Instance.inventory.GetGunAmmoLoaded(gun.data);
        if (needAmmo > 0)
        {
            //至少背包得有子弹
            if (GameManager.Instance.inventory.GetItemNum(gun.data.BulletId) > 0)
                IsReloading = true;
        }
    }

    public void StopReload(bool success = false)
    {
        if (success)
        {
            //装弹成功
            if (Equip == null)
                return;
            if (Equip.data.weaponType != WeaponType.Gun)
                return;
            var gun = Equip as Gun;
            var need = gun.data.MaxAmmo - GameManager.Instance.inventory.GetGunAmmoLoaded(gun.data); //10
            var bullet = GameManager.Instance.GetItemData(gun.data.BulletId);
            var bullets = GameManager.Instance.inventory.HasItem(bullet); //1
            if (bullets >= need)
            {
                GameManager.Instance.inventory.RemoveItem(bullet, need);
                GameManager.Instance.inventory.SetGunAmmoLoaded(gun.data, need);
            }
            else
            {
                //现有子弹全装填
                GameManager.Instance.inventory.RemoveItemAll(bullet);
                GameManager.Instance.inventory.SetGunAmmoLoaded(gun.data, bullets);
            }
            //UIManager.Instance.UpdateAmmo(gun);
        }
        IsReloading = false;
    }

    public void StopMove()
    {
        //direction = move;
        SetMove(Vector2.zero);
    }

    public void SetDirection(Vector2 val)
    {
        direction = Direction.NormalDir(val);
        DirChanged?.Invoke();
    }

    /// <summary>
    /// 0123下右左上
    /// </summary>
    /// <param name="val"></param>
    public void SetDirection(int val)
    {
        direction = Direction.Dir2Vector(val);
        DirChanged?.Invoke();
    }

    public void SetMove(Vector2 val) //#
    {
        move = val;
        MoveChanged?.Invoke();
    }

    public void SetRun(bool val) //#
    {
        IsRunning = val;
        if (val)
            StopReload(false);
        RunChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        GameManager.Instance.ChangeHp(-1 * damage);
        DamageTaken?.Invoke();
    }

    public void Die()
    {
        StopAction();
        IsDead = true;
        //GameManager.Instance.SwitchGameMode(GameMode.Timeline);
        OnDeath?.Invoke();
    }

}

public enum PlayerState
{
    Walk,
    Run,
    Aim,
}
