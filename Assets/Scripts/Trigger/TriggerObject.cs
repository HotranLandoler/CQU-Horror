using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerObject : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent onTrigger;

    [SerializeField]
    protected GameFlag triggeredFlag;

    private void Awake()
    {
        if (triggeredFlag.Has())
        {
            //Debug.Log("destroy");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //IMPORTANT 必须首先设置flag，否则flag无法被保存
            GameManager.Instance.gameVariables.SetFlag(triggeredFlag);
            onTrigger?.Invoke();
            Destroy(gameObject);
        }
    }
}
