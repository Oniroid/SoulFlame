using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    SpriteRenderer _renderer;
    ColliderReactive _c;
    AlphaReactive _ar;
    [SerializeField] private Color c;
    [SerializeField] private Color c2;
    void Start()
    {
        _ar = GetComponent<AlphaReactive>();
        _c = GetComponent<ColliderReactive>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void Flashazo()
    {
        _ar.enabled = false;
        _c.enabled = false;
        GetComponent<Collider2D>().enabled = true;
        _renderer.color = c2;
        StartCoroutine(CrReEnable());
    }
    IEnumerator CrReEnable()
    {
        yield return new WaitForSeconds(5);
        _ar.enabled = true;
        _c.enabled = true;
        _renderer.color = c;

    }


    void Update()
    {
        
    }
}
