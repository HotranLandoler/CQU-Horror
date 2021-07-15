using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public abstract class TriggerObject : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent onTrigger;

    [SerializeField]
    protected string triggeredFlag;

    private void Awake()
    {
        if (GameManager.Instance.gameVariables.HasFlag(triggeredFlag))
        {
            //Debug.Log("destroy");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //必须首先设置flag，否则flag无法被保存
            GameManager.Instance.gameVariables.SetFlag(triggeredFlag);
            onTrigger?.Invoke();
            Destroy(gameObject);
        }
    }
}
