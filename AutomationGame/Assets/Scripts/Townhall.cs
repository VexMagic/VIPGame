using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townhall : MonoBehaviour
{
    public static Townhall Instance;


    [SerializeField] public int level;

    // Start is called before the first frame update
    [SerializeField] public GameObject highlight;
    [SerializeField] public GameObject clickedHighlight;


    void Start()
    {
        level = 1;

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

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
        TownhallDisplay.Instance.SelectTownhall();
    }
}
