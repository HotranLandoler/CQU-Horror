using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float damage = 0;

    [SerializeField]
    private float speedMod = 0f;

    [SerializeField]
    private bool hasDuration = true;

    [SerializeField]
    private float duration = 5f;

    private float timer = 0f;

    private float destroyTimer = 0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (hasDuration)
            timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                anim.SetTrigger("FadeOut");
                destroyTimer = 1;
            }
        }
        if (destroyTimer > 0)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            player.SpeedModEffect = 1+speedMod;
            if (damage > 0)
                player.TakeDamage((int)damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO »®“À÷Æº∆
            collision.GetComponent<Player>().SpeedModEffect = 1f;
        }
    }
}
