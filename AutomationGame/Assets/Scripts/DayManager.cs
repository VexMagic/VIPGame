using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;

    [SerializeField] private float dayTimer;
    private float timer;
    private int dayNumber;

    private List<GridObject> allBuildings = new List<GridObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        foreach (var item in allBuildings) //triggers all end of day stuff for the buildings
        {
            item.EndDay();
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= dayTimer) //checks if a day has passed
        {
            dayNumber++;
            //Debug.Log("End day " + dayNumber);
            timer -= dayTimer;
            foreach (var item in allBuildings) //triggers all end of day stuff for the buildings
            {
                item.EndDay();
            }
            BuildingDisplay.instance.UpdateDisplay();
        }
    }

    public void AddBuilding(GridObject buildingObject)
    {
        allBuildings.Add(buildingObject);
    }
}
