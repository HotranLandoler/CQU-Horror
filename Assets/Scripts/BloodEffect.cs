using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private ParticleSystem ps;

    private float timer = 0f;
    public void Play()
    {
        if (!ps)
            ps = GetComponent<ParticleSystem>();
        ps.Play();
        timer = ps.main.duration;    
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                PoolManager.Instance.bloodPool.Release(this);
        }
    }
}
