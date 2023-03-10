using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctionsController : MonoBehaviour //Pensado para guardar elementos relacionados con la funcionalidad del juego
{
    public bool mobileMode;
    public enum Direction { Up, Down, Left, Right };
    private int _levelIndex;
    private bool _canStart, _cheatActive, _dead, _usingMobile;
    public static GameFunctionsController GameFunctionsControllerInstance;

    private void Awake()
    {
        if(Application.platform == RuntimePlatform.Android || mobileMode)
        {
            _usingMobile = true;
        }
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
        if (sceneName[0] == 'L') //Si cargamos desde el editor un nivel mayor que el primero nos deja activado el truco del contraste
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
    public bool UsingMobile
    {
        get { return _usingMobile; }
    }
}
