using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TileBehaviourEditor : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private string _tilePath;
    private MapFiller _mapFiller;
    private Image _image;

    public string TilePath
    {
        get { return _tilePath; }
        set
        {
            _tilePath = value;
            _image.sprite = _mapFiller.GetSpriteFromRawPath(value);
        }
    }
    void Awake()
    {
        _mapFiller = FindObjectOfType<MapFiller>();
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //if (Mouse.current.rightButton.isPressed)
        //{
        //    Tile = 0;
        //    _image.sprite = _mapFiller.GetSprite(Tile);
        //    return;
        //}
        if (Mouse.current.leftButton.isPressed && TilePath != MapFiller.selectedPath)
        {
            TilePath = MapFiller.selectedPath;
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //if (Mouse.current.rightButton.isPressed)
        //{
        //    Tile = "";
        //    _image.sprite = _mapFiller.GetSprite(Tile);
        //    return;
        //}
        if (TilePath != MapFiller.selectedPath)
        {
            TilePath = MapFiller.selectedPath;
        }
    }
}
