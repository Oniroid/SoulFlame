using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public static float _alpha;
    private bool _changingAlpha;
    private GameController _gameController;
    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _alpha = 100;
        GameEvents.OnAlpha.AddListener(OnAlpha);
        GameEvents.OnStopAlpha.AddListener(OnAlphaStop);
    }

    void Update()
    {
        if(_gameController == null)
        {
            return;
        }
        if (!_gameController.HasCheat())
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
            _gameController.StopBright();
        }
    }

    public void OnAlpha(bool up)
    {
        if (!_gameController.HasCheat())
        {
            return;
        }
        if (up)
        {
            if (_alpha < 100)
            {
                _changingAlpha = true;
                _alpha += 25 * Time.deltaTime;
                _gameController.OnBright(true);
            }
        }
        else
        {
            if (_alpha > 0)
            {
                _changingAlpha = true;
                _alpha -= 25 * Time.deltaTime;
                _gameController.OnBright(false);
            }
        }
        _alpha = Mathf.Clamp(_alpha, 0, 100);
    }

    public void OnAlphaStop()
    {
        if (_changingAlpha)
        {
            _changingAlpha = false;
        }
    }
}
