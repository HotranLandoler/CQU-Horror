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
            DamageTaken?.Invoke(collision.GetComponent<Bullet>().damage * damageMod);
            //enemy.Hp -= collision.GetComponent<Bullet>().damage * damageMod;
            Instantiate(GameManager.Instance.bloodPrefab, transform.position, Quaternion.identity);
            //enemy.Target = GameManager.Instance.player;
        }
        else if (isMeleeOnly && collision.CompareTag("PlayerAttack"))
        {
            DamageTaken?.Invoke(collision.GetComponent<AttackShape>().damage);
            Instantiate(GameManager.Instance.bloodPrefab, transform.position, Quaternion.identity);
            //if (enemy.Hp < 0)
            //    Destroy(enemy.gameObject);
        }
    }
}
