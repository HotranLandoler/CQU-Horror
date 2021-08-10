using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDirAnim : MonoBehaviour
{
    private IActor actor;

    private void Start()
    {
        actor = GetComponent<IActor>();
        actor.DirChanged += OnDirChanged;
    }

    private void OnDirChanged(Vector2 val)
    {
        Vector3 scale;
        if (val.x < 0)
            scale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            scale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }
}
