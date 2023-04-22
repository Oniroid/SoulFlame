using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    public Vector2 GetDirection()
    {
        return _joystick.Direction;
    }
}
