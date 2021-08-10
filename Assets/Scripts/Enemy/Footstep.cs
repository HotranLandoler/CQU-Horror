using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Footstep : MonoBehaviour
{
    public ObjectPool<GameObject> Pool { get; set; }

    [SerializeField]
    private float lifeTime = 1;

    private float timer;

    //private SpriteRenderer sr;

    //private float alpha;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        //sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Setup()
    {
        //sr.color = new Color(1, 1, 1);
        //alpha = 1;

        anim.SetTrigger("FadeOut");
        timer = lifeTime;
    }

    private void Update()
    {
        if (timer > 0)
        {
            //alpha -= Time.deltaTime;
            //sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Pool.Release(gameObject);
            }
        }       
    }
}
