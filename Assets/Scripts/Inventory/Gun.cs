using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class Gun : WeaponObject
{
    //[SerializeField]
    //private AudioClip gunSound;

    [SerializeField]
    private Transform gunHole;

    [SerializeField]
    private SpriteRenderer gunFlare;

    //[SerializeField]
    //private GameObject shellPrefab;

    [SerializeField]
    private Light2D gunLight;

    /// <summary>
    /// 弹仓内子弹数
    /// </summary>
    //public int AmmoOnLoad = 10;

    private float timer = 0;

    // Start is called before the first frame update
    //void Start()
    //{
    //    Setup(AmmoOnLoad);
    //}

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    //public override void Setup(Weapon weapon)
    //{
    //    base.Setup(weapon);
    //}


    /// <summary>
    /// 更新瞄准方向
    /// </summary>
    public override void UpdateAim(Vector3 mousePos)
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 place = (mousePos - transform.position).normalized;
        //计算角度
        float angle = Mathf.Atan2(place.y, place.x) * Mathf.Rad2Deg;
        //angle = Vector2.SignedAngle(Vector2.right, place);
        transform.eulerAngles = new Vector3(0, 0, angle);
        //player.Equip.transform.right = place.normalized;
        if (angle > 90 || angle < -90)
            FlipSprite(true);
        else
            FlipSprite(false);
    }

    public override void Attack()
    {
        if (timer <= 0)
        {
            if (GameManager.Instance.inventory.GetGunAmmoLoaded(data) > 0)
            {
                //开火
                _animator.SetTrigger("Attack");
                PlayGunSound();
                if (data.BulletsPerShot > 0)
                {
                    float angleunit = 30 / data.BulletsPerShot;
                    float angle = (data.BulletsPerShot / 2)*angleunit;
                    for (int i=0; i < data.BulletsPerShot; i++)
                    {
                        FireBullet(new Vector3(0, 0, angle));
                        angle -= angleunit;
                    }
                }
                else
                {
                    FireBullet(Vector3.zero);
                }
                //Debug.Log("fire");
                //Instantiate(GameManager.Instance.bulletPrefab, gunHole.transform.position, Quaternion.Euler(transform.eulerAngles));
                
                StartCoroutine(nameof(ShowGunLight));
                //Instantiate(shellPrefab, transform.position, Quaternion.identity);
                var shell = PoolManager.Instance.shellPool.Get();
                shell.transform.position = transform.position;
                shell.transform.rotation = Quaternion.identity;
                shell.GetComponent<BulletShell>().SetSpeed();
                GameManager.Instance.inventory.SetGunAmmoLoaded(data, -1);
                //GameManager.Instance.OnWeaponFired(AmmoOnLoad);
            }
            else
            {
                //没子弹
                AudioManager.Instance.PlayNoAmmoSound();
                
            }
            timer = data.ShootInterval;
        }    
    }

    private void FireBullet(Vector3 angleMod)
    {
        var bullet = PoolManager.Instance.bulletPool.Get();
        bullet.transform.position = gunHole.transform.position;
        bullet.transform.rotation = Quaternion.Euler(transform.eulerAngles);
        bullet.transform.eulerAngles += angleMod;
        bullet.GetComponent<Bullet>().SetSpeed();
    }

    private IEnumerator ShowGunLight()
    {
        gunLight.enabled = true;
        gunFlare.enabled = true;
        yield return new WaitForSeconds(data.ShootInterval*0.3f);
        gunLight.enabled = false;
        gunFlare.enabled = false;
    }

    private void PlayGunSound()
    {
        if (data.Sound)
            _audioSource.PlayOneShot(data.Sound);
    }

    public override int GetVal()
    {
        return GameManager.Instance.inventory.GetGunAmmoLoaded(data);
    }
}
