using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;
using UnityEditor;

public class MapFiller : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Transform _tilesHolder, _toolsHolder, _categoryButtonsHolder;
    [SerializeField] private GameObject _buttonTilePrefab, _toolsPanelPrefab, _categoryButtonPrefab;
    [SerializeField] private string _levelName;
    public static int selectedTile;
    private List<List<Image>> _tiles;
    private Level _level;
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
        _level = JsonUtility.FromJson<Level>(jsonLevel);
        LoadMap();
        LoadSideTools();
    }
    private void Update()
    {
        if (Keyboard.current[Key.S].wasPressedThisFrame)
        {
            SaveLevel();
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

    void LoadMap()
    {
        _tiles = new List<List<Image>>();
        for (int i = 0; i < 10; i++)
        {
            List<Image> tilesRow = new List<Image>();
            for (int j = 0; j < 11; j++)
            {
                Image targetTile = _tilesHolder.GetChild((11 * i) + j).GetComponent<Image>();
                tilesRow.Add(targetTile);
                int tileValue = _level._tiles[(11 * i) + j];
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
    }

    void LoadSideTools()
    {
        string[] subfolders = AssetDatabase.GetSubFolders("Assets/Resources/MapCreator");
        List<Transform> toolPanels = new List<Transform>();
        for (int i = 0; i < subfolders.Length; i++)
        {
            string folderName = subfolders[i].Split('/')[subfolders[i].Split('/').Length - 1];
            Transform t = Instantiate(_toolsPanelPrefab, _toolsHolder).transform;
            Instantiate(_categoryButtonPrefab, _categoryButtonsHolder).GetComponent<CategoryButton>().Init(folderName, i);
            toolPanels.Add(t);

            DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/MapCreator/" + folderName);
            if (d.GetFiles().Length != 0)
            {
                foreach (var file in d.GetFiles("*.png"))
                {
                    string convertedName = file.Name;
                    convertedName = convertedName.Remove(convertedName.Length - 4, 4);
                    Sprite[] sprites = Resources.LoadAll<Sprite>($"MapCreator/{folderName}/{convertedName}");
                    for (int j = 0; j < sprites.Length; j++)
                    {
                        GameObject g = Instantiate(_buttonTilePrefab, t.GetChild(0));
                        g.GetComponent<Image>().sprite = sprites[j];
                        int captured = j;
                        g.GetComponent<Button>().onClick.AddListener(() => SelectTile(captured));
                    }
                }
            }
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


