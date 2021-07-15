using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByFlag : MonoBehaviour
{
    [SerializeField]
    private string destroyFlag;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(destroyFlag))
            if (GameManager.Instance.gameVariables.HasFlag(destroyFlag))
                Destroy(gameObject);
    }

    [ContextMenu("GenerateFlag")]
    void GenerateFlag()
    {
        destroyFlag = System.Guid.NewGuid().ToString();
    }
}
