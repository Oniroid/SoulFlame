using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlphaManager : MonoBehaviour
{
    private bool _pressed, _up;
    public void OnAlphaButton(bool up)
    {
        _up = up;
        _pressed = true;
    }

    public void OnButtonUp()
    {
        _pressed = false;
    }

    private void Update()
    {
        if (_pressed)
        {
            GameEvents.OnAlpha.Invoke(_up);
        }
        else
        {
            GameEvents.OnStopAlpha.Invoke();
        }
    }
}
