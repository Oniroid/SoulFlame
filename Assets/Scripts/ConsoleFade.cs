using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConsoleFade : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        if(FindObjectsOfType<EventSystem>().Length > 1)
        {
            Destroy(transform.parent.GetChild(1).gameObject);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

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
