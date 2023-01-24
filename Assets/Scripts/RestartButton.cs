using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void SetEnable(bool enable)
    {
        gameObject.SetActive(enable);
    }
    public void Restart()
    {
        GameEvents.OnRestart.Invoke();
    }
}
