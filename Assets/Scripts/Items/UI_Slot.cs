using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    private Button button;
    public int index;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(SelectItem);
    }

    private void SelectItem()
    {
        GameManager.Instance.SetSelectedItem(index);
    }
}
