using UnityEngine;
using System.Collections;

public class InteractionPie : MonoBehaviour {

    public static GameObject pie;
    public static GameObject radioactivePie;

    public static int butterQuantity;
    public static int flourQuantity;
    public static int milkQuantity;
    public static int appleQuantity;

    public static bool doughState;
    public static bool pieState;
    public static bool radioactivePieState;

    void Start()
    {
        radioactivePie = GameObject.Find("radioactive_pie");
        pie = GameObject.Find("tarte");
        pie.SetActive(false);
        radioactivePie.SetActive(false);
        radioactivePieState = false;
        pieState = false;
        doughState = false;
        butterQuantity = 0;
        flourQuantity = 200;
        milkQuantity = 0;
        appleQuantity = 0;
    }

    void Update()
    {
        radioactivePie.SetActive(radioactivePieState);
        pie.SetActive(pieState);
        if(pieState)
            GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[7] = "Prête";
        else
            GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[7] = "Pas prête";

        if (butterQuantity >= 80 && butterQuantity <= 120 && flourQuantity >= 250 && flourQuantity <= 350 && milkQuantity >= 7 && milkQuantity <= 13)
            doughState = true;
        else
            doughState = false;
    }

    public void eat()
    {
        if (pieState)
        {
            pie.SetActive(false);
        }
    }
    
}