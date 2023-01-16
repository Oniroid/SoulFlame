using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRenderCallback : MonoBehaviour
{
    [SerializeField] private GameFlowController _gameController;
    public void CreditsCallback()
    {
        _gameController.FirstCallBack();
    }
}
