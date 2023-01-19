using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileBehaviour: MonoBehaviour
{
    [SerializeField] private TextMeshPro _valueText;
    private int _tile;
    private LevelGenerator _levelGenerator;
    public int Tile
    {
        get { return _tile;}
        set {
            _tile = value;
            GetComponent<SpriteRenderer>().sprite = _levelGenerator.GetSprite(value);
        }
    }

    private void Awake()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
    }
}
