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

    public Dungeon dungeon;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (dungeon != null)
            return;

        try
        {
            if (dungeon == null)
            {
                dungeon = GameObject.Find("Dungeon").GetComponent<Dungeon>();
            }
        }
        catch (System.Exception)
        {

           // Debug.Log("No dungeon spawned yet");
        }
    }
    private void Start()
    {
        foreach (var item in allBuildings) //triggers all end of day stuff for the buildings
        {
            item.EndDay();
        }
        if (dungeon != null)
            dungeon.Explore();
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
            if (dungeon != null)
            {
                if(dungeon.adv > 0)
                {
                    dungeon.daysWorked++;
                }
                dungeon.Explore();
            }
            BuildingDisplay.Instance.UpdateDisplay();
        }
       
    }

    public void AddBuilding(GridObject buildingObject)
    {
        allBuildings.Add(buildingObject);
    }
}
