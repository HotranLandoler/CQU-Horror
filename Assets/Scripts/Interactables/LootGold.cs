using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGold : PickableItem
{
    [SerializeField]
    private FadeLight goldLight;

    [SerializeField]
    private float lightIntensity;

    public void Initialize(int goldNum)
    {
        //Debug.Log("Gold Init");
        numInModes = new int[3] { goldNum, goldNum, goldNum };
        flag.GenerateFlag();
        goldLight.Fade(lightIntensity);
    }
}
