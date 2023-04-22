using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelTestingController : MonoBehaviour    //Pensado para los cambios de escena y/o estéticos
{
    [SerializeField] private GameObject _gradient, _body, _powerOn, _mobileUI;
    [SerializeField] private Animator _cameraAnim, _titleAnim, _brightAnim;
    [SerializeField] private AudioSource _aSource;
    private GameFunctionsController _gameFunctionsController;

    
    private void Awake()
    {

    }

    void Start()
    {
        GameEvents.OnAlphaChange.AddListener(OnBright);
        GameEvents.OnStopAlpha.AddListener(StopBright);
        _gameFunctionsController = FindObjectOfType<GameFunctionsController>();
        _body.SetActive(false);
        _powerOn.SetActive(false);
        StartGameCallBack();
        FirstCallBack();
    }

    public void OnAnyKey(InputAction.CallbackContext value)
    {
        StartGame();
    }

    public void StartGame()
    {
        if (_gameFunctionsController.CanStart)
        {
            _gameFunctionsController.CanStart = false;
            GameObject.Find("3DInput").SetActive(false);
            _cameraAnim.SetTrigger("Start");
            _titleAnim.SetTrigger("Start");
        }
    }

    public void StartGameCallBack()
    {
        StartCoroutine(CrBody());
        IEnumerator CrBody()
        {
            _body.transform.position -= Vector3.up;
            _body.SetActive(true);
            yield return null;
            _body.transform.position += Vector3.up;
            _powerOn.SetActive(true);
        }
    }

    public void OnBright(bool up)
    {
        if (up)
        {
            _brightAnim.SetBool("UpContrast", true);
            _brightAnim.SetBool("DownContrast", false);
        }
        else
        {
            _brightAnim.SetBool("UpContrast", false);
            _brightAnim.SetBool("DownContrast", true);
        }
    }

    public void StopBright()
    {
        _brightAnim.SetBool("UpContrast", false);
        _brightAnim.SetBool("DownContrast", false);
    }

    public void FirstCallBack()
    {
        _aSource.Play();
        SceneManager.LoadScene(PlayerPrefs.GetString("LevelToTest", "Level0"), LoadSceneMode.Additive);
    }
    public void FirstLevel()
    {
        _mobileUI.SetActive(true);
        SceneManager.UnloadSceneAsync("Start");
        SceneManager.LoadScene("Level0", LoadSceneMode.Additive);
    }
    public void NextLevelScene()
    {
        SceneManager.UnloadSceneAsync($"Level{_gameFunctionsController.LevelIndex - 1}");
        SceneManager.LoadScene($"Level{_gameFunctionsController.LevelIndex}", LoadSceneMode.Additive);
    }
}
