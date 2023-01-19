using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReactive : MonoBehaviour
{
    [SerializeField] private int _colliderThreshold;
    [SerializeField] private bool _disableCollider;
    Collider2D _collider;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if(AlphaController.Alpha<_colliderThreshold)
        {
            if (_disableCollider)
            {
                _collider.enabled = true;
            }
            else
            {
                _collider.enabled = false;
            }
        }
        else
        {
            if (_disableCollider)
            {
                _collider.enabled = false;
            }
            else
            {
                _collider.enabled = true;
            }
            
        }
    }
}
