using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level0Controller : MonoBehaviour
{
    private int _step;
    private bool _cheated, _up, _down;
    private Coroutine _crRestore;
    private GameFunctionsController.Direction _lastDirection;
    [SerializeField] private CharacterMovement _characterMovement;
    IEnumerator Start()
    {
        GameEvents.MobileMovement.AddListener(Up);
        GameEvents.OnStopMovement.AddListener(OnStopMovement);
        yield return null;
    }

    public void Up(GameFunctionsController.Direction targetDirection)
    {
        _lastDirection = targetDirection;
    }

    public void OnStopMovement()
    {
        if(_lastDirection != GameFunctionsController.Direction.Up && _lastDirection != GameFunctionsController.Direction.Down)
        {
            _step = 0;
        }
        if (_lastDirection == GameFunctionsController.Direction.Up)
        {
            _up = true;
            _down = false;
        }
        if (_lastDirection == GameFunctionsController.Direction.Down)
        {
            _up = false;
            _down = true;
        }
    }

    void Update()
    {
        if (_cheated)
        {
            return;
        }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || _up)
        {
            _up = false;
            if (_step == 0 || _step == 1 || _step == 4)
            {
                _step++;
                StopRestore();
                _crRestore = StartCoroutine(CrRestore());
            }
            else
            {
                _step = 0;
                StopRestore();
            }
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame || _down)
        {
            _down = false;
            if (_step == 2 || _step == 3 || _step == 5)
            {
                _step++;
                StopRestore();
                if (_step >= 5)
                {
                    FindObjectOfType<GameFunctionsController>().CheatActive = true;
                    GameEvents.ThrowDialog.Invoke("Intro");
                    _cheated = true;
                }
                else
                {
                    _crRestore = StartCoroutine(CrRestore());
                }
            }
            else
            {
                _step = 0;
                StopRestore();
            }
        }
    }

    void StopRestore()
    {
        if (_crRestore != null)
        {
            StopCoroutine(_crRestore);
        }
    }

    IEnumerator CrRestore()
    {
        yield return new WaitForSeconds(1f);
        _step = 0;
    }
}
