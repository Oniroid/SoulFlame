using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _gradient, _body, _powerOn, _powerOff, _mobileUI;
    [SerializeField] private Animator _cameraAnim, _titleAnim, _brightAnim;
    [SerializeField] private AudioSource _aSource;

    private bool _canStart, _cheatActive, _canRestart;
    private int _levelIndex;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _gradient.SetActive(true);
        _body.SetActive(false);
        _powerOn.SetActive(false);
        yield return new WaitForSeconds(3);
        _canStart = true;
    }

    private void Update()
    {
        if (_canStart && Input.anyKeyDown)
        {
            _canStart = false;
            StartGame();
        }
    }

    public void StartGame()
    {
        _cameraAnim.SetTrigger("Start");
        _titleAnim.SetTrigger("Start");
    }

    public void Restart()
    {
        if (_canRestart)
        {
            _canRestart = false;
            FindObjectOfType<Restarter>().Restart();
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
        SceneManager.LoadScene("Start",LoadSceneMode.Additive);
    }
    public void FirstLevel()
    {
        _mobileUI.SetActive(true);
        SceneManager.UnloadSceneAsync("Start");
        SceneManager.LoadScene("Level0", LoadSceneMode.Additive);
    }
    public void NextLevel()
    {
        _levelIndex++;
        if (_levelIndex == 3)
        {
            SceneManager.UnloadSceneAsync($"Level{_levelIndex-1}");
            SceneManager.LoadScene("Contrast_Room");
        }
        else
        {
            SceneManager.UnloadSceneAsync($"Level{_levelIndex - 1}");
            SceneManager.LoadScene($"Level{_levelIndex}", LoadSceneMode.Additive);
        }
    }

    public void ActiveCheat()
    {
        _cheatActive = true;
    }
    public bool HasCheat()
    {
        return _cheatActive;
    }
}
