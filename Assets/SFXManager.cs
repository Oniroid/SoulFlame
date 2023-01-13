using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public enum ClipName {Start };
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioSource[] _aSources;
    private int _currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.PlaySFX.AddListener(PlaySound);
    }

    public void PlaySound(ClipName name)
    {
        _aSources[_currentIndex].clip = _clips[(int)name];
        _aSources[_currentIndex].Play();
        _currentIndex++;
        _currentIndex %= _clips.Length;
    }
}
