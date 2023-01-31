using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;

public class MapFiller : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private List<Sprite> _sprites;
    private List<List<GameObject>> _tiles;
    public static int selectedTile;
    [SerializeField] private GameObject _buttonTilePrefab;
    [SerializeField] private GameObject _buttonHolder;
    [SerializeField] private string _levelName;
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
        print(jsonLevel);
        Level l = JsonUtility.FromJson<Level>(jsonLevel);
        _tiles = new List<List<GameObject>>();
        for (int i = 0; i < 10; i++)
        {
            List<GameObject> tilesRow = new List<GameObject>();
            for (int j = 0; j < 11; j++)
            {
                GameObject g = Instantiate(_tilePrefab, new Vector3(-5f + j, -5f + i, 0), Quaternion.identity);
                tilesRow.Add(g);
                int tileValue = l._tiles[(11 * i) + j];
                g.GetComponent<TileBehaviourEditor>().Tile = tileValue;
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


