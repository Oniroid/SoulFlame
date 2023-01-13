using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    private GameObject _fadeOut;

    private void Start()
    {
        _fadeOut = transform.GetChild(1).gameObject;
    }

    public void Restart()
    {
        if (_fadeOut.activeSelf)
        {
            return;
        }
        _fadeOut.SetActive(true);
        SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
