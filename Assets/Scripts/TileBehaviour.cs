using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileBehaviour: MonoBehaviour
{
    [SerializeField] private TextMeshPro _valueText;
    private string _tilePath;
    private LevelGenerator _levelGenerator;
    public string TilePath
    {
        get { return _tilePath; }
        set {
            _tilePath = value;
            GetComponent<SpriteRenderer>().sprite = _levelGenerator.GetSprite(value);
        }
    }

    private void Awake()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
    }
}
