using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class GameplayMenuSetup : MonoBehaviour
{
    public static int orderCount = 0;
    public static bool interfaceUseActive;
    public static bool interfaceProgramActive;
    private bool inputE;
    private bool inputA;
    private int panelId;
    private GameObject FPSControl;
    private GameObject overlayUse;
    private GameObject overlayProgram;
    private GameObject storedObject;
    public static bool overlayActive;
    private Ray ray;

    //Interacting

    private GameObject[] panels;
    private bool showPanel;
    private GameObject Dropdown;
    public GameObject interactedObject;
    private int LastUpdateDropdownValue;

    //Programming
    public GameObject BAO;
    public GameObject StartSlot;
    public GameObject Slot;
    public Sprite UIsprite;

    // Use this for initialization
    void Start()
    {
        panelId = 0;
        FPSControl = GameObject.Find("FPSController");
        inputE = false;
        inputA = false;
        interfaceUseActive = false;
        interfaceProgramActive = false;
        panelsInit();
        hideInteraction();
        overlayActive = false;
        overlayProgram = GameObject.Find("Overlay_Program");
        overlayUse = GameObject.Find("Overlay_Use");
        overlayProgram.SetActive(overlayActive);
        overlayUse.SetActive(overlayActive);
        storedObject = null;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) // quitte l'interface
        {
            overlayActive = true;
            interfaceUseActive = false;
            interfaceProgramActive = false;
            pauseSwitch(true);
            hideInteraction();
            CloseBAO(false);
        }

        else
        {
            inputE = Input.GetKeyUp(KeyCode.E);
            inputA = Input.GetKeyUp(KeyCode.A);

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3) && hit.collider.gameObject.tag == "Programmable")
            {
                if (storedObject != hit.collider.gameObject)
                    storedObject = hit.collider.gameObject;
                //Debug.Log(hit.collider.gameObject.GetComponent<ObjectInfo>().ObjectName);
                GameObject.Find("Object_aimed").GetComponent<Text>().text = hit.collider.gameObject.GetComponent<ObjectInfo>().ObjectName;

                if (interfaceUseActive || interfaceProgramActive)
                    overlayActive = false;
                else
                    overlayActive = true;

                inputBehavior(storedObject);
            }
            else if (interfaceProgramActive)
            {
                inputProgramBehavior(storedObject);
            }
            else if (interfaceUseActive)
            {

                inputUseBehavior();
            }
            else
            {
                overlayActive = false;
            }

            overlayProgram.SetActive(overlayActive);
            overlayUse.SetActive(overlayActive);
        }
    }

    void hideInteraction()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("UseWindows"))
        {
            showPanel = false;
            obj.SetActive(showPanel);
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetActive(showPanel);
            }
        }
    }


    void panelsInit()
    {
        panels = new GameObject[3];
        panels[0] = GameObject.Find("Use_Glass");
        panels[1] = GameObject.Find("Use_Heater");
        panels[2] = GameObject.Find("Use_Plate");
    }
    void selectInteractionWindow()
    {
        
        switch (interactedObject.name)
        {
            case "glass":
                panelId = 0;
                break;
            case "Chauffage":
                panelId = 1;
                break;
            case "plateInfo":
                panelId = 2;
                break;
            default:
                break;
        }
        showPanel = !showPanel;
        panels[panelId].SetActive(showPanel);
        foreach (Transform child in panels[panelId].transform)
        {
            child.gameObject.SetActive(showPanel);
        }
    }

    void OpenBAO()
    {
        LastUpdateDropdownValue = 0;
        string currentFunction = null;
        if (GameObject.Find("ProgLayout").transform.childCount <= 1)
        {
            int j;
            for (j = 0; j < interactedObject.GetComponent<ObjectInfo>().Functions.Length; j++)
            {
                if (j == Dropdown.GetComponent<Dropdown>().value) currentFunction = interactedObject.GetComponent<ObjectInfo>().Functions[j].FunctionName;
            }
        }

        if (currentFunction != null && GameObject.Find(currentFunction).transform.childCount >= 2)
        {
            GameObject temp = GameObject.Find(currentFunction).transform.GetChild(1).gameObject;
            temp.transform.SetParent(GameObject.Find("ProgLayout").transform);
            temp.SetActive(true);
        }
        else
        {
            if(GameObject.Find("ProgLayout").transform.childCount < 1)
            {
                GameObject slot = Instantiate(StartSlot);
                slot.transform.SetParent(GameObject.Find("ProgLayout").transform);
                slot.GetComponent<RectTransform>().localPosition = new Vector3(0, 25, 0);
                slot.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                slot.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, 40);
            }
        }

        GameObject copy = Instantiate(interactedObject);
        GameObject light = new GameObject();
        light.AddComponent<Light>();
        light.transform.position = new Vector3(-9, 1005, 5);
        light.transform.SetParent(GameObject.Find("ObjectCamera").transform);
        copy.transform.position = new Vector3(-9, 1004, 5);
        if (copy.GetComponent<MeshFilter>()) copy.transform.localScale = new Vector3(1/copy.GetComponent<MeshFilter>().mesh.bounds.extents.x, 1/copy.GetComponent<MeshFilter>().mesh.bounds.extents.x, 1/copy.GetComponent<MeshFilter>().mesh.bounds.extents.x);
        else if (copy.transform.GetChild(0).GetComponent<MeshFilter>()) copy.transform.localScale = new Vector3(1 / copy.transform.GetChild(0).GetComponent<MeshFilter>().mesh.bounds.extents.x, 1 / copy.transform.GetChild(0).GetComponent<MeshFilter>().mesh.bounds.extents.x, 1 / copy.transform.GetChild(0).GetComponent<MeshFilter>().mesh.bounds.extents.x);
        copy.transform.rotation = Quaternion.Euler(0, -90, 0);
        copy.transform.SetParent(GameObject.Find("ObjectCamera").transform);
        copy.layer = LayerMask.NameToLayer("ObjectWindow");
        foreach(Transform child in copy.transform.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("ObjectWindow");
        }
        if (GameObject.Find("Dropdown"))
        {
            Dropdown.GetComponent<Dropdown>().options.Clear();
            foreach (Function fonction in interactedObject.GetComponent<ObjectInfo>().Functions)
            {
                Dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = fonction.FunctionName });
                Dropdown.GetComponent<Dropdown>().value = 0;
                Dropdown.transform.GetChild(0).GetComponent<Text>().text = Dropdown.GetComponent<Dropdown>().options[0].text;
                StartCoroutine("UpdateBAOvariables");
            }
        }
    }

    public void dropdownUpdate()
    {
        StartCoroutine("UpdateBAOvariables");
    }

    IEnumerator UpdateBAOvariables()
    {
        if (GameObject.Find("Programming").transform.childCount > 1)
        {
            Destroy(GameObject.Find("Programming").transform.GetChild(1).gameObject);
        }

        if (GameObject.Find("ProgLayout").transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject temp = GameObject.Find("ProgLayout").transform.GetChild(0).gameObject;
            temp.transform.SetParent(GameObject.Find(interactedObject.GetComponent<ObjectInfo>().Functions[LastUpdateDropdownValue].FunctionName).transform);
            temp.SetActive(false);
        }
        string currentFunction = null;
        if (GameObject.Find("ProgLayout").transform.childCount <= 1)
        {
            int j;
            for (j = 0; j < interactedObject.GetComponent<ObjectInfo>().Functions.Length; j++)
            {
                if (j == Dropdown.GetComponent<Dropdown>().value)
                {
                    currentFunction = interactedObject.GetComponent<ObjectInfo>().Functions[j].FunctionName;
                }
            }
        }
        if (currentFunction != null && GameObject.Find(currentFunction).transform.childCount >= 2)
        {
            GameObject temp = GameObject.Find(currentFunction).transform.GetChild(1).gameObject;
            temp.transform.SetParent(GameObject.Find("ProgLayout").transform);
            temp.SetActive(true);
        }
        else
        {
            if (GameObject.Find("ProgLayout").transform.childCount < 1)
            {
                GameObject slot = Instantiate(StartSlot);
                slot.transform.SetParent(GameObject.Find("ProgLayout").transform);
                slot.GetComponent<RectTransform>().localPosition = new Vector3(0, 25, 0);
                slot.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                slot.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, 40);
            }
        }

        foreach (Transform go in BAO.transform.GetChild(1).GetChild(0).GetChild(2))
        {
            Destroy(go.gameObject);
        }
        int i,k = 0;
        GameObject variables = GameObject.Find("Variables_List");
        for (i = 0; i < variables.GetComponent<VariablesInfo>().VariablesName.Length; i++)
        {
            foreach (string varAssociated in GameObject.Find(GameObject.Find("Dropdown").GetComponent<Dropdown>().options[GameObject.Find("Dropdown").GetComponent<Dropdown>().value].text).GetComponent<Function>().VariablesAssociated)
            {
                if (variables.GetComponent<VariablesInfo>().VariablesName[i] == varAssociated)
                {
                    k += 1;
                    GameObject var = new GameObject();
                    var.transform.parent = BAO.transform.GetChild(1).GetChild(0).GetChild(2);
                    var.transform.name = variables.GetComponent<VariablesInfo>().VariablesName[i];
                    Text varText = var.AddComponent<Text>();
                    varText.text = " -> " + variables.GetComponent<VariablesInfo>().VariablesValue[i];
                    varText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    varText.resizeTextForBestFit = true;
                    var.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.85f);
                    var.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                    var.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    var.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 45 -k * 45);
                    var.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    varText.color = Color.black;

                    GameObject var2 = Instantiate(Slot);
                    var2.transform.SetParent(BAO.transform.GetChild(1).GetChild(0).GetChild(2));
                    var2.transform.GetChild(0).GetComponent<DragHandler>().DragWindow = GameObject.Find("DragWindow");
                    var2.transform.GetChild(0).GetComponent<DragHandler>().type = 1;
                    var2.transform.GetChild(0).GetComponent<DragHandler>().dragID = "var";
                    var2.transform.GetChild(0).GetComponent<DragHandler>().varDragId =  getId(var.transform.name);
                    var2.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = variables.GetComponent<VariablesInfo>().VariablesSprite[i];
                    var2.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    var2.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    var2.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.6f);
                    var2.GetComponent<RectTransform>().anchorMax = new Vector2(0.4f, 1);
                    var2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    var2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 45 - k * 45);
                    var2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    var2.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                    var2.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                    var2.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    var2.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    var2.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -10);
                    break;
                }
            }
        }
        LastUpdateDropdownValue = Dropdown.GetComponent<Dropdown>().value;
        yield return null;
    }

    void pauseSwitch(bool state)
    {
        Screen.lockCursor = state;
        FPSControl.GetComponent<FirstPersonController>().enabled = state;
    }

    void inputBehavior(GameObject objectTargeted) // aucune UI affichée
    {

        if (inputE)
        {
            if (interfaceUseActive)
            {
                overlayActive = true;
                interfaceUseActive = false;
                hideInteraction();
                pauseSwitch(true);
            }
            else if (interfaceProgramActive)
            {
                overlayActive = false;
                interfaceUseActive = true;
                interfaceProgramActive = false;
                CloseBAO(false);
                interactedObject = objectTargeted;
                selectInteractionWindow();
                pauseSwitch(false);
            }
            else
            {
                overlayActive = false;
                interfaceUseActive = true;
                interactedObject = objectTargeted;
                selectInteractionWindow();
                pauseSwitch(false);
            }
        }

        if (inputA)
        {
            if (interfaceProgramActive)
            {
                overlayActive = true;
                interfaceProgramActive = false;
                CloseBAO(false);
                pauseSwitch(true);
            }
            else if (interfaceUseActive)
            {
                overlayActive = false;
                interfaceUseActive = false;
                interfaceProgramActive = true;
                hideInteraction();
                if (GameObject.Find("Dropdown"))
                {
                    Dropdown.GetComponent<Dropdown>().options.Clear();
                    Destroy(GameObject.Find("Blocker"));
                    Destroy(GameObject.Find("Dropdown List"));
                }
                CloseBAO(!BAO.activeSelf);
                Dropdown = GameObject.Find("Dropdown");
                interactedObject = storedObject;
                OpenBAO();
                pauseSwitch(false);
            }
            else
            {
                overlayActive = false;
                interfaceProgramActive = true;
                if (GameObject.Find("Dropdown"))
                {
                    Dropdown.GetComponent<Dropdown>().options.Clear();
                    Destroy(GameObject.Find("Blocker"));
                    Destroy(GameObject.Find("Dropdown List"));
                }
                CloseBAO(!BAO.activeSelf);
                Dropdown = GameObject.Find("Dropdown");
                interactedObject = storedObject;
                OpenBAO();
                pauseSwitch(false);
            }
        }

    }

    void inputProgramBehavior(GameObject objectTargeted) // UI program déjà affichée
    {
        if (inputE)
        {
            overlayActive = false;
            interfaceUseActive = true;
            interfaceProgramActive = false;
            CloseBAO(false);
            pauseSwitch(false);
            interactedObject = objectTargeted;
            selectInteractionWindow();
            pauseSwitch(false);
        }
        if (inputA)
        {
            overlayActive = true;
            interfaceProgramActive = false;
            CloseBAO(false);
            pauseSwitch(true);
        }
    }

    void inputUseBehavior() // UI use déjà affichée
    {

        if (inputE)
        {
            overlayActive = true;
            interfaceUseActive = false;
            hideInteraction();
            pauseSwitch(true);
        }
        if (inputA)
        {
            overlayActive = false;
            interfaceUseActive = false;
            interfaceProgramActive = true;
            hideInteraction();
            if (GameObject.Find("Dropdown"))
            {
                Dropdown.GetComponent<Dropdown>().options.Clear();
                Destroy(GameObject.Find("Blocker"));
                Destroy(GameObject.Find("Dropdown List"));
            }
            CloseBAO(!BAO.activeSelf);
            Dropdown = GameObject.Find("Dropdown");
            interactedObject = storedObject;
            OpenBAO();
            pauseSwitch(false);
        }
    }

    void CloseBAO(bool set)
    {
        if (!set)
        {
            foreach (Transform child in GameObject.Find("ObjectCamera").transform)
            {
                Destroy(child.gameObject);
            }
            if(GameObject.Find("ProgLayout") && GameObject.Find("ProgLayout").transform.childCount >= 1)
            {
                if (GameObject.Find("ProgLayout").transform.GetChild(0).gameObject.activeSelf)
                {
                    GameObject temp = GameObject.Find("ProgLayout").transform.GetChild(0).gameObject;
                    temp.transform.SetParent(GameObject.Find(interactedObject.GetComponent<ObjectInfo>().Functions[Dropdown.GetComponent<Dropdown>().value].FunctionName).transform);
                    temp.SetActive(false);
                }
            }
        }
        BAO.SetActive(set);
    }

    int getId(string name)
    {
        switch (name)
        {
            case "Chauffage":
                return 4;
            case "Neige":
                return 5;
            case "Verre":
                return 6;
            case "Tarte":
                return 7;
            case "Pomme":
                return 8;
            case "Farine":
                return 9;
            case "Beurre":
                return 10;
            case "Lait":
                return 11;
            default:
                return 0;

        }
    }
}