using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearByFlag : MonoBehaviour
{
    [SerializeField]
    private string appearFlag;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(appearFlag))
            if (!GameManager.Instance.gameVariables.HasFlag(appearFlag))
                Destroy(gameObject);
    }

    [ContextMenu("GenerateFlag")]
    void GenerateFlag()
    {
        appearFlag = System.Guid.NewGuid().ToString();
    }
}
