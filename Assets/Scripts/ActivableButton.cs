using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableButton : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject _slime;
    bool _in, _disabled;
    void Start()
    {
        _player = FindObjectOfType<CharacterMovement>().gameObject;
    }
    public void Enable()
    {
        FindObjectOfType<Flash>().ReFlash();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < 0.1f)
        {
            if (_in == false)
            {
                Enable();
            }
            _in = true;
        }
        if (Vector2.Distance(transform.position, _player.transform.position) > 0.3f)
        {
            _in = false;
        }
    }
}
