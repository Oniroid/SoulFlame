using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveDur, _minMovement;
    [SerializeField] private AnimationCurve _easyInAnim, _keepAnim;
    [SerializeField] private GameObject _fadeOut;
    private Animator _animator;
    public enum Direction {up, down, left, right};
    private Direction _targetDir, _lastDir;
    private float xAxis, yAxis;
    private bool _moving, _keeping, _movementDisabled, _usingMobile, _pressingButton;
    [SerializeField] private LayerMask _mask;
    SpriteRenderer _sr;
    private GameFunctionsController _gameFunctionsController;

    private void Start()
    {
        _gameFunctionsController = FindObjectOfType<GameFunctionsController>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        GameEvents.MobileMovement.AddListener(OnMobileInput);
        GameEvents.OnStopMovement.AddListener(OnMobileStop);
        GameEvents.OnRestart.AddListener(RestartButton);
    }

    //CAMBIAR ESTO A UNA SOLA FUNCION
    public void DieElectrocuted()
    {
        if(_sr.color.a > 0.1f)
        {
            if (!_gameFunctionsController.Dead)
            {
                _gameFunctionsController.Dead = true;
                _animator.SetTrigger("DieElectrocuted");
                StartCoroutine(CrDie());
            }
        }      
        //this.enabled = false;
    }
    public void DieArrowed()
    {
        if (_sr.color.a > 0.1f)
        {
            if (!_gameFunctionsController.Dead)
            {
                _gameFunctionsController.Dead = true;
                _animator.SetTrigger("DieArrowed");
                StartCoroutine(CrDie());
            }
        }
    }
    IEnumerator CrDie()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<Restarter>().Restart();
    }
    void Update()
    {
        if (_movementDisabled)
        {
            return;
        }
        if (transform.position.y >= 4)
        {
            _keeping = false;
            _movementDisabled = false;
            StartCoroutine(CrNext());
            IEnumerator CrNext()
            {
                _fadeOut.SetActive(true);
                yield return new WaitForSeconds(1f);
                FindObjectOfType<GameFlowController>().NextLevelScene();
            }
        }

        if (_usingMobile)
        {
            if (!_moving && _pressingButton)
            {
                StartCoroutine(CrMoveChar(_targetDir));
            }
            return;
        }
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        if (Mathf.Abs(xAxis) > _minMovement || Mathf.Abs(yAxis) > _minMovement)
        {
            _keeping = true;
            if (!_moving)
            {
                CheckInputs();
            }
        }
        else
        {
            _keeping = false;
        }
    }

    void OnMobileInput(Direction targetDirection)
    {
        _targetDir = targetDirection;
        _usingMobile = true;
        _pressingButton = true;
    }

    void OnMobileStop()
    {
        _pressingButton = false;
    }

    IEnumerator CrMoveChar(Direction dir)
    {
        if (!_gameFunctionsController.Dead)
        {
            _moving = true;
            float dur = _moveDur;
            Vector2 startPos = transform.position;
            Vector2 targetPos = transform.position;
            AnimationCurve aCurve = _easyInAnim;
            if (_keeping || _pressingButton)
            {
                aCurve = _keepAnim;
            }
            if (_lastDir != dir)
            {
                RestoreAnim();
            }
            switch (dir)
            {
                case Direction.up:

                    RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + Vector3.up * 0.8f, _mask);
                    if (!hit)
                    {
                        targetPos += Vector2.up;
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "MovableRock")
                        {
                            hit.collider.gameObject.GetComponent<MovableRock>().Move(Vector3.up);
                        }
                    }

                    _animator.SetBool("Up", true);
                    break;
                case Direction.down:

                    hit = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 0.8f, _mask);
                    if (!hit)
                    {
                        targetPos += Vector2.down;
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "MovableRock")
                        {
                            hit.collider.gameObject.GetComponent<MovableRock>().Move(Vector3.down);
                        }
                    }
                    _animator.SetBool("Down", true);
                    break;
                case Direction.left:
                    hit = Physics2D.Linecast(transform.position, transform.position + Vector3.left * 0.8f, _mask);
                    if (!hit)
                    {
                        targetPos += Vector2.left;
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "MovableRock")
                        {
                            hit.collider.gameObject.GetComponent<MovableRock>().Move(Vector3.left);
                        }
                    }
                    _animator.SetBool("Left", true);
                    break;
                case Direction.right:
                    hit = Physics2D.Linecast(transform.position, transform.position + Vector3.right * 0.8f, _mask);
                    if (!hit)
                    {
                        targetPos += Vector2.right;
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "MovableRock")
                        {
                            hit.collider.gameObject.GetComponent<MovableRock>().Move(Vector3.right);
                        }
                    }
                    _animator.SetBool("Right", true);
                    break;
            }
            _lastDir = dir;
            for (float i = 0; i < dur; i += Time.deltaTime)
            {
                transform.position = Vector2.Lerp(startPos, targetPos, aCurve.Evaluate(i / dur));
                yield return null;
            }
            transform.position = targetPos;
            if (_keeping || _pressingButton)
            {
                if (!_usingMobile)
                {
                    CheckInputs();
                }
                StartCoroutine(CrMoveChar(_targetDir));
            }
            else
            {
                RestoreAnim();
                IdleSprite();
                _moving = false;
            }
        }
        
    }
    
    public void IdleSprite()
    {
        float targetBlend = 0;
        switch (_lastDir)
        {
            case Direction.up:
                targetBlend = 0;
                break;
            case Direction.down:
                targetBlend = 0.33f;
                break;
            case Direction.left:
                targetBlend = 0.66f;
                break;
            case Direction.right:
                targetBlend = 1;
                break;
        }
        _animator.SetFloat("Blend", targetBlend);
    }

    void CheckInputs()
    {
        if(xAxis!= 0)
        {
            if (xAxis > _minMovement)
            {
                _targetDir = Direction.right;
            }
            if (xAxis < -_minMovement)
            {
                _targetDir = Direction.left;
            }
        }
        else if (yAxis != 0)
        {
            if (yAxis > _minMovement)
            {
                _targetDir = Direction.up;
            }
            if (yAxis < -_minMovement)
            {
                _targetDir = Direction.down;
            }
        }
        if (!_moving)
        {
            StartCoroutine(CrMoveChar(_targetDir));
        }
    }
    void RestoreAnim()
    {
        _animator.SetBool("Up", false);
        _animator.SetBool("Down", false);
        _animator.SetBool("Right", false);
        _animator.SetBool("Left", false);
    }

    public void RestartButton()
    {
        if (!_gameFunctionsController.Dead)
        {
            _gameFunctionsController.Dead = true;
            FindObjectOfType<Restarter>().Restart();
        }
    }
}
