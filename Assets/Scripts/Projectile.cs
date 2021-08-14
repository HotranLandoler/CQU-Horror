using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rd;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject spawnedPrefab;

    private bool active = false;

    private Vector3 targetPos;

    // Start is called before the first frame update
    void Awake()
    {
       
    }

    public void Shoot(Vector3 pos)
    {
        rd = GetComponent<Rigidbody2D>();
        targetPos = pos;
        active = true;
        //rd.velocity = targetPos.normalized * speed;
    }

    private void Update()
    {
        if (active)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (transform.position == targetPos)
                OnHitTarget();
        }
    }

    private void OnHitTarget()
    {
        active = false;
        ///rd.velocity = Vector2.zero;
        if (spawnedPrefab)
            Instantiate(spawnedPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitTarget();
    }
}
