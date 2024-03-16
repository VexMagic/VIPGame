using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingDisplay : MonoBehaviour
{
    public static BuildingDisplay instance;

    private BuildingObject selectedBuilding;

    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI workerAmount;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SelectBuilding(BuildingObject selected)
    {
        selectedBuilding = selected;
        animator.SetBool("IsOpen", true);
        UpdateDisplay();
    }

    public void EndSelect()
    {
        animator.SetBool("IsOpen", false);
        selectedBuilding = null;
    }

    public void Increase()
    {
        if (WorkerManager.instance.HasWorkersFree() && selectedBuilding.workers < selectedBuilding.workerLimit)
        {
            WorkerManager.instance.UseWorker();
            selectedBuilding.workers++;
            UpdateDisplay();
        }
    }

    public void Decrease()
    {
        if (selectedBuilding.workers > 0)
        {
            WorkerManager.instance.FreeWorker();
            selectedBuilding.workers--;
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        if (selectedBuilding == null)
            return;

        buildingName.text = selectedBuilding.buildingType.name;
        workerAmount.text = selectedBuilding.workers.ToString() + "/" + selectedBuilding.workerLimit.ToString();
        slider.maxValue = selectedBuilding.buildingType.days;
        slider.value = selectedBuilding.daysWorked;
    }
}
