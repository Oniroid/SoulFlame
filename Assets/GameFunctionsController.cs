using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctionsController : MonoBehaviour //Pensado para guardar elementos relacionados con la funcionalidad del juego
{
    private int _levelIndex;
    private bool _canStart, _cheatActive, _dead;

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
