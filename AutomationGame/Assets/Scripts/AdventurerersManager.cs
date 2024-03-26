using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdventurerersManager : MonoBehaviour
{
    public static AdventurerersManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI adventurerDisplay;
    [SerializeField] private int adventurerCount;
    [SerializeField] private int adventurersUsed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UpdateDisplay();
        }
    }

    public bool HasFreeAdv(int amount)
    {
        return adventurerCount - adventurersUsed >= amount;
    }

    public void AssignAdv(int amount)
    {
        adventurersUsed += amount;
    }

    public void FreeAdv(int amount)
    {
        adventurersUsed -= amount;
    }

    public void GainAdv(int amount)
    {
        adventurerCount += amount;
    }

    public void UpdateDisplay()
    {
        adventurerDisplay.text = (adventurerCount - adventurersUsed) + "/" + adventurerCount;
    }
}

