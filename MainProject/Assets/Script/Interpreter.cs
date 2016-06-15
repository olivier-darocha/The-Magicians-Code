using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Interpreter : MonoBehaviour
{

    public string toolTag;
    public string variableTag;
    public string functionTag;


    List<GameObject> getToolsInProgramList()
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
        return GameObject.FindGameObjectsWithTag(toolTag).ToList();
    }



    bool conditionTest(GameObject condition)
    {
        if (condition.GetComponent<VariableId>().varId == "0") //not
            return false; //!condition.GetComponent<VariableId>().value;
        else if (condition.GetComponent<VariableId>().varId == "1") //variable
            return false;//condition.GetComponent<VariableId>().value;
        else
        {
            Debug.Log("error");
            return false;
        }
    }

    public void runInterpreter(string funcId)
    {
        StopAllCoroutines();
        StartCoroutine(funcId);
    }


    IEnumerator interpretRemplir()
    {
        GameObject condition = null;
        GameObject function = null;
        List<GameObject> toolList = getToolsInProgramList();
        int toolCount = toolList.Count;
        int i = 0;
        while (i < toolCount)
        {
            condition = null;
            function = null;
            GameObject currentTool = toolList[i];
            i++;
            // quelle outil utilisé par l'utilisateur?
            switch (currentTool.GetComponent<ConditionScript>().toolId)
            {
                case "0": //if
                    
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                        else if (child.gameObject.tag == functionTag)
                            function = child.gameObject;
                    }

                    // quelle fonction utilisé par l'utilisateur?
                    switch (function.GetComponent<FunctionId>().functionId)
                    {
                        case "0":
                            // animation de remplissage pour une petite dosage d'eau
                            // if = petite dose
                            // while = verre rempli
                            
                            break;
                        case "1":
                            break;
                        case "2":
                            break;
                        default:
                            break;
                    }


                    break;
                case "1": // else
                    // faire qqch
                    break;
                case "2": // else if
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                    }

                    if (conditionTest(condition))
                    {
                        // mettre un bool à true dans la fonction en question
                        // pour permettre l'override de la fonction de base par celle
                        // codé ici ?
                    }
                    break;
                case "3": // while
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == variableTag)
                            condition = child.gameObject;
                        else if (child.gameObject.tag == functionTag)
                            function = child.gameObject;
                    }

                    // quelle fonction utilisé par l'utilisateur?
                    switch (function.GetComponent<FunctionId>().functionId)
                    {
                        case "0":
                            // autorise l'exécution de la fonction 
                            // si la condition du if est à true (verre vide)
                            // sinon -> mauvais code de la part de l'user
                           // InteractionGlass.allowFill = conditionTest(condition);
                            break;
                        case "1":
                            break;
                        case "2":
                            break;
                        default:
                            break;
                    }
                    break;
                case "4": // for
                    break;
            }

        }
        toolList.Clear();
        yield return null;
    }

    IEnumerator interpretBoire()
    {
        List<GameObject> toolList = getToolsInProgramList();
        int toolCount = toolList.Count;
        int i = 0;
        while (i < toolCount)
        {
            GameObject currentTool = toolList[i];
            i++;
            // quelle outil utilisé par l'utilisateur?
            switch (currentTool.GetComponent<ConditionScript>().toolId)
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

                    // quelle fonction utilisé par l'utilisateur?
                    switch (function.GetComponent<FunctionId>().functionId)
                    {
                        case "0":

                            break;
                        case "1":
                            // autorise l'exécution de la fonction 
                            // si la condition du if est à true (verre plein)
                            // sinon -> mauvais code de la part de l'user
                            InteractionGlass.allowEmpty = conditionTest(condition);
                            break;
                        case "2":
                            break;
                        default:
                            break;
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
