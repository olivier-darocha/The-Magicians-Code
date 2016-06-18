using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BetterInterpreter : MonoBehaviour
{
    int a;
    private bool interpreterRunned;
    public string toolTag;
    public string conditionTag;
    public string functionTag;
    private GameObject use_glass;
    private GameObject use_heater;

    void Start()
    {
        a = 0;
        interpreterRunned = false;
        use_glass = GameObject.Find("Use_Glass");
        use_heater = GameObject.Find("Use_Heater");
    }
    List<GameObject> getToolsInProgramList()
    {
        List<GameObject> temp = GameObject.FindGameObjectsWithTag(toolTag).ToList();
        SortedDictionary<int, GameObject> dic = new SortedDictionary<int, GameObject>();
        foreach(GameObject o in temp)
        {
            dic.Add(o.GetComponent<ConditionScript>().order, o);
        }
        return new List<GameObject>(dic.Values);
    }

    public void runInterpreter()
    {
        if (interpreterRunned)
        {
            for(int i=0;i<100; i++) { }
            
        }
        StopAllCoroutines();
        StartCoroutine("interpret");

    }

    bool conditionFunction(GameObject tool)
    {
        tool.GetComponent<ConditionScript>().Init(tool, conditionTag);
        return tool.GetComponent<ConditionScript>().getConditionValue();
    }



    IEnumerator interpret()
    {
        interpreterRunned = true;
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
            // quel outil utilisé par l'utilisateur?
            switch (currentTool.GetComponent<ConditionScript>().toolId)
            {
                case "0": //if
                    if (conditionFunction(currentTool)) // condition à true on exécute le programme situé dans le if
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
                                    fillGlass();
                                    yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                                    break;
                                case "1": // ajouter bûche
                                    addLog();
                                    yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
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
                        InteractionGlass.allowFill = false;
                        break; // condition à false, on passe à la suite du programme
                    }

                    break;
                case "1": // else
                          
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
                    int l = 0;
                    while (l < codeContent.Count)
                    {
                        switch (codeContent[l].GetComponent<FunctionId>().functionId)
                        {
                            case "0":
                                fillGlass();
                                yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                                break;
                            case "1": // ajouter bûche
                                addLog();
                                yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
                                break;
                            case "2":
                                break;
                            default:
                                break;
                        }
                        l++;
                    }

                    break;
                case "2":
                    break;
                case "3": // while
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
                    while (conditionFunction(currentTool))
                    {
                        for (int k = 0; k < codeContent.Count; k++)
                        {
                            switch (codeContent[k].GetComponent<FunctionId>().functionId)
                            {
                                case "0":
                                    fillGlass();
                                    yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                                    break;
                                case "1":
                                    addLog();
                                    yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
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
        yield return null;// StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
    }

    void fillGlass()
    {
        InteractionGlass.allowFill = true;
        if (InteractionGlass.fillMode < 4)
            InteractionGlass.fillMode++;
        else
            InteractionGlass.overflow = true;
    }


    void addLog()
    {
        InteractionHeater.logNum++;
        if (InteractionHeater.triggerNum < 4)
            InteractionHeater.triggerNum++;
    }
}





//IEnumerator interpretRemplir()
//{
//    GameObject functionContainer = null;
//    List<GameObject> codeContent = new List<GameObject>();
//    List<GameObject> toolList = getToolsInProgramList();
//    int toolCount = toolList.Count;
//    int i = 0;
//    while (i < toolCount)
//    {
//        codeContent.Clear();
//        functionContainer = null;
//        GameObject currentTool = toolList[i];
//        i++;
//        // quelle outil utilisé par l'utilisateur?
//        switch (currentTool.GetComponent<ConditionScript>().toolId)
//        {
//            case "0": //if
//                InteractionGlass.toolUsed = 0;

//                if (conditionFunction(currentTool)) // condition à true on exécute le programme situé dans le if
//                {

//                    // cherche les enfants dans le sous-dossier 'functions' dans le dossier 'if'
//                    foreach (Transform child in currentTool.transform)
//                    {
//                        if (child.gameObject.tag == functionTag)
//                        {
//                            functionContainer = child.gameObject;
//                            foreach (Transform c in functionContainer.transform)
//                            {
//                                codeContent.Add(c.gameObject);
//                            }
//                        }
//                    }
//                    int j = 0;
//                    while (j < codeContent.Count)
//                    {
//                        switch (codeContent[j].GetComponent<FunctionId>().functionId)
//                        {
//                            case "0":// remplir une dose de verre
//                                fillGlass();
//                                break;
//                            case "1":
//                                break;
//                            case "2":
//                                break;
//                            default:
//                                break;
//                        }
//                        j++;
//                    }

//                }
//                else
//                {
//                    InteractionGlass.allowFill = false;
//                    break; // condition à false, on passe à la suite du programme
//                }

//                break;
//            case "1": // else
//                // faire qqch
//                break;
//            case "2":
//                break;
//            case "3": // while
//                InteractionGlass.toolUsed = 3;
//                // condition à true on exécute le programme situé dans le if
//                foreach (Transform child in currentTool.transform)
//                {
//                    if (child.gameObject.tag == functionTag)
//                    {
//                        functionContainer = child.gameObject;
//                        foreach (Transform c in functionContainer.transform)
//                        {
//                            codeContent.Add(c.gameObject);
//                        }
//                    }
//                }
//                while (conditionFunction(currentTool))
//                {
//                    for (int k = 0; k < codeContent.Count; k++)
//                    {
//                        switch (codeContent[k].GetComponent<FunctionId>().functionId)
//                        {
//                            case "0": // remplir une dose de verre
//                                fillGlass();
//                                break;
//                            case "1":
//                                break;
//                            case "2":
//                                break;
//                            default:
//                                break;
//                        }
//                    }
//                }
//                break;
//            case "4": // for
//                break;
//        }

//    }
//    yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
//}


    