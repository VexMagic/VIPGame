using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingDisplay : MonoBehaviour
{
    public static BuildingDisplay Instance;

    public BuildingObject selectedBuilding;
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI amountDisplay;
    [SerializeField] private TextMeshProUGUI inputAmount;
    [SerializeField] private TextMeshProUGUI outputAmount;
    [SerializeField] private GameObject noWorkers;
    [SerializeField] private Slider progress;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SelectBuilding(BuildingObject buildingObject)
    {
        animator.SetBool("IsOpen", true);
        selectedBuilding = buildingObject;
        UpdateDisplay();
    }

    public void CloseDisplay()
    {
        animator.SetBool("IsOpen", false);
        selectedBuilding = null;
    }

    public void Increase(int amount)
    {
        if (selectedBuilding == null || !WorkerManager.Instance.HasFreeWorkers(amount))
            return;

        if (selectedBuilding.workerLimit >= selectedBuilding.workers + amount)
        {
            selectedBuilding.workers += amount;
            WorkerManager.Instance.AssignWorkers(amount);
            UpdateDisplay();
        }
    }

    public void Decrease(int amount)
    {
        if (selectedBuilding == null)
            return;

        if (selectedBuilding.workers >= amount)
        {
            selectedBuilding.workers -= amount;
            WorkerManager.Instance.FreeWorkers(amount);
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        if (selectedBuilding == null)
            return;

        noWorkers.SetActive(selectedBuilding.workers == 0);

        buildingName.text = selectedBuilding.buildingType.name;
        amountDisplay.text = selectedBuilding.workers + "/" + selectedBuilding.workerLimit;

        progress.value = selectedBuilding.daysWorked;
        progress.maxValue = selectedBuilding.buildingType.days;

        outputAmount.text = selectedBuilding.outputStorage.Count.ToString();

        WorkerManager.Instance.UpdateDisplay();
    }
}
