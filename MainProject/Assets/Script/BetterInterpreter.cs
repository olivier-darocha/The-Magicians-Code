using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BetterInterpreter : MonoBehaviour
{
    private bool interpreterRunned;
    public string toolTag;
    public string conditionTag;
    public string functionTag;
    private GameObject parent;
    private GameObject use_glass;
    private GameObject use_heater;

    void Start()
    {
        interpreterRunned = false;
        use_glass = GameObject.Find("Use_Glass");
        use_heater = GameObject.Find("House_Heater");
    }

    public void defineParent(string name)
    {
        parent = GameObject.Find(name);
    }

    List<GameObject> getToolsInProgramList()
    {
        List<GameObject> temp = new List<GameObject>();
        Debug.Log(parent.name);
        foreach (Transform o in parent.transform)
        {
            if (o.gameObject.tag.Equals(toolTag))
            {
                temp.Add(o.gameObject);
            }

        }
        temp.AddRange(GameObject.FindGameObjectsWithTag("freeFunc"));
        SortedDictionary<int, GameObject> dic = new SortedDictionary<int, GameObject>();
        foreach(GameObject o in temp)
        {
            if(o.tag == toolTag)
                dic.Add(o.GetComponent<ConditionScript>().order, o);
            else
                dic.Add(o.GetComponent<FunctionId>().order, o);
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
            // s'il y a des fonctions hors des conditions
            if (currentTool.tag.Equals("freeFunc"))
            {
                switch (currentTool.GetComponent<FunctionId>().functionId)
                {
                    case "16":
                        fillGlass();
                        yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                        break;
                    case "17": // ajouter bûche
                        addLog();
                        yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
                        break;
                    case "19": // ajouter beurre
                        addButter();
                        break;
                    case "20": // ajouter farine
                        addFlour();
                        break;
                    case "21": // ajouter lait
                        addMilk();
                        break;
                    case "18": // ajouter pommes
                        addApple();
                        break;
                    default:
                        break;
                }
            }
            else
            {
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
                                switch (currentTool.transform.GetChild(1).GetChild(0).GetComponent<FunctionId>().functionId)
                                {
                                    case "16":
                                        fillGlass();
                                        yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                                        break;
                                    case "17": // ajouter bûche
                                        addLog();
                                        yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
                                        break;
                                    case "19": // ajouter beurre
                                        addButter();
                                        break;
                                    case "20": // ajouter farine
                                        addFlour();
                                        break;
                                    case "21": // ajouter lait
                                        addMilk();
                                        break;
                                    case "18": // ajouter pommes
                                        addApple();
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
                            switch (currentTool.transform.GetChild(1).GetChild(0).GetComponent<FunctionId>().functionId)
                            {
                                case "16":
                                    fillGlass();
                                    yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                                    break;
                                case "17": // ajouter bûche
                                    addLog();
                                    yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
                                    break;
                                case "19": // ajouter beurre
                                    addButter();
                                    break;
                                case "20": // ajouter farine
                                    addFlour();
                                    break;
                                case "21": // ajouter lait
                                    addMilk();
                                    break;
                                case "18": // ajouter pommes
                                    addApple();
                                    break;
                                default:
                                    break;
                            }
                            l++;
                        }

                        break;
                    case "2": // else if
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
                                switch (currentTool.transform.GetChild(1).GetChild(0).GetComponent<FunctionId>().functionId)
                                {
                                    case "16":
                                        fillGlass();
                                        yield return StartCoroutine(use_glass.GetComponent<InteractionGlass>().fillGlass());
                                        break;
                                    case "17": // ajouter bûche
                                        addLog();
                                        yield return StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
                                        break;
                                    case "19": // ajouter beurre
                                        addButter();
                                        break;
                                    case "20": // ajouter farine
                                        addFlour();
                                        break;
                                    case "21": // ajouter lait
                                        addMilk();
                                        break;
                                    case "18": // ajouter pommes
                                        addApple();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        break;
                }
            }

        }
        yield return null;// StartCoroutine(use_heater.GetComponent<InteractionHeater>().addLog());
    }

    void fillGlass()
    {
        
        InteractionGlass.allowFill = true;
        if (InteractionGlass.fillMode < 5)
            InteractionGlass.fillMode++;
    }


    void addLog()
    {
        InteractionHeater.logNum++;
        if (InteractionHeater.triggerNum < 4)
            InteractionHeater.triggerNum++;
    }

    void addButter()
    {
        InteractionPie.butterQuantity += 10;
    }

    void addMilk()
    {
        InteractionPie.milkQuantity++;
    }

    void addFlour()
    {
        InteractionPie.flourQuantity += 10;
    }

    void addApple()
    {
        InteractionPie.appleQuantity++;
    }

}