using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager instance;

    [SerializeField] private TextMeshProUGUI amoundDisplay;

    public int workerAmount;
    public int workersUsed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        amoundDisplay.text = (workerAmount - workersUsed).ToString() + "/" + workerAmount.ToString();
    }

    public bool HasWorkersFree()
    {
        return workerAmount > workersUsed;
    }

    public void FreeWorker()
    {
        workersUsed--;
        UpdateDisplay();
    }

    public void UseWorker()
    {
        workersUsed++;
        UpdateDisplay();
    }

    public void GainWorker()
    {
        workerAmount++;
        UpdateDisplay();
    }
}
