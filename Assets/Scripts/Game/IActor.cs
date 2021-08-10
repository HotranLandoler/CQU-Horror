using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IActor
{
    bool IsMoving { get; }

    Vector2 Dir { get; }

    event UnityAction<Vector2> DirChanged;
}
