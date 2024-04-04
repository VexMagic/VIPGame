using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;

public class FilterDisplay : MonoBehaviour
{
    public static FilterDisplay Instance;
    [SerializeField] private Animator animator;

    [SerializeField] private TMP_Dropdown dropdown;


    private Filter filter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < Enum.GetValues(typeof(Storage.Resource)).Length; i++)
        {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
            newOption.text = ((Storage.Resource)i).ToString();
            newOption.image = Storage.instance.GetSprite((Storage.Resource)i);
            options.Add(newOption);
        }

        dropdown.AddOptions(options);
    }

    public void SelectResource(Storage.Resource resource)
    {
        if (filter != null)
        {
            filter.filteredResource = resource;
            filter.selected = true;
        }
    }

    public void SelectFilter(Filter filter)
    {
        if (CustomCursor.Instance != null && CustomCursor.Instance.destroyMode)
            return;

        this.filter = filter;
        dropdown.value = (int)filter.filteredResource;
        animator.SetBool("IsOpen", true);
        BuildingDisplay.Instance.CloseDisplay();
        DungeonDisplay.Instance.CloseDisplay();
        TownhallDisplay.Instance.CloseDisplay();
    }

    public void CloseDisplay()
    {
        animator.SetBool("IsOpen", false);
    }
}
