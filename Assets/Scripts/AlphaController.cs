using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public static float Alpha;
    private bool _changingAlpha;
    private GameFunctionsController _gameFunctionsController;
    private void Start()
    {
        _gameFunctionsController = FindObjectOfType<GameFunctionsController>();
        Alpha = 100;
        GameEvents.OnAlphaInput.AddListener(OnAlpha);
        GameEvents.OnStopAlpha.AddListener(OnAlphaStop);
    }

    void Update()
    {
        if(_gameFunctionsController == null)
        {
            return;
        }
        if (!_gameFunctionsController.CheatActive)
        {
            return;
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.KeypadMinus)))
        {
            OnAlpha(false);
        }
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.KeypadPlus)))
        {
            OnAlpha(true);
        }
        if (!_changingAlpha)
        {
            GameEvents.OnStopAlpha.Invoke();
        }
    }

    public void OnAlpha(bool up)
    {
        if (!_gameFunctionsController.CheatActive)
        {
            return;
        }
        if (up)
        {
            if (Alpha < 100)
            {
                _changingAlpha = true;
                Alpha += 25 * Time.deltaTime;
                GameEvents.OnAlphaChange.Invoke(true);
            }
        }
        else
        {
            if (Alpha > 0)
            {
                _changingAlpha = true;
                Alpha -= 25 * Time.deltaTime;
                GameEvents.OnAlphaChange.Invoke(false);
            }
        }
        Alpha = Mathf.Clamp(Alpha, 0, 100);
    }

    public void OnAlphaStop()
    {
        if (_changingAlpha)
        {
            _changingAlpha = false;
        }
    }
}
