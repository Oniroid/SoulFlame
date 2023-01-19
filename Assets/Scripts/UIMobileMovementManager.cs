using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMobileMovementManager : MonoBehaviour
{
    private bool _pressing;
    GameFunctionsController.Direction _currentDirection;
    public void MoveUp()
    {
        _pressing = true;
        _currentDirection = GameFunctionsController.Direction.Up;
    }
    public void MoveDown()
    {
        _pressing = true;
        _currentDirection = GameFunctionsController.Direction.Down;
    }
    public void MoveLeft()
    {
        _pressing = true;
        _currentDirection = GameFunctionsController.Direction.Left;
    }
    public void MoveRight()
    {
        _pressing = true;
        _currentDirection = GameFunctionsController.Direction.Right;
    }

    public void OnButtonExit()
    {
        _pressing = false;
    }
    private void Update()
    {
        if (_pressing)
        {
            GameEvents.MobileMovement.Invoke(_currentDirection);
        }
        else 
        {
            GameEvents.OnStopMovement.Invoke();
        }
    }

    public void OnUp()
    {
        GameEvents.OnPressUp.Invoke();
    }
    public void OnDown()
    {
        GameEvents.OnPressDown.Invoke();
    }
}
