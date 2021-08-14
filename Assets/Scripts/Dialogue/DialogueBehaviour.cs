using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

[System.Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    //public LocalizedString phrase;

    [TextArea(3,4)]
    public string phrase;
    //public Dialogue CurDialogue;

    public bool IsSystemTip = false;

    public bool hasToPause = true;

    private bool clipPlayed = false;
    //private bool pauseScheduled = false;
    
    private PlayableDirector _director;

    private UIManager ui => Locator.Get<UIManager>();

    public override void OnPlayableCreate(Playable playable)
    {
        _director = playable.GetGraph().GetResolver() as PlayableDirector;        
    }

    //public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    //{
    //    Debug.LogError("process " + phrase);
    //    if (!clipPlayed
    //        && info.weight > 0f)
    //    {
    //        if (IsSystemTip)
    //            ui.ShowTip(phrase);
    //        else
    //            ui.ShowDialogue(phrase);

    //        //if (Application.isPlaying)
    //        //{
    //        //    if (hasToPause)
    //        //    {
    //        //        pauseScheduled = true;
    //        //    }
    //        //}

    //        clipPlayed = true;
    //    }
    //}

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Debug.LogError("pause "+phrase);
        if (clipPlayed)
        {
            if (hasToPause) //pauseScheduled
            {
                //pauseScheduled = false;

                GameManager.Instance.PauseTimeline(_director);

                //Debug.Log("pause");
            }
            else
            {
                if (IsSystemTip)
                    ui.ToggleTip(false);
                else
                    ui.ToggleDialoguePanel(false);
            }
            clipPlayed = false;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.LogError("play " + phrase);
        if (!clipPlayed
            && info.weight > 0f)
        {
            if (IsSystemTip)
                ui.ShowTip(phrase);
            else
                ui.ShowDialogue(phrase);
            clipPlayed = true;
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        base.ProcessFrame(playable, info, playerData);
    }

}
