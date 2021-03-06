using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay)
            return;
        if (enemy.Target != null) return;
        if (collision.CompareTag("Player"))
        {
            enemy.Target = collision.GetComponent<Player>();
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        enemy.Target = null;
    //    }
    //}
}
