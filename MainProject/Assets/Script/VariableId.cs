﻿using UnityEngine;


public class VariableId : MonoBehaviour
{
    public string id;
    public int idVar;
    public bool value;

    void Update()
    {
        switch (idVar) {
            case 0:
                value = InteractionGlass.filled;
                break;
            default:
                break;
    }
    }

}