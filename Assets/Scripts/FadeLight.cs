using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FadeLight : MonoBehaviour
{
    private Light2D light2d;

    [SerializeField]
    private float fadeSpeed = 3;

    private float currIntensity;
    private float targetIntensity;
    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
        currIntensity = light2d.intensity;
        targetIntensity = currIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (currIntensity != targetIntensity)
        {
            currIntensity = Mathf.Lerp(currIntensity, targetIntensity, fadeSpeed * Time.deltaTime);
            light2d.intensity = currIntensity;
        }
    }

    /// <summary>
    /// 亮度渐变
    /// </summary>
    /// <param name="target">目标intensity</param>
    public void Fade(float target)
    {
        targetIntensity = target;
    }

    public void FadeOut() => Fade(0);
}
