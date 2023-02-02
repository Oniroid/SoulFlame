using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TileBehaviourEditor : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _valueText;
    private string _tilePath;
    private MapFiller _mapFiller;
    private Image _image;
    public string Tile
    {
        get { return _tilePath;}
        set {
            _tilePath = value;
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
            Tile = "";
            _image.sprite = _mapFiller.GetSprite(Tile);
            return;
        }
        if (Mouse.current.leftButton.isPressed && Tile != MapFiller.selectedTilePath)
        {
            Tile = MapFiller.selectedTilePath;
            _image.sprite = _mapFiller.GetSprite(Tile);
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Tile = "";
            _image.sprite = _mapFiller.GetSprite(Tile);
            return;
        }
        if (Tile != MapFiller.selectedTilePath)
        {
            Tile = MapFiller.selectedTilePath;
            _image.sprite = _mapFiller.GetSprite(Tile);
        }
    }
}
