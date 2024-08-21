using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FadeLight : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D light2d;

    [SerializeField]
    private float fadeSpeed = 3;

    private float currIntensity;
    private float targetIntensity;
    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
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
    /// ���Ƚ���
    /// </summary>
    /// <param name="target">Ŀ��intensity</param>
    public void Fade(float target)
    {
        targetIntensity = target;
    }

    public void FadeOut() => Fade(0);
}
