using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveDur, _minMovement;
    [SerializeField] private AnimationCurve _easyInAnim, _keepAnim;
    private Animator _animator;
    public enum DieType {Arrow, Ray};
    private GameFunctionsController.Direction _targetDir, _lastDir;
    private bool _moving, _keeping, _end, _pressingButton, _restarting;
    [SerializeField] private LayerMask _mask;
    SpriteRenderer _sr;
    private GameFunctionsController _gameFunctionsController;
    private GameFlowController _gameFlowController;
    private Vector2 _inputMovement;
    private MovementJoystick _joystick;
    private void Start()
    {
        _joystick = FindObjectOfType<MovementJoystick>();
        _gameFunctionsController = FindObjectOfType<GameFunctionsController>();
        _gameFlowController = FindObjectOfType<GameFlowController>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        GameEvents.MobileMovement.AddListener(OnMobileInput);
        GameEvents.OnStopMovement.AddListener(OnMobileStop);
        GameEvents.OnRestart.AddListener(Restart);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        //_inputMovement = value.ReadValue<Vector2>();
        //if (Mathf.Abs(_inputMovement.magnitude) > _minMovement)
        //{
        //    _keeping = true;
        //    if (!_moving)
        //    {
        //        CheckInputs();
        //    }
        //}
        //else
        //{
        //    _keeping = false;
        //}
    }
    public void OnContrast(InputAction.CallbackContext value)
    {

    }
    public void OnRestart(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Restart();
        }
    }

    //CAMBIAR ESTO A UNA SOLA FUNCION
    public void DieElectrocuted()
    {
        if (_sr.color.a > 0.1f)
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
    public void Die()
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
        ReloadScene();
    }

    public void CheckJoystickInput()
    {
        _inputMovement = _joystick.GetDirection();
        if (Mathf.Abs(_inputMovement.magnitude) > _minMovement)
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
    void Update()
    {
        CheckJoystickInput();
        if (_end)
        {
            return;
        }
        if (transform.position.y >= 4)
        {
            _keeping = false;
            _end = true;
            StartCoroutine(CrNext());
            IEnumerator CrNext()
            {
                GameEvents.ConsoleFadeOut.Invoke();
                yield return new WaitForSeconds(1f);
                _gameFunctionsController.LevelIndex++;
                if (_gameFlowController != null)
                {
                    FindObjectOfType<RestartButton>(true).SetEnable(true);
                    _gameFlowController.NextLevelScene();
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

        //if (_gameFunctionsController.UsingMobile)
        //{
        //    if (!_moving && _pressingButton)
        //    {
        //        StartCoroutine(CrMoveChar(_targetDir));
        //    }
        //}
    }

    void OnMobileInput(GameFunctionsController.Direction targetDirection)
    {
        _targetDir = targetDirection;
        _pressingButton = true;
    }

    void OnMobileStop()
    {
        _pressingButton = false;
    }

    IEnumerator CrMoveChar(GameFunctionsController.Direction dir)
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
                case GameFunctionsController.Direction.Up:
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
                case GameFunctionsController.Direction.Down:

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
                case GameFunctionsController.Direction.Left:
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
                case GameFunctionsController.Direction.Right:
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
                CheckInputs();
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
            case GameFunctionsController.Direction.Up:
                targetBlend = 0;
                break;
            case GameFunctionsController.Direction.Down:
                targetBlend = 0.33f;
                break;
            case GameFunctionsController.Direction.Left:
                targetBlend = 0.66f;
                break;
            case GameFunctionsController.Direction.Right:
                targetBlend = 1;
                break;
        }
        _animator.SetFloat("Blend", targetBlend);
    }

    void CheckInputs()
    {
        _inputMovement = _joystick.GetDirection();

        if(Mathf.Abs(_inputMovement.x )> Mathf.Abs(_inputMovement.y))
        {
            if (_inputMovement.x > _minMovement)
            {
                _targetDir = GameFunctionsController.Direction.Right;
            }
            if (_inputMovement.x < -_minMovement)
            {
                _targetDir = GameFunctionsController.Direction.Left;
            }
        }
        else
        {
            if (_inputMovement.y > _minMovement)
            {
                _targetDir = GameFunctionsController.Direction.Up;
            }
            if (_inputMovement.y < -_minMovement)
            {
                _targetDir = GameFunctionsController.Direction.Down;
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

    public void Restart()
    {
        if (!_gameFunctionsController.Dead)
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        if (!_restarting)
        {
            _restarting = true;
            _gameFunctionsController.Dead = true;
            GameEvents.ConsoleFadeOut.Invoke();
            StartCoroutine(CrRestart());
            IEnumerator CrRestart()
            {
                yield return new WaitForSeconds(1.25f);
                _gameFunctionsController.Dead = false;
                if (SceneManager.sceneCount > 1)
                {
                    SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(gameObject.scene.name);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}
