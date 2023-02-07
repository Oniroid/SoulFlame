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
    [SerializeField] private GameObject _lines;
    public static string selectedCategoryPath;
    public static int selectedTileIndex;
    private List<List<Image>> _tiles;
    private Level _level;
    List<Transform> _toolPanels;

    public Sprite GetSpriteFromRawPath(string spritePath)
    {
        Sprite targetSprite;
        Sprite[] sprites = Resources.LoadAll<Sprite>(GetSplittedPath(spritePath));
        if (sprites.Length == 0) //De esta forma sabemos si es un sprite suelto
        {
            targetSprite = Resources.Load<Sprite>(GetSplittedPath(spritePath));
        }
        else
        {
            //print(spritePath + "  //  " + GetPathIndex(spritePath));
            targetSprite = sprites[GetPathIndex(spritePath)];
        }
        return targetSprite;
    }
    public void SelectTile(int newSelectedTile)
    {
        selectedTileIndex = newSelectedTile;
    }
    public void SelectCategory(string selectedCategory)
    {
        selectedCategoryPath = selectedCategory;
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
        List<string> tilesToJSON = new List<string>();
        for (int i = 0; i < _tiles.Count; i++)
        {
            for (int j = 0; j < _tiles[i].Count; j++)
            {
                tilesToJSON.Add(_tiles[i][j].GetComponent<TileBehaviourEditor>().TilePath);
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
                string tileValue = _level._tilePath[(11 * i) + j];
                targetTile.GetComponent<TileBehaviourEditor>().Tile = GetPathIndex(tileValue);
                targetTile.GetComponent<TileBehaviourEditor>().TilePath = tileValue;
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
        _toolPanels = new List<Transform>();
        for (int i = 0; i < subfolders.Length; i++)
        {
            string folderName = subfolders[i].Split('/')[subfolders[i].Split('/').Length - 1];
            Transform t = Instantiate(_toolsPanelPrefab, _toolsHolder).transform;
            //Revisar si hay varios archivos para crear sub rutas
            Instantiate(_categoryButtonPrefab, _categoryButtonsHolder).GetComponent<CategoryButton>().Init(folderName, i, $"MapCreator/{folderName}/");
            if(i == 0) 
            {
                selectedCategoryPath = "MapCreator/" + folderName + "/";
            }
            _toolPanels.Add(t);
            DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Resources/MapCreator/" + folderName);
            if (d.GetFiles().Length != 0)
            {
                foreach (var file in d.GetFiles("*.png"))
                {
                    string convertedName = file.Name;
                    convertedName = convertedName.Remove(convertedName.Length - 4, 4);
                    Sprite[] sprites = Resources.LoadAll<Sprite>($"MapCreator/{folderName}/{convertedName}");
                    if (sprites.Length == 0) //De esta forma sabemos si es un sprite suelto
                    {
                        GameObject g = Instantiate(_buttonTilePrefab, t.GetChild(0));
                        g.GetComponent<Image>().sprite = Resources.Load<Sprite>($"MapCreator/{folderName}/{convertedName}");
                    }
                    else
                    {
                        for (int j = 0; j < sprites.Length; j++)
                        {
                            //$"MapCreator/{folderName}/{ convertedName}/{j}"
                            GameObject g = Instantiate(_buttonTilePrefab, t.GetChild(0));
                            int extract = j;
                            g.GetComponent<Image>().sprite = sprites[extract];
                            g.GetComponent<Button>().onClick.AddListener(() => SelectTile(extract));
                        }
                    }
                }
            }
        }
        ShowCategory(0);
    }

    public string GetSplittedPath(string path)
    {
        string[] converted = path.Split('/');
        string final = "";
        for (int i = 0; i < converted.Length - 1; i++)
        {
            final += converted[i] + "/";
        }
        final = final.Remove(final.Length - 1, 1);
        return final;
    }

    public int GetPathIndex(string path)
    {
        string[] converted = path.Split('/');
        return int.Parse(converted[converted.Length - 1]);
    }

    public void ShowCategory(int categoryIndex)
    {
        foreach (Transform t in _toolPanels)
        {
            t.gameObject.SetActive(false);
        }
        _toolPanels[categoryIndex].gameObject.SetActive(true);
    }
    public void LinesButton()
    {
        _lines.SetActive(!_lines.activeSelf);
    }
}



[System.Serializable]
public class Level
{
    public List<string> _tilePath;

    public Level(List<string> tilesPath)
    {
        _tilePath = tilesPath;
    }
    public Level()
    {
        _tilePath = new List<string>();
        for (int i = 0; i < 110; i++)
        {
            _tilePath.Add("MapCreator/Walls/paredes/19");
        }
    }
}


