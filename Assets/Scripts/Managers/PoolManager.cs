using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject bulletShellPrefab;

    [SerializeField]
    private GameObject bloodPrefab;

    public ObjectPool<GameObject> bulletPool { get; private set; }

    public ObjectPool<GameObject> shellPool { get; private set; }

    public ObjectPool<BloodEffect> bloodPool { get; private set; }

    private void Awake()
    {
        _instance = this;
        bulletPool = new ObjectPool<GameObject>(() => Instantiate(bulletPrefab), 
            obj => { obj.SetActive(true); }, 
            obj => { obj.SetActive(false); },
            obj => Destroy(obj), collectionCheck: true, defaultCapacity: 10, maxSize: 50);
        shellPool = new ObjectPool<GameObject>(() => Instantiate(bulletShellPrefab),
            obj => { obj.SetActive(true); },
            obj => { obj.SetActive(false); },
            obj => Destroy(obj), collectionCheck: true, defaultCapacity: 10, maxSize: 50);
        bloodPool = new ObjectPool<BloodEffect>(() => Instantiate(bloodPrefab).GetComponent<BloodEffect>(),
            obj => { obj.gameObject.SetActive(true); },
            obj => { obj.gameObject.SetActive(false); },
            obj => Destroy(obj.gameObject), collectionCheck: true, defaultCapacity: 10, maxSize: 50);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        bulletPool?.Clear();
        shellPool?.Clear();
        bulletPool?.Clear();
    }
    //public GameObject GetBullet()
    //{
    //    return bulletPool.Get();
    //}

    //public void ReturnBullet(GameObject bullet)
    //{
    //    bulletPool.Release(bullet);
    //}

}
