using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TileBehaviourEditor : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _valueText;
    private int _tile;
    private MapFiller _mapFiller;
    private Image _image;
    public int Tile
    {
        get { return _tile;}
        set {
            _tile = value;
            GetComponent<Image>().sprite = _mapFiller.GetSprite(value);
            _valueText.text = Tile.ToString();
        }
    }

    void Awake()
    {
        _mapFiller = FindObjectOfType<MapFiller>();
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Tile = 1;
            _image.sprite = _mapFiller.GetSprite(1);
            return;
        }
        if (Mouse.current.leftButton.isPressed && Tile != MapFiller.selectedTile)
        {
            Tile = MapFiller.selectedTile;
            _image.sprite = _mapFiller.GetSprite(Tile);
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Tile = 1;
            _image.sprite = _mapFiller.GetSprite(1);
            return;
        }
        if (Tile != MapFiller.selectedTile)
        {
            Tile = MapFiller.selectedTile;
            _image.sprite = _mapFiller.GetSprite(Tile);
        }
    }
}
