using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _categoryName;
    private int _categoryIndex;
    private MapFiller _mapFiller;
    private string _categoryPath;

    public void Init(string category, int index, string path)
    {
        _categoryName.text = category;
        _categoryIndex = index;
        _categoryPath = path;
        _mapFiller = FindObjectOfType<MapFiller>();
    }

    public void SelectCategory()
    {
        _mapFiller.ShowCategory(_categoryIndex);
        _mapFiller.SelectCategory(_categoryPath);
    }
}
