using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField]
    private GameObject[] bulletPrefab;

    public float shootInterval = 1f;

    public int shootCount = 3;

    [SerializeField]
    private float accuracyRange = 1.5f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Shoot()
    {
        if (enemy.Target == null) return;
        Vector2 target = (Vector2)enemy.Target.transform.position + accuracyRange * Random.insideUnitCircle;
        GameObject prefab;
        if (bulletPrefab.Length == 1)
            prefab = bulletPrefab[0];
        else
        {
            var chance = Random.Range(0, bulletPrefab.Length);
            prefab = bulletPrefab[chance];
        }
        enemy.PlaySpecialSound(0);
        Instantiate(prefab, transform.position, Quaternion.identity)
            .GetComponent<Projectile>().Shoot(target);
    }
}
