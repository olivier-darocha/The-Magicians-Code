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
    private GameObject FPSControl;
    private GameObject overlayUse;
    private GameObject overlayProgram;
    private GameObject storedObject;
    public static bool overlayActive;

    //Interacting

    private GameObject panel;
    private bool showPanel;
    private GameObject Dropdown;


    //Programming
    public GameObject BAO;

    // Use this for initialization
    void Start()
    {
        FPSControl = GameObject.Find("FPSController");
        inputE = false;
        inputA = false;
        interfaceUseActive = false;
        interfaceProgramActive = false;
        interactionGlassInit();
        overlayActive = false;
        overlayProgram = GameObject.Find("Overlay_Program");
        overlayUse = GameObject.Find("Overlay_Use");
        overlayProgram.SetActive(overlayActive);
        overlayUse.SetActive(overlayActive);
        storedObject = null;
    }

    void Update()
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


    //interacting part
    void interactionGlassInit()
    {
        panel = GameObject.Find("Use_Glass");
        hideInteraction();
    }

    void interactionGlass()
    {
        showPanel = !showPanel;
        panel.SetActive(showPanel);
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(showPanel);
        }
    }

    void hideInteraction()
    {
        showPanel = false;
        panel.SetActive(showPanel);
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(showPanel);
        }
    }

    void selectInteractionWindow(GameObject interactedObject)
    {
        switch (interactedObject.name)
        {
            case "Final_glass_water":
                interactionGlass();
                break;
            default:
                break;
        }
    }

    void OpenBAO(GameObject interactedObject)
    {
        if (GameObject.Find("Dropdown"))
        {
            Debug.Log(GameObject.Find("Content").transform.GetChild(0).GetComponent<Text>().text.Split('\n').Length - 1);
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

    /*public void UpdateBAOvariables()
    {
        foreach(Transform go in BAO.transform.GetChild(1).GetChild(0).GetChild(2))
        {
            Destroy(go.gameObject);
        }
        int i;
        GameObject variables = GameObject.Find("Variables_List");
        for (i = 0; i < variables.GetComponent<VariablesInfo>().VariablesName.Length; i++)
        {
            foreach (string varAssociated in GameObject.Find(GameObject.Find("Dropdown").GetComponent<Dropdown>().options[GameObject.Find("Dropdown").GetComponent<Dropdown>().value].text).GetComponent<Function>().VariablesAssociated)
            {
                if(variables.GetComponent<VariablesInfo>().VariablesName[i] == varAssociated)
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
    }*/

    void pauseSwitch(bool state)
    {
        Screen.lockCursor = state;
        FPSControl.GetComponent<FirstPersonController>().enabled = state;
    }

    void inputBehavior(GameObject objectTargeted)
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

    void inputProgramBehavior(GameObject objectTargeted)
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

    void inputUseBehavior()
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