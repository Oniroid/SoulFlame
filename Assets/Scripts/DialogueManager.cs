using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _contentTx, _buttonTitle;
    [SerializeField] private float _writeCharTime;
    [SerializeField] private GameObject _continueButton, _mainHolder;
    [SerializeField] private Dialogue[] _dialogs;
    private int _currentDialog, _currentSentenceIndex = 0;
    private float _currentWriteTime;
    private bool _canNext, _clickingButton;
    void Start()
    {
        _continueButton.SetActive(false);
        _mainHolder.SetActive(false);
        _currentWriteTime = _writeCharTime;
        GameEvents.ThrowDialog.AddListener(StartDialog);
        GameEvents.MobileMovement.AddListener(AnyButton);
        GameEvents.OnStopMovement.AddListener(StopButton);
    }

    public void AnyButton(GameFunctionsController.Direction dir)
    {
        _clickingButton = true;
    }

    public void StopButton()
    {
        _clickingButton = false;
    }

    public void StartDialog(string dialogName)
    {
        _mainHolder.SetActive(true);
        for (int i = 0; i<_dialogs.Length; i++)
        {
            if (_dialogs[i].name == dialogName)
            {
                _currentDialog = i;
                WriteSentence();
            }
        }
    }

    private void Update()
    {
        if (_clickingButton)
        {
            if (_canNext)
            {
                WriteSentence();
            }
        }
        if (_clickingButton)
        {
            _currentWriteTime = _writeCharTime / 3f;
        }
        else if(_currentWriteTime != _writeCharTime)
        {
            _currentWriteTime = _writeCharTime;
        }
    }

    public void WriteSentence()
    {
        int totalSentences = _dialogs[_currentDialog].sentences.Length;
        StartCoroutine(CrWaitForWrite());
        IEnumerator CrWaitForWrite()
        {
            _canNext = false;
            _continueButton.SetActive(false);
            _contentTx.text = "";
            if (_currentSentenceIndex == 0)
            {
                yield return new WaitForSeconds(1f);
            }
            if (_currentSentenceIndex < totalSentences)
            {
                StartCoroutine(CrWriteText(_dialogs[_currentDialog].sentences[_currentSentenceIndex]));
                _currentSentenceIndex++;
                if(_currentSentenceIndex == totalSentences)
                {
                    _buttonTitle.text = "Close";
                }
                else
                {
                    _buttonTitle.text = "Continue";
                }
            }
            else
            {
                _continueButton.SetActive(false);
                _mainHolder.SetActive(false);
                _currentSentenceIndex = 0;
            }
        }
    }

    IEnumerator CrWriteText(string targetContent)
    {
        _contentTx.text = "";
        for (int i = 0; i < targetContent.Length; i++)
        {
            _contentTx.text += targetContent[i];
            if (targetContent[i] == ',')
            {
                yield return new WaitForSeconds(_currentWriteTime * 2);
            }
            if (targetContent[i] == '.' || targetContent[i] == '!')
            {
                yield return new WaitForSeconds(_currentWriteTime * 4);
            }
            yield return new WaitForSeconds(_currentWriteTime);
        }
        _continueButton.SetActive(true);
        _canNext = true;
    }
}
