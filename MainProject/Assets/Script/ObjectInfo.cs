using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ObjectInfo : MonoBehaviour
{

    public string ObjectName;
    public Dictionary<string, object> variablesList;

    void Start()
    {
        variablesList = new Dictionary<string, object>();
        createList();
    }

    void Update()
    {
        variablesList.Clear();
        createList();


    }

    void createList()
    {
        switch (ObjectName)
        {
            case "Verre":
                variablesList.Add("plein", InteractionGlass.filled);
                break;
            case "Chauffage":
                variablesList.Add("chauffe", true);
                break;
            default:
                break;
        }
    }
}
