using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    void getToolsInProgramList()
    {
        // Attention à l'ordre dans lequel les tools sont dans la liste
        // autre solution?
        toolList = GameObject.FindGameObjectsWithTag(toolTag).ToList();
    }

    IEnumerator interpretProgram()
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
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                    }

                    if (conditionTest(condition)) {
                        // mettre un bool à true dans la fonction en question
                        // pour permettre l'override de la fonction de base par celle
                        // codé ici ?
                        yield return null;
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

    void runInterpreter()
    {
        StartCoroutine("interpretProgram");
    }


    bool conditionTest(GameObject condition)
    {
        if (condition.GetComponent<ToolId>().id == "1") //not
            return !condition.GetComponent<ToolId>().value;
        else if (condition.GetComponent<ToolId>().id == "2") //variable
            return condition.GetComponent<ToolId>().value;
        else
        {
            Debug.Log("error");
            return false;
        }
    }
}
