using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    Level _level;
    [SerializeField] private string _levelName;
    public Sprite GetSprite(string spritePath)
    {
        return Resources.Load<Sprite>(spritePath);
    }
    void Start()
    {
        string jsonLevel = System.IO.File.ReadAllText(Application.dataPath + "/"+_levelName+".json");
        _level = JsonUtility.FromJson<Level>(jsonLevel);
        for (int i = 0; i < 10; i++)
        {
            List<GameObject> tilesRow = new List<GameObject>();
            for (int j = 0; j < 11; j++)
            {
                GameObject g = Instantiate(_tilePrefab, new Vector3(-5f + j, -5f + i, 0), Quaternion.identity);
                string tilePath = _level._tilePath[(11 * i) + j];
                g.GetComponent<TileBehaviour>().TilePath = tilePath;
            }
        }
    }
}
