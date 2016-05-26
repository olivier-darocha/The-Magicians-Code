using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class VariablesList : MonoBehaviour {

    private GameObject[] variablesObjects; // contient la liste des variables à afficher
    private GameObject variablesList;
    private string list;
    private bool variablesDisplayed;

    // Use this for initialization
    void Start () {
        variablesInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!variablesDisplayed)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {

                variablesDisplayed = !variablesDisplayed;
                clearList();
                StartCoroutine("updateList");
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                clearList();
                variablesDisplayed = !variablesDisplayed;
            }
            else
            {
                clearList();
                StartCoroutine("updateList");
            }
            
        }
    }

    IEnumerator updateList()
    {
        int i;
        for (i = 0; i < variablesList.GetComponent<VariablesInfo>().VariablesName.Length; i++)
        {
                list += variablesList.GetComponent<VariablesInfo>().VariablesName[i] + " = " + variablesList.GetComponent<VariablesInfo>().VariablesValue[i] + "\n";
        }
        variablesList.GetComponent<Text>().text = list;
        yield return null;
    }
    
    void clearList()
    {
        list = "";
        variablesList.GetComponent<Text>().text = "";
    }

    void variablesInit()
    {
        variablesDisplayed = false;
        variablesList = GameObject.Find("Variables_List");
        list = "";
        variablesObjects = GameObject.FindGameObjectsWithTag("Programmable");
    }
}
