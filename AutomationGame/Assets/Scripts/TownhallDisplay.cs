using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownhallDisplay : MonoBehaviour
{
    public static TownhallDisplay Instance;
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI outputAmount;


    private Townhall townhall;

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
        if (townhall == null)
        {
            townhall = Townhall.Instance;
        }
    }


    public void SelectTownhall()
    {
        if (CustomCursor.Instance != null && CustomCursor.Instance.destroyMode)
            return;

        animator.SetBool("IsOpen", true);
        BuildingDisplay.Instance.CloseDisplay();
        DungeonDisplay.Instance.CloseDisplay();
        UpdateTownhallDisplay();
    }

    public void Upgrade()
    {
        townhall.level++;
        GameManager.Instance.gold -= int.Parse(cost.text);
        cost.text = (int.Parse(cost.text) + 100).ToString(); //balance cost later
        UpdateTownhallDisplay();

    }

    public void CloseDisplay()
    {
        animator.SetBool("IsOpen", false);
    }


    public void UpdateTownhallDisplay()
    {


        buildingName.text = townhall.name;
        lvlText.text = townhall.level.ToString();

    }
}
