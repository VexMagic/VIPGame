using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [SerializeField] private Building buildingType;
    [SerializeField] private int workers;
    [SerializeField] private int workerLimit;

    [SerializeField] private int daysWorked;
    [SerializeField] private bool isWorking;
    [SerializeField] private int amountWorking;

    private void Start()
    {
        DayManager.instance.AddBuilding(this);
    }

    public void EndDay()
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
                        Storage.instance.GainResource(item.resource, item.amount);
                    }
                }

                daysWorked = 0;
                isWorking = false;
                amountWorking = 0;
            }
        }

        if (!isWorking)
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
                    if (buildingType.HasResources()) //... check if they have the resources to start working for each worker
                    {
                        isWorking = true;
                        amountWorking++; //increase the amount of workers with enough resources
                        foreach (InputOutput item in buildingType.input)
                        {
                            Storage.instance.SpendResource(item.resource, item.amount); //spend the resources
                        }
                    }
                }
            }
        }
    }
}
