using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : GridObject
{
    public Building buildingType;
    [SerializeField] private int workers;
    [SerializeField] private int workerLimit;

    [SerializeField] private int daysWorked;
    [SerializeField] private bool isWorking;
    [SerializeField] private int amountWorking;

    public Dictionary<Storage.Resource, int> storage = new Dictionary<Storage.Resource, int>();

    [SerializeField] GameObject highlight;


    public override void InputResource(Storage.Resource resource)
    {
        if (!storage.ContainsKey(resource))
            storage[resource] = 1;
        else
            storage[resource]++;
    }

    public override void EndDay()
    {
        if (isWorking) //checks if people are working in the building
        {
            daysWorked++;

            if (daysWorked >= buildingType.days) //checks if people have worked for enough days to produce the resource
            {
                for (int i = 0; i < amountWorking; i++) //adds an amount of resources to the storage depending on the amount of workers
                {
                    foreach (InputOutput item in buildingType.output)
                    {
                        OutputResource(item.resource);
                    }
                }

                daysWorked = 0;
                isWorking = false;
                amountWorking = 0;
            }
        }

        if (!isWorking && workers > 0)
        {
            //checks if building needs an input to work or not
            
            if (buildingType.input.Count == 0) //if input isn't needed, all workers start working
            {
                isWorking = true;
                amountWorking = workers;
            }
            else //if input is needed...
            {
                for (int i = 0; i < workers; i++)
                {
                    if (HasResources()) //... check if they have the resources to start working for each worker
                    {
                        isWorking = true;
                        amountWorking++; //increase the amount of workers with enough resources
                        foreach (InputOutput item in buildingType.input)
                        {
                            storage[item.resource] -= item.amount;
                        }
                    }
                }
            }
        }
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
}
