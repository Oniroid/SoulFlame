using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalArrow : MonoBehaviour
{
    public Vector2 _target;
    public bool _killPlayer;
    void Start()
    {
        
    }

    void Update()
    {

        transform.Translate(Vector3.down*50*Time.deltaTime, Space.Self);
        if (transform.position.y < _target.y)
        {
            Destroy(gameObject);
        }
    }
}
