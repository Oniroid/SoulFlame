using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalArrow : MonoBehaviour
{
    public Vector2 _target;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * 50 * Time.deltaTime, Space.Self);
        if (transform.position.x > _target.x)
        {
            Destroy(gameObject);

        }
    }
}
