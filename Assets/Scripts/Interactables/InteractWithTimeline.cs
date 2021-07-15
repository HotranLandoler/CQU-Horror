using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InteractWithTimeline : InteractObjects
{
    [SerializeField]
    private PlayableDirector director;

    public override void Interact()
    {
        director.Play();
        Destroy(gameObject);
    }
}
