using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int adv;
    public int daysWorked;
    [SerializeField] public GameObject highlight;
    [SerializeField] private GameObject clickedHighlight;

    public int daysToYieldGold = 5;
    public int gold = 100;
    public int RNG;
    void Start()
    {
    }

    private void Update()
    {
    }


    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        clickedHighlight.SetActive(false);

    }

    private void OnMouseUpAsButton()
    {
        clickedHighlight.SetActive(false);
        highlight.SetActive(true);

    }

    private void OnMouseDown()
    {
        clickedHighlight.SetActive(true);
        highlight.SetActive(false);
        DungeonDisplay.Instance.SelectDungeon();
    }
    public void Explore() // prototype
    {
        if(daysWorked >= daysToYieldGold)
        {
            GameManager.Instance.gold += adv * gold;

            if (adv > 0)
            {
                RNG = Random.Range(0, 100);
                if (RNG <= 60) // 60%
                {
                    //lose nothing
                }
                else if (RNG > 60 && RNG <= 80) //20%
                {
                    adv -= 1;

                }
                else if (RNG > 80 && RNG <= 95) //15%
                {
                    adv -= 2;

                }
                else // 5%
                    adv -= 3;
            }

            if (adv < 0)
            {
                adv = 0;
            }

            daysWorked = 0;
        }
      
        DungeonDisplay.Instance.UpdateDungeonDisplay();

    }

   
}
