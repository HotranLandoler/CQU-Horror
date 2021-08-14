using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private Player player;
    //private CapsuleCollider2D trigger;
    //ÎÞµÐ×´Ì¬
    //private bool isProtect = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        //trigger = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timer <= 0 && GameManager.Instance.CurGameMode == GameMode.Gameplay 
            && collision.CompareTag("EnemyAttack"))
        {
            //isProtect = true;
            timer = 1;
            player.TakeDamage(collision.GetComponent<EnemyAttack>().Damage);
            //GameManager.Instance.ChangeHp(-1 * collision.GetComponent<EnemyAttack>().damage);
            //StartCoroutine(DamageProtect());
        }
    }

    //private void OnCollisionStay2D(Collider2D collision)
    //{
    //    if (timer <= 0 && collision.CompareTag("EnemyAttack"))
    //    {
    //        isProtect = true;
    //        timer = 1;
    //        player.TakeDamage(collision.GetComponent<EnemyAttack>().damage);
    //        //GameManager.Instance.ChangeHp(-1 * collision.GetComponent<EnemyAttack>().damage);
    //        //StartCoroutine(DamageProtect());
    //    }
    //}

    public void TakeDamage(int damage)
    {
        player.TakeDamage(damage);
    }

    //private IEnumerator DamageProtect()
    //{    
    //    yield return new WaitForSeconds(0.4f);
    //    isProtect = false;
    //}
}
