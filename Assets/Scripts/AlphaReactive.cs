using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaReactive : MonoBehaviour
{
    SpriteRenderer _sr;
    [SerializeField] private int minAlpha=0;
    [SerializeField] private bool _inverse;
    void Start()
    {
        
        _sr = GetComponent<SpriteRenderer>();

    }

    
    void Update()
    {
        Color c = _sr.color;
        if(minAlpha == 0)
        {
            if (!_inverse)
            {
                c.a = (AlphaController._alpha- minAlpha) / 100f;
            }
            else
            {
                c.a = 1-((AlphaController._alpha - minAlpha) / 100f);
            }
        }
        else
        {
            if (!_inverse)
            {
                c.a = ((AlphaController._alpha- minAlpha)*(100/(100-minAlpha))) / 100f;
            }
            else
            {
                c.a = 1-(((AlphaController._alpha- minAlpha)*(100/(100-minAlpha))) / 100f);
            }
        }
        _sr.color = c;    
    }
}
