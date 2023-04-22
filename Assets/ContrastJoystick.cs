using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrastJoystick : MonoBehaviour
{

    [SerializeField] private FloatingJoystick _joystick;
    public Vector2 GetDirection()
    {
        return _joystick.Direction;
    }

    private void Update()
    {
        if (_joystick.Direction.magnitude < 0.3f)
        {
            GameEvents.OnStopAlpha.Invoke();
            return;
        }
        float contrastInput = _joystick.Vertical;
        if (contrastInput > 0)
        {
            GameEvents.OnAlphaInput.Invoke(true);
        }
        else
        {
            GameEvents.OnAlphaInput.Invoke(false);
        }
    }
}
