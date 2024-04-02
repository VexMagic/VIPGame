using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DropdownItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void SelectResource()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Storage.Resource)).Length; i++)
        {
            if (label.text == ((Storage.Resource)i).ToString())
            {
                FilterDisplay.Instance.SelectResource((Storage.Resource)i);
                break;
            }
        }
    }
}
