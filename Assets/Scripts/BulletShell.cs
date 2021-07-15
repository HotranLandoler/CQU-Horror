using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    private Rigidbody2D rb;

    public float stopTime;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed()
    {
        rb.gravityScale = 1;
        Vector2 force = new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1));
        rb.AddForce(force);
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(stopTime);
        AudioManager.Instance.PlayBulletShellSound();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(1.5f);
        PoolManager.Instance.shellPool.Release(gameObject);
        //Destroy(gameObject, 1);
    }
}
