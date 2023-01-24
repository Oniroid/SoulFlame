using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMobileMovementManager : MonoBehaviour
{
    private bool _pressing;
    GameFunctionsController.Direction _currentDirection;
    public void Move(int targetDirIndex)
    {
        _pressing = true;
        _currentDirection = (GameFunctionsController.Direction)targetDirIndex;
    }

    public void OnButtonExit()
    {
        _pressing = false;
        GameEvents.OnStopMovement.Invoke();
    }
    private void Update()
    {
        if (_pressing)
        {
            GameEvents.MobileMovement.Invoke(_currentDirection);
        }
    }
}
