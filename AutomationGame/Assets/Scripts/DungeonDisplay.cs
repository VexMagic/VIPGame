using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonDisplay : MonoBehaviour
{
    public static DungeonDisplay Instance;
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI amountDisplay;

    public int RNG;


    public Dungeon dungeon;

    private void Start()
    {
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

            Debug.Log("No dungeon spawned yet");
        }
    }



    public void SelectDungeon()
    {
        animator.SetBool("IsOpen", true);
        BuildingDisplay.Instance.CloseDisplay();
        TownhallDisplay.Instance.CloseDisplay();
        UpdateDungeonDisplay();
    }


    public void CloseDisplay()
    {
        animator.SetBool("IsOpen", false);
    }

    public void Increase(int amount)
    {
        if (/*selectedBuilding == null || */!AdventurerersManager.Instance.HasFreeAdv(amount))
            return;

        /*        if (selectedBuilding.workerLimit >= dungeon.adv + amount)
                {*/
        dungeon.adv += amount;
        AdventurerersManager.Instance.AssignAdv(amount);
        UpdateDungeonDisplay();
        //}
    }

    public void Decrease(int amount)
    {
        if (dungeon == null)
            return;

        if (dungeon.adv >= amount)
        {
            dungeon.adv -= amount;
            AdventurerersManager.Instance.FreeAdv(amount);
            UpdateDungeonDisplay();
        }
    }

    public void Explore() // prototype
    {
        GameManager.Instance.gold += dungeon.adv * 200;

        if(int.Parse(amountDisplay.text) > 0)
        {
            RNG = Random.Range(0, 100);
            if (RNG <= 60) // 60%
            {
                //lose nothing
            }
            else if (RNG > 60 && RNG <= 80) //20%
            {
                dungeon.adv -= 1;

            }
            else if (RNG > 80 && RNG <= 95) //15%
            {
                dungeon.adv -= 2;

            }
            else // 5%
                dungeon.adv -= 3;           
        }
        UpdateDungeonDisplay();

    }
    public void UpdateDungeonDisplay()
    {

        if (dungeon == null)
            return;

        buildingName.text = dungeon.name;

        amountDisplay.text = dungeon.adv.ToString();

        if (int.Parse(amountDisplay.text) < 0)
        {
            amountDisplay.text = "0";
        }
        /*        progress.value = selectedBuilding.daysWorked;
                progress.maxValue = selectedBuilding.buildingType.days;*/


        AdventurerersManager.Instance.UpdateDisplay();

    }
}
