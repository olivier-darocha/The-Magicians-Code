using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BetterInterpreter : MonoBehaviour
{

    public string toolTag;
    public string variableTag;
    public string functionTag;
    private GameObject use_glass;
    private GameObject use_heater;


    void Start()
    {
        use_glass = GameObject.Find("Use_Glass");
        use_heater = GameObject.Find("Use_Heater");
    }
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

    public void runInterpreter(string funcId)
    {
        StopAllCoroutines();
        StartCoroutine(funcId);
    }

    bool ifFunction(GameObject ifTool)
    {
        return true;// ifTool.GetComponent<ifScript>().value;
    }

    bool whileFunction(GameObject whileTool)
    {
        return false;// whileTool.GetComponent<whileScript>().value;
    }

    IEnumerator interpretRemplir()
    {
        GameObject functionContainer = null;
        List<GameObject> codeContent = new List<GameObject>();
        List<GameObject> toolList = getToolsInProgramList();
        int toolCount = toolList.Count;
        int i = 0;
        while (i < toolCount)
        {
            codeContent.Clear();
            functionContainer = null;
            GameObject currentTool = toolList[i];
            i++;
            // quelle outil utilisé par l'utilisateur?
            switch (currentTool.GetComponent<ToolId>().id)
            {
                case "0": //if
                    InteractionGlass.toolUsed = 0;
                    if (ifFunction(currentTool)) // condition à true on exécute le programme situé dans le if
                    {

                        // cherche les enfants dans le sous-dossier 'functions' dans le dossier 'if'
                        foreach (Transform child in currentTool.transform)
                        {
                            if (child.gameObject.tag == functionTag)
                            {
                                functionContainer = child.gameObject;
                                foreach (Transform c in functionContainer.transform)
                                {
                                    codeContent.Add(c.gameObject);
                                }
                            }
                        }
                        int j = 0;
                        while (j < codeContent.Count)
                        {
                            switch (codeContent[j].GetComponent<FunctionId>().functionId)
                            {
                                case "0":// remplir une dose de verre
                                    InteractionGlass.allowFill = true;
                                    if (InteractionGlass.fillMode < 3)
                                        InteractionGlass.fillMode++;
                                    else
                                        InteractionGlass.overflow = true;
                                    break;
                                case "1":
                                    break;
                                case "2":
                                    break;
                                default:
                                    break;
                            }
                            j++;
                        }

                    }
                    else
                    {
                        Debug.Log("15");
                        InteractionGlass.allowFill = false;
                        break; // condition à false, on passe à la suite du programme
                    }

                    break;
                case "1": // else
                    // faire qqch
                    break;
                case "2":
                    break;
                case "3": // while
                    InteractionGlass.toolUsed = 3;
                    // condition à true on exécute le programme situé dans le if
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == functionTag)
                        {
                            functionContainer = child.gameObject;
                            foreach (Transform c in functionContainer.transform)
                            {
                                codeContent.Add(c.gameObject);
                            }
                        }
                    }
                    while (whileFunction(currentTool))
                    {
                        for (int k = 0; k < codeContent.Count; k++)
                        {
                            switch (codeContent[k].GetComponent<FunctionId>().functionId)
                            {
                                case "0": // remplir une dose de verre
                                    if (InteractionGlass.fillMode < 3)
                                        InteractionGlass.fillMode++;
                                    else
                                        InteractionGlass.overflow = true;
                                    break;
                                case "1":
                                    break;
                                case "2":
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case "4": // for
                    break;
            }

        }
        yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
    }


    IEnumerator interpretAddLog()
    {
        GameObject functionContainer = null;
        List<GameObject> codeContent = new List<GameObject>();
        List<GameObject> toolList = getToolsInProgramList();
        int toolCount = toolList.Count;
        int i = 0;
        while (i < toolCount)
        {
            codeContent.Clear();
            functionContainer = null;
            GameObject currentTool = toolList[i];
            i++;
            // quelle outil utilisé par l'utilisateur?
            switch (currentTool.GetComponent<ToolId>().id)
            {
                case "0": //if
                    InteractionGlass.toolUsed = 0;
                    if (ifFunction(currentTool)) // condition à true on exécute le programme situé dans le if
                    {

                        // cherche les enfants dans le sous-dossier 'functions' dans le dossier 'if'
                        foreach (Transform child in currentTool.transform)
                        {
                            if (child.gameObject.tag == functionTag)
                            {
                                functionContainer = child.gameObject;
                                foreach (Transform c in functionContainer.transform)
                                {
                                    codeContent.Add(c.gameObject);
                                }
                            }
                        }
                        int j = 0;
                        while (j < codeContent.Count)
                        {
                            switch (codeContent[j].GetComponent<FunctionId>().functionId)
                            {
                                case "0":
                                    // animation de remplissage pour une petite dosage d'eau
                                    // if = petite dose
                                    // while = verre rempli  
                                    break;
                                case "1": // ajouter bûche
                                    Debug.Log("78");
                                    InteractionHeater.logNum++;
                                    if(InteractionHeater.triggerNum <= 4)
                                        InteractionHeater.triggerNum++;
                                    break;
                                case "2":
                                    break;
                                default:
                                    break;
                            }
                            j++;
                        }

                    }
                    break;
                case "1": // else
                    // faire qqch
                    break;
                case "2":
                    break;
                case "3": // while
                    InteractionGlass.toolUsed = 3;
                    // condition à true on exécute le programme situé dans le if
                    foreach (Transform child in currentTool.transform)
                    {
                        if (child.gameObject.tag == functionTag)
                        {
                            functionContainer = child.gameObject;
                            foreach (Transform c in functionContainer.transform)
                            {
                                codeContent.Add(c.gameObject);
                            }
                        }
                    }
                    while (whileFunction(currentTool))
                    {
                        for (int k = 0; k < codeContent.Count; k++)
                        {
                            switch (codeContent[k].GetComponent<FunctionId>().functionId)
                            {
                                case "0":
                                    break;
                                case "1":
                                    
                                    InteractionHeater.logNum++;
                                    if (InteractionHeater.triggerNum <= 5)
                                        InteractionHeater.triggerNum++;
                                    break;
                                case "2":
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case "4": // for
                    break;
            }

        }

        yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
    }


}
