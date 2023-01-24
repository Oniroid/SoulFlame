using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private GameObject _fadeOut, _press;
    private bool _canLoad;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        _canLoad = true;
    }

    public void OnAnyKey(InputAction.CallbackContext value)
    {
        LoadGame();
    }

    public void LoadGame()
    {
        if (_canLoad)
        {
            StartCoroutine(CrLoadLevel());
        }
    }

    IEnumerator CrLoadLevel()
    {
        _canLoad = false;
        _press.SetActive(false);
        _fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<GameFlowController>().FirstLevel();
    }
}
