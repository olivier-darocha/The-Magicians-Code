using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class GameplayMenuSetup : MonoBehaviour
{

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

    //Interacting

    private GameObject[] panels;
    private bool showPanel;
    private GameObject Dropdown;


    //Programming
    public GameObject BAO;

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
            BAO.SetActive(false);
        }

        else
        {
            inputE = Input.GetKeyUp(KeyCode.E);
            inputA = Input.GetKeyUp(KeyCode.A);
            if (Physics.Raycast(RaycastShowInfo.ray, out RaycastShowInfo.hit, 3) && RaycastShowInfo.hit.collider.gameObject.tag == "Programmable")
            {
                if (storedObject != RaycastShowInfo.hit.collider.gameObject)
                    storedObject = RaycastShowInfo.hit.collider.gameObject;

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
        panels = new GameObject[2];
        panels[0] = GameObject.Find("Use_Glass");
        panels[1] = GameObject.Find("Use_Heater");
    }
    void selectInteractionWindow(GameObject interactedObject)
    {
        
        switch (interactedObject.name)
        {
            case "Final_glass_water":
                panelId = 0;
                break;
            case "Chauffage":
                panelId = 1;
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

    void OpenBAO(GameObject interactedObject)
    {
        if (GameObject.Find("Dropdown"))
        {
            //Debug.Log(GameObject.Find("Content").transform.GetChild(0).GetComponent<Text>().text.Split('\n').Length - 1);
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
        
        foreach (Transform go in BAO.transform.GetChild(1).GetChild(0).GetChild(2))
        {
            Destroy(go.gameObject);
        }
        int i;
        GameObject variables = GameObject.Find("Variables_List");
        for (i = 0; i < variables.GetComponent<VariablesInfo>().VariablesName.Length; i++)
        {
            foreach (string varAssociated in GameObject.Find(GameObject.Find("Dropdown").GetComponent<Dropdown>().options[GameObject.Find("Dropdown").GetComponent<Dropdown>().value].text).GetComponent<Function>().VariablesAssociated)
            {
                if (variables.GetComponent<VariablesInfo>().VariablesName[i] == varAssociated)
                {
                    GameObject var = new GameObject();
                    var.transform.parent = BAO.transform.GetChild(1).GetChild(0).GetChild(2);
                    var.transform.name = variables.GetComponent<VariablesInfo>().VariablesName[i];
                    Text varText = var.AddComponent<Text>();
                    varText.text = variables.GetComponent<VariablesInfo>().VariablesName[i] + " - " + variables.GetComponent<VariablesInfo>().VariablesValue[i];
                    varText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    varText.resizeTextForBestFit = true;
                    var.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.85f);
                    var.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                    var.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -i * 15, 0);
                    var.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    varText.color = Color.black;
                    break;
                }
            }
        }
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
                BAO.SetActive(false);
                selectInteractionWindow(objectTargeted);
                pauseSwitch(false);
            }
            else
            {
                overlayActive = false;
                interfaceUseActive = true;
                selectInteractionWindow(objectTargeted);
                pauseSwitch(false);
            }
        }

        if (inputA)
        {
            if (interfaceProgramActive)
            {
                overlayActive = true;
                interfaceProgramActive = false;
                BAO.SetActive(false);
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
                BAO.SetActive(!BAO.activeSelf);
                Dropdown = GameObject.Find("Dropdown");
                OpenBAO(storedObject);
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
                BAO.SetActive(!BAO.activeSelf);
                Dropdown = GameObject.Find("Dropdown");
                OpenBAO(storedObject);
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
            BAO.SetActive(false);
            pauseSwitch(false);
            selectInteractionWindow(objectTargeted);
            pauseSwitch(false);
        }
        if (inputA)
        {
            overlayActive = true;
            interfaceProgramActive = false;
            BAO.SetActive(false);
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
            BAO.SetActive(!BAO.activeSelf);
            Dropdown = GameObject.Find("Dropdown");
            OpenBAO(storedObject);
            pauseSwitch(false);
        }
    }
}