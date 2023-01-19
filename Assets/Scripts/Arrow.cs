using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector2 _target, _direction;

    public void Init(GameFunctionsController.Direction targetDirection, Vector2 targetPosition)
    {
        switch (targetDirection)
        {
            case GameFunctionsController.Direction.Up:
                transform.Rotate(0,0,90);
                _direction = Vector2.up;
                break;
            case GameFunctionsController.Direction.Down:
                transform.Rotate(0, 0, -90);
                _direction = Vector2.down;
                break;
            case GameFunctionsController.Direction.Left:
                transform.Rotate(0, 0, 180);
                _direction = Vector2.left;
                break;
            case GameFunctionsController.Direction.Right:
                _direction = Vector2.right;
                break;
        }
        _target = targetPosition;
    }

    void Update()
    {
        transform.Translate(_direction * 50 * Time.deltaTime, Space.World);
        if (Vector2.Distance(transform.position, _target) < 1)
        {
            Destroy(gameObject);
        }
    }
}
