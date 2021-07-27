using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByFlag : MonoBehaviour
{
    [SerializeField]
    private GameFlag destroyFlag;

    private void Awake()
    {
        if (destroyFlag.Has())
            Destroy(gameObject);
    }
    //[ContextMenu("GenerateFlag")]
    //void GenerateFlag()
    //{
    //    destroyFlag = System.Guid.NewGuid().ToString();
    //}
}
