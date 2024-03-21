using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI workerDisplay;
    [SerializeField] private int workerCount;
    [SerializeField] private int workersUsed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UpdateDisplay();
        }
    }

    public bool HasFreeWorkers(int amount)
    {
        return workerCount - workersUsed >= amount;
    }

    public void AssignWorkers(int amount)
    {
        workersUsed += amount;
    }

    public void FreeWorkers(int amount)
    {
        workersUsed -= amount;
    }

    public void GainWorkers(int amount )
    {
        workerCount += amount;
    }

    public void UpdateDisplay()
    {
        workerDisplay.text = (workerCount - workersUsed) + "/" + workerCount;
    }
}
