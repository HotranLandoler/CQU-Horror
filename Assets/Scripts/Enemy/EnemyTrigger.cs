using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Enemy enemy;

    /// <summary>
    /// 受伤系数，是否为要害
    /// </summary>
    [SerializeField]
    private float damageMod = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy.IsDead) return;
        if (collision.CompareTag("Bullet"))
        {
            enemy.Hp -= collision.GetComponent<Bullet>().damage * damageMod;
            Instantiate(GameManager.Instance.bloodPrefab, transform.position, Quaternion.identity);
            enemy.target = GameManager.Instance.player;
            //if (enemy.Hp < 0)
            //    Destroy(enemy.gameObject);
        }
        else if (collision.CompareTag("PlayerAttack"))
        {
            enemy.Hp -= collision.GetComponent<AttackShape>().damage;
            Instantiate(GameManager.Instance.bloodPrefab, transform.position, Quaternion.identity);
            //if (enemy.Hp < 0)
            //    Destroy(enemy.gameObject);
        }
    }
}
