using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    //private Enemy enemy;
    public event UnityAction<float> DamageTaken;

    /// <summary>
    /// 受伤系数，是否为要害
    /// </summary>
    [SerializeField]
    private float damageMod = 1;

    /// <summary>
    /// 只能被近战攻击
    /// </summary>
    [SerializeField]
    private bool isMeleeOnly = false;

    // Start is called before the first frame update
    //void Start()
    //{
    //    enemy = GetComponentInParent<Enemy>();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (enemy.IsDead) return;
        if (!isMeleeOnly && collision.CompareTag("Bullet"))
        {
            var damage = collision.GetComponent<Bullet>().damage 
                * damageMod * GameManager.Instance.playerSkills.CriticDamageMod;
            DamageTaken?.Invoke(damage);
            //enemy.Hp -= collision.GetComponent<Bullet>().damage * damageMod;
            //Instantiate(GameManager.Instance.bloodPrefab, transform);
            BloodEffect bloodEffect = PoolManager.Instance.bloodPool.Get();
            bloodEffect.transform.position = transform.position;
            bloodEffect.Play();
            //Instantiate(GameManager.Instance.bloodPrefab, transform.position, Quaternion.identity);
            //enemy.Target = GameManager.Instance.player;
        }
        else if (isMeleeOnly && collision.CompareTag("PlayerAttack"))
        {
            var damage = collision.GetComponent<AttackShape>().damage * GameManager.Instance.playerSkills
                .MeleeDamageMod;
            DamageTaken?.Invoke(damage);
            //Instantiate(GameManager.Instance.bloodPrefab, transform);
            //Instantiate(GameManager.Instance.bloodPrefab, transform.position, Quaternion.identity);
            BloodEffect bloodEffect = PoolManager.Instance.bloodPool.Get();
            bloodEffect.transform.position = transform.position;
            bloodEffect.Play();
            GameManager.Instance.ChangeSanity(GameManager.Instance.playerSkills.MeleeAtkAddSanity);
            //if (enemy.Hp < 0)
            //    Destroy(enemy.gameObject);
        }
    }
}
