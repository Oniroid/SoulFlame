using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private CharacterMovement _c;
    void Start()
    {
        _c = FindObjectOfType<CharacterMovement>();
    }

    void Update()
    {
        if(Vector3.Distance(_c.transform.position, transform.position) < 0.75f)
        {
            _c.DieElectrocuted();
        }
    }
}
