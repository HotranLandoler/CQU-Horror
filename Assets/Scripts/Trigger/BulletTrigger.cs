using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField]
    private GameFlag flag;

    [SerializeField]
    private UnityEvent Shot;

    private void Awake()
    {
        if (flag.Has())
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Shot?.Invoke();
            flag.Set();
            Destroy(gameObject);
        }
    }
}
