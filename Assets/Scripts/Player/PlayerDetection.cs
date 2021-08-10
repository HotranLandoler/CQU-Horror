using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ghost"))
        {
            GameManager.Instance.OnEnemyDetected(true);
            if (collision.CompareTag("Ghost"))
                GameManager.Instance.player.Light.SetActive(false);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ghost"))
        {
            GameManager.Instance.OnEnemyDetected(false);
            if (collision.CompareTag("Ghost"))
                GameManager.Instance.player.Light.SetActive(true);
        }
    }
}
