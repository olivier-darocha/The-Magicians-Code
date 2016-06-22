using UnityEngine;
using System.Collections.Generic;

public class ConditionScript : MonoBehaviour
{

    public string toolId;
    public int order;
    private string conditionTag;
    private GameObject condition;
    private GameObject neg;
    private GameObject glass;
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
            case "6": // eau
                if (InteractionGlass.quantity == 1 && conditionList[2].GetComponent<Value>().valueFloat > 1)
                {
                    InteractionGlass.fillMode = 4;
                    StartCoroutine(glass.GetComponent<InteractionGlass>().fillGlass());
                    return false;
                }
                else
                    return interpretFloat(InteractionGlass.quantity, conditionList[1], conditionList[2]);
            case "4": // chauffage/feu
                return interpretBool(InteractionHeater.fireState);
            case "5": // neige
                return interpretInt(int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[2]), conditionList[1], conditionList[2]);
            case "8": // pommes
                return interpretInt(int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[3]), conditionList[1], conditionList[2]);
            case "9": // farine
                return interpretInt(int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[4]), conditionList[1], conditionList[2]);
            case "10": // beurre
                return interpretInt(int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[5]), conditionList[1], conditionList[2]);
            case "11": // lait
                return interpretInt(int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[6]), conditionList[1], conditionList[2]);
            case "7": // état de la tarte
                return interpretInt(int.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[7]), conditionList[1], conditionList[2]);
            default:
                break;
        }
        return true;
    }

    

    public void Init(GameObject tool, string tag)
    {
        condition = tool;
        conditionTag = tag;
        neg = GameObject.FindGameObjectWithTag("var");
        glass = GameObject.Find("Use_Glass");
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
                    if (c.gameObject.tag != "negation")
                    {
                        list.Add(c.gameObject);
                    }
                }
            }
        }
        
        if (list.Count == 3)
            list = sortList(list);

        return list;
    }

    List<GameObject> sortList(List<GameObject> list)
    {
        List<GameObject> temp = list;
        foreach (GameObject obj in list)
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

    bool interpretBool(bool boolVar)
    {
        if (neg.GetComponent<VariableId>().negationState)
            return boolVar;
        else
            return !boolVar;
    }
}
