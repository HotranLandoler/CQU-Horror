using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float interval = 5;

    [SerializeField]
    private float maxCount = 20;

    public bool Spawning = false;

    private float timer;

    private int count = 0;

    private void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawning && count < maxCount)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = interval;
                    count++;
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity)
                        .GetComponent<Enemy>().Target = GameManager.Instance.player;
                }
            }
        }
    }
}
