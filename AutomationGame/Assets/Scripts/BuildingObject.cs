using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BuildingObject : GridObject
{
    public Building buildingType;
    public int workers;
    public int workerLimit;

    public int daysWorked;
    [SerializeField] private bool isWorking;
    [SerializeField] private int amountWorking;
    [SerializeField] private GameObject needWorkerWarning;

    public Dictionary<Storage.Resource, int> storage = new Dictionary<Storage.Resource, int>();

    public List<Storage.Resource> outputStorage = new List<Storage.Resource>();

    protected override void Start()
    {
        base.Start();
        needWorkerWarning.SetActive(false);
    }

    public override void InputResource(Storage.Resource resource)
    {
        if (!storage.ContainsKey(resource))
            storage[resource] = 1;
        else
            storage[resource]++;
    }

    public override void EndDay()
    {
        if (outputStorage.Count == 0)
            daysWorked += amountWorking;

        if (daysWorked >= buildingType.days)
        {
            foreach (InputOutput item in buildingType.input)
            {
                storage[item.resource] -= item.amount;
            }

            foreach (InputOutput item in buildingType.output)
            {
                for (int i = 0; i < item.amount; i++)
                    outputStorage.Add(item.resource);
            }

            daysWorked -= buildingType.days;
            amountWorking = 0;
        }

        if (outputStorage.Count > 0)
        {
            if (OutputResource(outputStorage[0], output))
                outputStorage.RemoveAt(0);
        }

        if (buildingType.input.Count == 0)
        {
            amountWorking = workers;
        }
        else if (HasResources())
        {
            amountWorking = workers;
        }

        needWorkerWarning.SetActive(workers == 0);
    }

    private bool HasResources() //check if the building has the resources in its storage
    {
        foreach (InputOutput item in buildingType.input)
        {
            if (storage.ContainsKey(item.resource))
            {
                if (storage[item.resource] < item.amount)
                {
                    return false;
                }
            }
            else
                return false;
        }

        return true;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);

    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);

    }

    private void OnMouseDown()
    {
        BuildingDisplay.Instance.SelectBuilding(this);
    }
}
