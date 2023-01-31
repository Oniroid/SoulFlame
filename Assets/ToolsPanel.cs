using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTxt;
    public void Init(string targetName)
    {
        _nameTxt.text = targetName;
    }
}
