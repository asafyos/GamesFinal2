using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject red;
    public GameObject yellow;

    private Text redCount;
    private Text yellowCount;

    // Start is called before the first frame update
    void Start()
    {
        redCount = red.GetComponentInChildren<Text>();
        yellowCount = yellow.GetComponentInChildren<Text>();

        ChangeSelection(DuckColor.Red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setColorCount(DuckColor color, int count)
    {
        switch (color)
        {
            case DuckColor.Red:
                redCount.text = "" + count;
                break;
            case DuckColor.Yellow:
                yellowCount.text = "" + count;
                break;
            default:
                break;
        }
    }

    public void ChangeSelection(DuckColor color)
    {
        switch (color)
        {
            case DuckColor.Red:
                red.GetComponent<Outline>().enabled = true;
                yellow.GetComponent<Outline>().enabled = false;
                break;
            case DuckColor.Yellow:
                red.GetComponent<Outline>().enabled = false;
                yellow.GetComponent<Outline>().enabled = true;
                break;
            default:
                break;
        }
    }
}
