using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDisplay : MonoBehaviour
{
    public static DungeonDisplay Instance;
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private Slider progress;
    [SerializeField] private TextMeshProUGUI advAmount;
    [SerializeField] private TextMeshProUGUI totalGoldDisplay;
    int totalGold;



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

    public void UpdateDungeonDisplay()
    {

        if (dungeon == null)
            return;

        totalGold = dungeon.adv * dungeon.gold;

        buildingName.text = dungeon.name;

        advAmount.text = dungeon.adv.ToString();

        progress.value = dungeon.daysWorked;
        progress.maxValue = dungeon.daysToYieldGold;
        totalGoldDisplay.text = totalGold.ToString();
        /*        progress.value = selectedBuilding.daysWorked;
                progress.maxValue = selectedBuilding.buildingType.days;*/


        AdventurerersManager.Instance.UpdateDisplay();

    }
}
