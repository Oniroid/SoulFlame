using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    public static float Alpha;
    private bool _changingAlpha, _up;
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
        //if(_gameFunctionsController == null)
        //{
        //    return;
        //}
        //if (!_gameFunctionsController.CheatActive || !_changingAlpha)
        //{
        //    return;
        //}
        if(!_changingAlpha)
        {
            return;
        }
        float targetAlpha = Alpha;
        if (_up)
        {
            if (targetAlpha < 100)
            {
                targetAlpha += 25 * Time.deltaTime;
                GameEvents.OnAlphaChange.Invoke(true);
            }
        }
        else
        {
            if (targetAlpha > 0)
            {
                targetAlpha -= 25 * Time.deltaTime;
                GameEvents.OnAlphaChange.Invoke(false);
            }
        }
        Alpha = Mathf.Clamp(targetAlpha, 0, 100);
    }

    public void OnAlpha(bool up)
    {

        _changingAlpha = true;
        _up = up;
    }

    public void OnAlphaStop()
    {
        _changingAlpha = false;
    }
}
