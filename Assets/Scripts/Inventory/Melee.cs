using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponObject
{
    [SerializeField]
    private AttackShape attackShape;

    private float timer = 0;

    public override void Setup(Weapon weapon)
    {
        base.Setup(weapon);
        attackShape.damage = weapon.Damage;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    public override void Attack()
    {
        if (timer <= 0)
        {
            AudioManager.Instance.PlaySound(data.Sound);
            _animator.SetTrigger("Attack");
            timer = data.ShootInterval;
        } 
    }

    public override int GetVal()
    {
        return -1;
    }

    public override void UpdateAim(Vector3 mousePos)
    {
        Vector2 place = (mousePos - transform.position).normalized;
        //¼ÆËã½Ç¶È
        float angle = Mathf.Atan2(place.y, place.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (angle > 90 || angle < -90)
            FlipSprite(true);
        else
            FlipSprite(false);
    }

    public void ShowAttackShape(int show)
    {
        if (show == 1)
            attackShape.gameObject.SetActive(true);
        else
            attackShape.gameObject.SetActive(false);
    }
}
