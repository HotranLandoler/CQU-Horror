using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    [SerializeField]
    private AttackShape attackShape;

    public void ShowAttackShape(int show)
    {
        if (show == 1)
            attackShape.gameObject.SetActive(true);
        else
            attackShape.gameObject.SetActive(false);
    }
}
