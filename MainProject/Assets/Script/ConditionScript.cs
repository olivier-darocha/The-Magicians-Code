using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConditionScript : MonoBehaviour
{

    public string toolId;
    public int order;
    private string conditionTag;
    private GameObject condition;
    private List<GameObject> conditionList;

    private bool valueBool;
    private int valueInt;
    private float valueFloat;

    public bool getConditionValue()
    {
        conditionList = getConditions();
        int conditionListCount = conditionList.Count;
        
        switch (conditionList[0].GetComponent<VariableId>().varId)
        {
            case "0": // eau
                valueFloat = InteractionGlass.quantity;
                return interpretFloat(valueFloat, conditionList[1], conditionList[2]);
            case "1": // chauffage/feu
                valueBool = InteractionHeater.fireState;
                return valueBool;
            case "2": // neige
                valueInt = int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[2]);
                return interpretInt(valueInt, conditionList[1], conditionList[2]);
            default:
                break;
        }
        return true;
    }

    

    public void Init(GameObject tool, string tag)
    {
        condition = tool;
        conditionTag = tag;
    }


    List<GameObject> getConditions()
    {
        GameObject conditionContainer;
        List<GameObject> list = new List<GameObject>();
        foreach (Transform child in condition.transform)
        {
            if (child.gameObject.tag == conditionTag)
            {
                conditionContainer = child.gameObject;
                foreach (Transform c in conditionContainer.transform)
                {
                    list.Add(c.gameObject);
                }
            }
        }
        if (list.Count == 31)
            list = sortList(list);

        return list;
    }

    List<GameObject> sortList(List<GameObject> list)
    {
        List<GameObject> temp = new List<GameObject>(3);
        foreach(GameObject obj in list)
        {
            if (obj.tag == "var")
                temp[0] = obj;
            else if (obj.tag == "sign")
                temp[1] = obj;
            else if (obj.tag == "value")
                temp[2] = obj;
        }
        return temp;
    }


    bool interpretFloat(float var, GameObject sign, GameObject value)
    {
        float val = value.GetComponent<Value>().valueFloat;
        switch (sign.GetComponent<Sign>().sign)
        {
            case "==":
                return (var == val);
            case "!=":
                return (var != val);
            case "<":
                return (var < val);
            case "<=":
                return (var <= val);
            case ">":
                return (var > val);
            case ">=":
                return (var >= val);
            default:
                return false;
        }
    }


    bool interpretInt(int var, GameObject sign, GameObject value)
    {
        float val = value.GetComponent<Value>().valueInt;
        switch (sign.GetComponent<Sign>().sign)
        {
            case "==":
                return (var == val);
            case "!=":
                return (var != val);
            case "<":
                return (var < val);
            case "<=":
                return (var <= val);
            case ">":
                return (var > val);
            case ">=":
                return (var >= val);
            default:
                return false;
        }
    }
}
