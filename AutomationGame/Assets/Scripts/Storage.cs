using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    public static Storage instance;

    public enum Resource { Food, Metal, Weapon }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int[] allResources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            allResources = new int[Enum.GetNames(typeof(Resource)).Length];
            UpdateText();
        }
    }

    public void GainResource(Resource resource, int amount) //adds resources to the storage
    {
        allResources[(int)resource] += amount;
        UpdateText();
    }

    public void SpendResource(Resource resource, int amount) //spends the resources
    {
        allResources[(int)resource] -= amount;
        UpdateText();
    }

    public bool HasResource(Resource resource, int amount) //checks if storage has the specified resource
    {
        return allResources[(int)resource] >= amount;
    }

    private void UpdateText()
    {
        text.text = string.Empty;
        for (int i = 0; i < allResources.Length; i++) //goes through all the resources and displays them
        {
            text.text += (Resource)i + ": " + allResources[i] + "\n";
        }
    }
}
