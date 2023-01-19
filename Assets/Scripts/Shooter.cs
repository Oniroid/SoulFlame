using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameFunctionsController.Direction targetDirection;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _arrow;
    private Vector3 _direction;
    float _shootCd;
    void Start()
    {
        _shootCd = 0f;
    }

    void Update()
    {
        if (_shootCd > -1)
        {
            _shootCd -= Time.deltaTime;
        }
        RaycastHit2D hit;

        switch (targetDirection)
        {
            case GameFunctionsController.Direction.Up:
                _direction = Vector2.up;
                break;
            case GameFunctionsController.Direction.Down:
                _direction = Vector2.down;
                break;
            case GameFunctionsController.Direction.Left:
                _direction = Vector2.left;
                break;
            case GameFunctionsController.Direction.Right:
                _direction = Vector2.right;
                break;
        }
        hit = Physics2D.Linecast(transform.position, transform.position + _direction * 20, _mask);

        if (hit)
        {
            if (_shootCd <= 0)
            {        
                GameObject g = Instantiate(_arrow, transform.position, Quaternion.identity);
                g.GetComponent<Arrow>().Init(targetDirection, hit.transform.position);
                if (hit.collider.GetComponent<CharacterMovement>() != null)
                {
                    hit.collider.GetComponent<CharacterMovement>().Die();
                }
                //if (hit.collider.gameObject.GetComponent<StandardSlimeBehaviour>()!=null)
                //{
                //    if (_vertical)
                //    {
                //        Destroy(hit.collider.gameObject);
                //    }
                //    else
                //    {
                //        hit.collider.gameObject.GetComponent<StandardSlimeBehaviour>().Die();
                //    }
                //}
                _shootCd = 2f;
            }
        }
    }
}
