using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Interpreter : MonoBehaviour
{

    private List<GameObject> toolList;
    public string toolTag;
    public string variableTag;
    public string functionTag;

    // Use this for initialization
    void Start()
    {
        toolList = new List<GameObject>();
        toolList.Clear();
    }


    void Update()
    {
        runInterpreter();
    }
    void getToolsInProgramList()
    {

        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //


        // Attention à l'ordre dans lequel les tools sont dans la liste
        // autre solution?
        toolList = GameObject.FindGameObjectsWithTag(toolTag).ToList();
    }

    

    bool conditionTest(GameObject condition)
    {
        if (condition.GetComponent<VariableId>().id == "0") //not
            return !condition.GetComponent<VariableId>().value;
        else if (condition.GetComponent<VariableId>().id == "1") //variable
            return condition.GetComponent<VariableId>().value;
        else
        {
            Debug.Log("error");
            return false;
        }
    }

    void runInterpreter()
    {
        string funcName = GameObject.Find("Label").GetComponent<Text>().text;
        switch(funcName)
        {
            case "Remplir":
                StartCoroutine("interpretRemplir");
                break;
            case "Boire":
                StartCoroutine("interpretBoire");
                break;
            default:
                break;
        }     
    }


    IEnumerator interpretRemplir()
    {
        getToolsInProgramList();
        int toolCount = toolList.Count;
        int i = 0;
        while (i < toolCount)
        {
            GameObject currentTool = toolList[i];
            i++;
            switch (currentTool.GetComponent<ToolId>().id)
            {
                case "0": //if
                    GameObject condition = null;
                    GameObject function = null;
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                        else if (child.gameObject.tag == functionTag)
                            function = child.gameObject;
                    }
                    if (function.GetComponent<FunctionId>().id == 0)
                        InteractionGlass.allowFill = !conditionTest(condition);
                    else
                    { //do something else
                    }

                    break;
                case "1": // else
                    // faire qqch
                    break;
                case "2": // else if
                    GameObject otherCondition = null;
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                    }

                    if (conditionTest(otherCondition))
                    {
                        // mettre un bool à true dans la fonction en question
                        // pour permettre l'override de la fonction de base par celle
                        // codé ici ?
                        yield return null;
                    }
                    break;
                case "3": // for
                    break;
                case "4": // while
                    break;
            }

        }
        toolList.Clear();
        yield return null;
    }

    IEnumerator interpretBoire()
    {
        getToolsInProgramList();
        int toolCount = toolList.Count;
        int i = 0;
        while (i < toolCount)
        {
            GameObject currentTool = toolList[i];
            i++;
            switch (currentTool.GetComponent<ToolId>().id)
            {
                case "0": //if
                    GameObject condition = null;
                    GameObject function = null;
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                        else if (child.gameObject.tag == functionTag)
                            function = child.gameObject;
                    }
                    if (function.GetComponent<FunctionId>().id == 0)
                    {
                        // do domething
                    }
                    else
                    { //do something else
                    }

                    break;
                case "1": // else
                    // faire qqch
                    break;
                case "2": // else if
                    GameObject otherCondition = null;
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                    }

                    if (conditionTest(otherCondition))
                    {
                        // mettre un bool à true dans la fonction en question
                        // pour permettre l'override de la fonction de base par celle
                        // codé ici ?
                        yield return null;
                    }
                    break;
                case "3": // for
                    break;
                case "4": // while
                    break;
            }

        }
        toolList.Clear();
        yield return null;
    }

}
