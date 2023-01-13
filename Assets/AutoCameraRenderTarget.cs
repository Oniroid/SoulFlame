using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoCameraRenderTarget : MonoBehaviour
{
    [SerializeField] private RenderTexture _renderTexture;
    void Start()
    {
        if(SceneManager.sceneCount > 1)
        {
            GetComponent<Camera>().targetTexture = _renderTexture;
        }
    }
}
