using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Controller : MonoBehaviour
{
    private int _step;
    private bool _cheated, _up, _down;
    private Coroutine _crRestore;
    [SerializeField] private CharacterMovement _characterMovement;
    IEnumerator Start()
    {
        GameEvents.OnPressUp.AddListener(Up);
        GameEvents.OnPressDown.AddListener(Down);
        yield return null;
    }

    public void Up()
    {
        _up = true;
        _down = false;

    }
    public void Down()
    {
        _up = false;
        _down = true;

    }
    void Update()
    {
        if (_cheated)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || _up)
        {
            _up = false;
            _down = false;
            if (_step ==0 || _step == 1 || _step == 4)
            {
                _step++;
                StopRestore();
                _crRestore = StartCoroutine(CrRestore());
            }
            else
            {
                _step = 0;
                StopRestore();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || _down)
        {
            _up = false;
            _down = false;
            if (_step == 2 || _step == 3 || _step == 5)
            {
                _step++;
                StopRestore();
                if (_step >= 5)
                {
                    FindObjectOfType<GameController>().ActiveCheat();
                    GameEvents.ThrowDialog.Invoke("Intro");
                    _cheated = true;
                }
                else
                {
                    _crRestore = StartCoroutine(CrRestore());
                }
            }
            else
            {
                _step = 0;
                StopRestore();
            }
        }
    }

    void StopRestore()
    {
        if (_crRestore != null)
        {
            StopCoroutine(_crRestore);
        }
    }

    IEnumerator CrRestore()
    {
        yield return new WaitForSeconds(1f);
        _step = 0;
    }
}
