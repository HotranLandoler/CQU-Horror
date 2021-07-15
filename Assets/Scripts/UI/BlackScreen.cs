using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        FadeIn();
    }

    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }
}
