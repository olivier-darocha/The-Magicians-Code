using UnityEngine;
using System.Collections.Generic;

public class ConditionScript : MonoBehaviour
{

    public string toolId;
    public int order;
    private string conditionTag;
    private GameObject condition;
    private GameObject glass;
    private List<GameObject> conditionList;

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
                return interpretBool(InteractionHeater.fireState, conditionList[2].GetComponent<Value>().valueBool);
            case "5": // neige
                return interpretFloat(float.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[2]), conditionList[1], conditionList[2]);
            case "8": // pommes
                return interpretFloat(float.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[3]), conditionList[1], conditionList[2]);
            case "9": // farine
                return interpretFloat(float.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[4]), conditionList[1], conditionList[2]);
            case "10": // beurre
                return interpretFloat(float.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[5]), conditionList[1], conditionList[2]);
            case "11": // lait
                return interpretFloat(float.Parse(GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[6]), conditionList[1], conditionList[2]);
            case "7": // état de la tarte
                return interpretBool(InteractionPie.doughState, conditionList[2].GetComponent<Value>().valueBool);
            default:
                break;
        }
        return true;
    }

    

    public void Init(GameObject tool, string tag)
    {
        condition = tool;
        conditionTag = tag;
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
                    list.Add(c.gameObject);
                }
            }
        }
        List<GameObject> new_list = new List<GameObject>();
        new_list = sortList(list);

        return new_list;
    }

    List<GameObject> sortList(List<GameObject> list)
    {
        GameObject[] temp = new GameObject[3];
        foreach (GameObject obj in list)
        {
            
            switch (obj.tag)
            {
                case "var":
                    temp[0] = obj;
                    break;
                case "sign":
                    temp[1] = obj;
                    break;
                case "value":
                    temp[2] = obj;
                    break;
                default:
                    break;
            }
        }
        
        return new List<GameObject>(temp);
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


    bool interpretBool(bool boolVar, bool valBool)
    {
        return boolVar == valBool;
    }
}
