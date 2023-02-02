using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _categoryName;
    private int _categoryIndex;
    private MapFiller _mapFiller;

    public void Init(string category, int index)
    {
        _categoryName.text = category;
        _categoryIndex = index;
        _mapFiller = FindObjectOfType<MapFiller>();
    }

    public void SelectCategory()
    {
        _mapFiller.ShowCategory(_categoryIndex);
    }
}
