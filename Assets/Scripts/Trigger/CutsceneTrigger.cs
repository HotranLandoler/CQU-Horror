using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    private PlayableDirector director;

    //进入碰撞器触发，否则为自动触发
    [SerializeField]
    private bool triggerOnEnter = true;

    [SerializeField]
    private string playedFlag;

    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        Debug.Log($"GetFlag{playedFlag}:{GameManager.Instance.gameVariables.HasFlag(playedFlag)}");
        if (!triggerOnEnter && !GameManager.Instance.gameVariables.HasFlag(playedFlag))
        {
            director.Play();
            GameManager.Instance.gameVariables.SetFlag(playedFlag);
            Debug.Log($"SetFlag{playedFlag}:true");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnEnter && !GameManager.Instance.gameVariables.HasFlag(playedFlag) &&
            collision.CompareTag("Player"))
        {
            director.Play();
            GameManager.Instance.gameVariables.SetFlag(playedFlag);
        }
    }
}
