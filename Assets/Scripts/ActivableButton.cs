using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableButton : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject _slime;
    bool _in;
    void Start()
    {
        _player = FindObjectOfType<CharacterMovement>().gameObject;
    }
    public void Enable()
    {
        FindObjectOfType<Flash>().ReFlash();
    }
    public void Disable()
    {

    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 0.1f)
        {
            if (_in == false)
            {
                Enable();
            }
            _in = true;
        }
        if (Vector3.Distance(transform.position, _player.transform.position) > 0.3f)
        {
            if (_in == true)
            {
                Disable();
            }
            _in = false;
        }
    }
}
