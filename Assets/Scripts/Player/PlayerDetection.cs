using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ghost"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy) player.DetectEnemy(enemy);
            //if (!GameManager.Instance.EnemyDetected)
            //    Debug.Log($"Detected enemy {collision.name}");
            //GameManager.Instance.OnEnemyDetected(true);
            if (collision.CompareTag("Ghost"))
                GameManager.Instance.player.Light.SetActive(false);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ghost"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy) player.EnemyLeft(enemy);
            //if (player.NearbyEnemies.Contains(enemy))
            //{
            //    player.NearbyEnemies.Remove(enemy);
            //    Debug.Log($"enemy {collision.name} left");
            //}
            //Debug.Log($"enemy {collision.name} left");
            //GameManager.Instance.OnEnemyDetected(false);
            if (collision.CompareTag("Ghost"))
                GameManager.Instance.player.Light.SetActive(true);
        }
    }
}
