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

    public Dictionary<Storage.Resource, int> inputStorage = new Dictionary<Storage.Resource, int>();
    public List<Storage.Resource> outputStorage = new List<Storage.Resource>();

    [SerializeField] GameObject highlight;


    public override void InputResource(Storage.Resource resource)
    {
        if (!inputStorage.ContainsKey(resource))
            inputStorage[resource] = 1;
        else
            inputStorage[resource]++;
    }

    public override void EndDay()
    {
        if (isWorking)
        {
            daysWorked += amountWorking;
            if (daysWorked >= buildingType.days)
            {
                amountWorking = 0;
                daysWorked -= buildingType.days;
                foreach (InputOutput item in buildingType.output)
                {
                    for (int i = 0; i < item.amount; i++)
                    {
                        outputStorage.Add(item.resource);
                        Debug.Log("add " + item.resource);
                    }
                }
            }
        }

        //if (outputStorage.Count != 0)
        //{
        //    OutputResource(outputStorage[0]);
        //    outputStorage.RemoveAt(0);
        //}

        if (workers > 0 && outputStorage.Count == 0)
        {
            //checks if building needs an input to work or not
            if (buildingType.input.Count == 0)
            {
                isWorking = true;
                amountWorking = workers;
            }
            else if (HasResources()) //if input is needed, check if they have the resources to start working for each worker
            {
                isWorking = true;
                amountWorking = workers;
                foreach (InputOutput item in buildingType.input)
                {
                    inputStorage[item.resource] -= item.amount;
                }
            }
        }
    }

    //public override void EndDay()
    //{
    //    if (isWorking) //checks if people are working in the building
    //    {
    //        daysWorked++;

    //        if (daysWorked >= buildingType.days) //checks if people have worked for enough days to produce the resource
    //        {
    //            for (int i = 0; i < amountWorking; i++) //adds an amount of resources to the storage depending on the amount of workers
    //            {
    //                foreach (InputOutput item in buildingType.output)
    //                {
    //                    OutputResource(item.resource);
    //                }
    //            }

    //            daysWorked = 0;
    //            isWorking = false;
    //            amountWorking = 0;
    //        }
    //    }

    //    if (!isWorking && workers > 0)
    //    {
    //        //checks if building needs an input to work or not

    //        if (buildingType.input.Count == 0) //if input isn't needed, all workers start working
    //        {
    //            isWorking = true;
    //            amountWorking = workers;
    //        }
    //        else //if input is needed...
    //        {
    //            for (int i = 0; i < workers; i++)
    //            {
    //                if (HasResources()) //... check if they have the resources to start working for each worker
    //                {
    //                    isWorking = true;
    //                    amountWorking++; //increase the amount of workers with enough resources
    //                    foreach (InputOutput item in buildingType.input)
    //                    {
    //                        storage[item.resource] -= item.amount;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    private bool HasResources() //check if the building has the resources in its storage
    {
        foreach (InputOutput item in buildingType.input)
        {
            if (inputStorage.ContainsKey(item.resource))
            {
                if (inputStorage[item.resource] < item.amount)
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
