using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _categoryName;
    private int _categoryIndex;

    public void Init(string category, int index)
    {
        _categoryName.text = category;
        _categoryIndex = index;
    }
}
