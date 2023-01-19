using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileBehaviourEditor : MonoBehaviour
{
    [SerializeField] private TextMeshPro _valueText;
    private int _tile;
    private MapFiller _mapFiller;
    public int Tile
    {
        get { return _tile;}
        set {
            _tile = value;
            GetComponent<SpriteRenderer>().sprite = _mapFiller.GetSprite(value);
        }
    }
    SpriteRenderer _sprite;

    void Awake()
    {
        _mapFiller = FindObjectOfType<MapFiller>();
    }

    void Update()
    {
        _valueText.text = Tile.ToString();
    }

    private void OnMouseDown()
    {
        Tile = MapFiller.selectedTile;
        GetComponent<SpriteRenderer>().sprite = _mapFiller.GetSprite(Tile);
    }
}
