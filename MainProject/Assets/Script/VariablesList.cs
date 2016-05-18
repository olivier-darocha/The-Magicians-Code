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
                updateList();
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
                updateList();
            }
            
        }
    }

    void updateList()
    {
        for (int i = 0; i < variablesObjects.Length; i++)
        {
            foreach (KeyValuePair<string, object> entry in variablesObjects[i].GetComponent<ObjectInfo>().variablesList)
            {
                list += entry.Key + " = " + entry.Value.ToString() + "\n";

            }
        }
        variablesList.GetComponent<Text>().text = list;
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
