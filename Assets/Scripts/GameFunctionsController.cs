using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctionsController : MonoBehaviour //Pensado para guardar elementos relacionados con la funcionalidad del juego
{
    private int _levelIndex;
    private bool _canStart, _cheatActive, _dead;
    public static GameFunctionsController GameFunctionsControllerInstance;

    private void Awake()
    {
        if(GameFunctionsControllerInstance == null)
        {
            GameFunctionsControllerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(GameFunctionsControllerInstance != this)
        {
            Destroy(gameObject);
        }
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName[0] == 'L')
        {
            _cheatActive = true;
            _levelIndex = int.Parse(sceneName[sceneName.Length-1].ToString());
        }
    }

    public int LevelIndex
    {
        get { return _levelIndex; }
        set { _levelIndex = value; }
    }
    public bool CanStart
    {
        get { return _canStart; }
        set { _canStart = value; }
    }
    public bool CheatActive
    {
        get { return _cheatActive; }
        set { _cheatActive = value; }
    }
    public bool Dead
    {
        get { return _dead; }
        set { _dead = value; }
    }
}
