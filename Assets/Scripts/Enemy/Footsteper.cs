using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Footsteper : MonoBehaviour
{
    private ObjectPool<GameObject>[] footstepPools;

    [SerializeField]
    private GameObject[] footstepPrefab;

    [SerializeField]
    private float interval = 0.5f;

    private IActor actor;

    private float timer;

    private int stepIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        actor = GetComponent<IActor>();
        footstepPools = new ObjectPool<GameObject>[2];
        footstepPools[0] = new ObjectPool<GameObject>(() => CreateStep(0),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); },
            obj => Destroy(obj), collectionCheck: true, defaultCapacity: 10, maxSize: 20);
        footstepPools[1] = new ObjectPool<GameObject>(() => CreateStep(1),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); },
            obj => Destroy(obj), collectionCheck: true, defaultCapacity: 10, maxSize: 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        if (actor.IsMoving && timer <= 0)
        {
            var obj = footstepPools[stepIndex].Get();
            SetStep(obj);
            if (++stepIndex >= footstepPrefab.Length) stepIndex = 0;
            timer = interval;
        }
    }

    private GameObject CreateStep(int idx)
    {
        var step = Instantiate(footstepPrefab[idx]);
        step.GetComponent<Footstep>().Pool = footstepPools[idx];
        //SetStep(step);      
        return step;
    }

    private void SetStep(GameObject step)
    {
        step.transform.position = transform.position;
        Vector3 scale;
        if (actor.Dir.x < 0)
            scale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            scale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        step.transform.localScale = scale;
        step.GetComponent<Footstep>().Setup();        
    }

    private void OnDestroy()
    {
        for (int i = 0; i < footstepPools.Length; i++)
        {
            footstepPools[i].Clear();
        }
    }
}
