using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private int speed = 20;

    private int releaseTime = 3;
    private float timer = 3;

    public float damage = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //    SetSpeed();
    //}

    public void SetSpeed(float damage = 1f)
    {
        this.damage = damage;
        rb.velocity = transform.right * speed;
        timer = releaseTime;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && gameObject.activeSelf)
                PoolManager.Instance.bulletPool.Release(gameObject);
        }
     
    }
    //private void OnBecameInvisible()
    //{
    //    //Destroy(gameObject);
    //    PoolManager.Instance.bulletPool.Release(gameObject);
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    PoolManager.Instance.bulletPool.Release(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timer = 0;
        if (gameObject.activeSelf)
            PoolManager.Instance.bulletPool.Release(gameObject);
    }
}
