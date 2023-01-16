using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCallBack : MonoBehaviour
{
    [SerializeField] private GameFlowController _gameController;

    public void OnCameraCallback()
    {
        _gameController.StartGameCallBack();
    }
}
