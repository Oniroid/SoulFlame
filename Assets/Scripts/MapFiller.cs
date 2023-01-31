using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;

public class MapFiller : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Transform _tilesHolder;
    [SerializeField] private GameObject _buttonTilePrefab;
    [SerializeField] private GameObject _buttonHolder;
    [SerializeField] private string _levelName;
    public static int selectedTile;
    private List<List<Image>> _tiles;

    public Sprite GetSprite(int spriteIndex)
    {
        return _sprites[spriteIndex];
    }
    public void SelectTile(int newSelectedTile)
    {
        selectedTile = newSelectedTile;
    }
    IEnumerator Start()
    {
        if (!File.Exists(Application.streamingAssetsPath +"/"+ _levelName + ".json"))
        {
            File.WriteAllText(Application.streamingAssetsPath + "/" + _levelName + ".json", JsonUtility.ToJson(new Level()));
            yield return new WaitForSeconds(1f);
        }
        string jsonLevel = File.ReadAllText(Application.streamingAssetsPath + "/" + _levelName + ".json");
        Level l = JsonUtility.FromJson<Level>(jsonLevel);
        _tiles = new List<List<Image>>();
        for (int i = 0; i < 10; i++)
        {
            List<Image> tilesRow = new List<Image>();
            for (int j = 0; j < 11; j++)
            {
                Image targetTile = _tilesHolder.GetChild((11 * i) + j).GetComponent<Image>();
                tilesRow.Add(targetTile);
                int tileValue = l._tiles[(11 * i) + j];
                targetTile.GetComponent<TileBehaviourEditor>().Tile = tileValue;
            }
            _tiles.Add(tilesRow);
        }
        for (int i = 0; i < _tiles.Count; i++)
        {
            for (int j = 0; j < _tiles[i].Count; j++)
            {
                _tiles[i][j].name = $"[{i},{j}]";
            }
        }
        for(int i =0; i< _sprites.Count; i++)
        {
           GameObject g = Instantiate(_buttonTilePrefab, _buttonHolder.transform);
            g.GetComponent<Image>().sprite = _sprites[i];
            int captured = i;
            g.GetComponent<Button>().onClick.AddListener(()=>SelectTile(captured));
        }
    }

    public void SaveLevel()
    {
        List<int> tilesToJSON = new List<int>();
        for (int i = 0; i < _tiles.Count; i++)
        {
            for (int j = 0; j < _tiles[i].Count; j++)
            {
                tilesToJSON.Add(_tiles[i][j].GetComponent<TileBehaviourEditor>().Tile);
            }
        }
        Level l = new Level(tilesToJSON);
        File.WriteAllText(Application.streamingAssetsPath + "/" + _levelName + ".json", JsonUtility.ToJson(l));      
    }
    private void Update()
    {
        if (Keyboard.current[Key.S].wasPressedThisFrame)
        {
            SaveLevel();
        }
    }
}


[System.Serializable]
public class Level
{
    public List<int> _tiles;

    public Level(List<int> tiles)
    {
        _tiles = tiles;
    }
    public Level()
    {
        _tiles = new List<int>();
        for (int i = 0; i < 110; i++)
        {
            _tiles.Add(0);
        }
    }
}


