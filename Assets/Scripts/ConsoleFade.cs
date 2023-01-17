using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleFade : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        GameEvents.ConsoleFadeOut.AddListener(FadeOut);
    }

    public void FadeOut()
    {
        _animator.Play("FadeOutGradient");
    }
}
