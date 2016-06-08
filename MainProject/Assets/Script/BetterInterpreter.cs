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
    public static bool interpretEnd = false;
    private GameObject use_glass;


    void Start()
    {
        use_glass = GameObject.Find("Use_Glass");
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
        interpretEnd = false;
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
                    InteractionObjects.toolUsed = 0;
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
                                    InteractionObjects.allowFill = true;
                                    if (InteractionObjects.fillMode < 3)
                                        InteractionObjects.fillMode++;
                                    else
                                        InteractionObjects.overflow = true;
                                    Debug.Log("interpreter "+InteractionObjects.fillMode);
                                    Debug.Log("interpreter " + interpretEnd);
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
                        InteractionObjects.allowFill = false;
                        break; // condition à false, on passe à la suite du programme
                    }

                    break;
                case "1": // else
                    // faire qqch
                    break;
                case "2":
                    break;
                case "3": // while
                    InteractionObjects.toolUsed = 3;
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
                                    // animation de remplissage pour une petite dosage d'eau
                                    // if = petite dose
                                    // while = verre rempli
                                    if (InteractionObjects.fillMode < 3)
                                        InteractionObjects.fillMode++;
                                    else
                                        InteractionObjects.overflow = true;
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
        interpretEnd = true;
        yield return StartCoroutine(use_glass.GetComponent<InteractionObjects>().fillGlass());
    }


}
