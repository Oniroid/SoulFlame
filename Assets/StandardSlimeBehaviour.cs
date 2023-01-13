using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardSlimeBehaviour : MonoBehaviour
{
    private GameObject _player;
    bool _dead;
    [SerializeField] private int _minAlpha;
    bool _enabled;
    Coroutine _cr;
    void Start()
    {
        _player = FindObjectOfType<CharacterMovement>().gameObject;
        CheckMove();
    }
    public void Die()
    {
        _dead = true;
    }
    public void CheckMove()
    {
        if (_dead) return;
        if (AlphaController._alpha < _minAlpha) return;
        float horizontalDistance = transform.position.x - _player.transform.position.x;
        float verticalDistance = transform.position.y - _player.transform.position.y;
        
        if(_player.transform.position.y >= -3.5f)
        {
            Move(Vector3.left);
        }
        else
        {
            Move(Vector3.down);
        }
        //if (Mathf.Abs(horizontalDistance)+8 > Mathf.Abs(verticalDistance))
        //{
        //    if (horizontalDistance < 0)
        //    {
        //        Move(Vector3.right);
        //    }
        //    else
        //    {
        //        Move(Vector3.left);
        //    }
        //}
        //else
        //{
        //    if (verticalDistance < 0)
        //    {
        //        Move(Vector3.up);
        //    }
        //    else
        //    {
        //        Move(Vector3.down);
        //    }
        //}
    }
    public void Disable()
    {
        StopCoroutine(_cr);
    }
    public void Enable()
    {
        CheckMove();
    }
    void Update()
    {
        if (AlphaController._alpha < _minAlpha)
        {
            if (_enabled)
            {
                _enabled = false;
                Disable();
            }
        }
        else
        {
            if (!_enabled)
            {
                _enabled = true;
                Enable();
            }
        }
    }
    public void Move(Vector3 direction)
    {
        _cr = StartCoroutine(CrMove(direction));
    }
    IEnumerator CrMove(Vector3 direction)
    {
        //float _elapsedTime = 0f;
        //while(_elapsedTime < 0.5f)
        //{
        //    yield return null;
        //    if(AlphaController._alpha > _minAlpha)
        //    {
        //        _elapsedTime += Time.deltaTime;
        //    }
        //}
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + direction;
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, i/0.5f);
            yield return null;
        }
        CheckMove();
    }
}
