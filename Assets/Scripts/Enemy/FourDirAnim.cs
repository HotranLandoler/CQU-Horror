using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirAnim : MonoBehaviour
{
    private IActor actor;

    private void Start()
    {
        actor = GetComponent<IActor>();
        OnDirChanged(actor.Dir);
        actor.DirChanged += OnDirChanged;
    }

    private void OnDirChanged(Vector2 val)
    {
        actor.animator.SetFloat("MoveX", val.x);
        actor.animator.SetFloat("MoveY", val.y);
    }
}
