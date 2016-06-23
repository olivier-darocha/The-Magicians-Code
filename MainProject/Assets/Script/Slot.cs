using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Slot : MonoBehaviour, IDropHandler
{
    public static GameObject interpreterParent;
    public int dropNumber;
    public Sprite UIsprite;
    public Sprite Closesprite;
    public Sprite Checksprite;
    public bool used = false;
    public int type = 0;
    public int child_index = 0;
    public Font font;
    public string ID;
    public Color[] colors;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                //return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    void Start()
    {
        switch (GameObject.Find("Label").GetComponent<Text>().text)
        {
            case "Cuisiner":
                interpreterParent = GameObject.Find("Food");
                break;
            case "Remplir":
                interpreterParent = GameObject.Find("Water");
                break;
            case "Chauffer":
                interpreterParent = GameObject.Find("Heat");
                break;

        }
        
    }

    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            DragHandler.item.transform.SetParent(transform);
            if (DragHandler.item.GetComponent<DragHandler>().type == 0)
            {
                switch (DragHandler.item.GetComponent<DragHandler>().dragID)
                {
                    case "if":
                        CreateBox(DragHandler.item.GetComponent<DragHandler>().text, true, 3);
                        break;
                    case "elsif":
                        if (transform.childCount > 1) if (transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>() && transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "if" || transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "elsif") CreateBox(DragHandler.item.GetComponent<DragHandler>().text, true, 3);
                        break;
                    case "else":
                        if (transform.childCount > 1) if (transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>() && transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "if" || transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "elsif") CreateBox(DragHandler.item.GetComponent<DragHandler>().text, false, 2);
                        break;
                    case "while":
                        CreateBox(DragHandler.item.GetComponent<DragHandler>().text, true, 3);
                        break;
                    case "func":
                        PlaceFonction();
                        break;
                    default:
                        break;
                }
            }
            if (DragHandler.item.GetComponent<DragHandler>().type == 1)
            {
                switch (DragHandler.item.GetComponent<DragHandler>().dragID)
                {
                    case "var":
                        PlaceVariable();
                        break;
                    case "var2":
                        PlaceVariableToAssign();
                        break;
                    default:
                        break;
                }
            }
            if (DragHandler.item.GetComponent<DragHandler>().type == 2)
            {
                switch (DragHandler.item.GetComponent<DragHandler>().dragID)
                {
                    case "comp":
                        PlaceComp();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    #endregion

    public void CreateBox(string boxText, bool cond, int slotCount)
    {
        float contentSize = 0;
        int i;
        for(i = 0; i < GameObject.Find("ProgLayout").transform.childCount; i++)
        {
            contentSize += GameObject.Find("ProgLayout").transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
        }
        GameObject.Find("ProgContent").GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("ProgContent").GetComponent<RectTransform>().sizeDelta.x, contentSize + 90);
        if (transform.tag == "SlotLayout" && (transform.childCount <= 1 || child_index == 0))
        {
            if (GameObject.Find("ProgLayout").transform.childCount > 0 && GameObject.Find("ProgLayout").transform.GetChild(0).childCount == 1 && child_index == 0)
            {
                GameObject.Find("ProgLayout").transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(-10, 40);
                GameObject.Find("ProgLayout").transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
            }

            GameObject par = new GameObject();
            par.transform.SetParent(transform);
            par.transform.name = "parent";
            par.AddComponent<RectTransform>();
            par.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            par.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            par.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            par.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            par.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            GetComponent<Slot>().used = true;
            GameObject go = new GameObject();
            go.transform.SetParent(par.transform);
            go.transform.name = "content_slot";
            go.AddComponent<Image>();
            go.GetComponent<Image>().sprite = UIsprite;
            go.GetComponent<Image>().type = Image.Type.Sliced;
            go.transform.tag = "SlotLayout";
            go.AddComponent<Slot>();
            go.GetComponent<Slot>().UIsprite = UIsprite;
            go.GetComponent<Slot>().Closesprite = Closesprite;
            go.GetComponent<Slot>().Checksprite = Checksprite;
            go.GetComponent<Slot>().child_index = GetComponent<Slot>().child_index + 1;
            go.GetComponent<Slot>().colors = colors;
            go.GetComponent<Slot>().ID = DragHandler.item.GetComponent<DragHandler>().dragID;
            go.GetComponent<Slot>().font = font;
            go.GetComponent<Slot>().dropNumber = GameplayMenuSetup.orderCount; // pour Interpreter
            if (go.GetComponent<Slot>().child_index - 1 <= 6)
            {
                go.GetComponent<Image>().color = colors[go.GetComponent<Slot>().child_index - 1];
            }
            else
            {
                go.GetComponent<Image>().color = Color.white;
            }
            go.AddComponent<LayoutElement>();
            go.GetComponent<LayoutElement>().minHeight = 40;
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -45);
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, -17.5f, 0);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -17.5f);
            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            Transform transf = go.transform.parent;

            while (transf.name != "ProgLayout")
            {
                if (transf.parent.name == "ProgLayout")
                {
                    transf.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 80);
                    transf.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -40);
                }
                transf = transf.parent;
            }
            GameObject go3 = new GameObject();
            go3.transform.SetParent(par.transform);
            go3.transform.name = "condition_parent";
            go3.AddComponent<HorizontalLayoutGroup>();
            go3.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, 30);
            go3.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            go3.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20);
            if (cond)
            {
                GameObject go1 = new GameObject();
                go1.transform.SetParent(go3.transform);
                go1.transform.name = "condition_slot";
                go1.AddComponent<Button>();
                go1.AddComponent<ConditionButton>();
                go1.GetComponent<ConditionButton>().UIsprite = UIsprite;
                go1.GetComponent<ConditionButton>().Closesprite = Closesprite;
                go1.GetComponent<ConditionButton>().Checksprite = Checksprite;
                go1.GetComponent<ConditionButton>().font = font;
                go1.AddComponent<Image>();
                go1.GetComponent<Image>().sprite = UIsprite;
                go1.GetComponent<Image>().type = Image.Type.Sliced;
                go1.GetComponent<Button>().targetGraphic = go1.GetComponent<Image>();
                go1.GetComponent<Button>().onClick.AddListener(() => go1.GetComponent<ConditionButton>().Clicked(go1,go));
                GameObject go4 = new GameObject();
                go4.transform.SetParent(go1.transform);
                go4.transform.name = "condition_text";
                go4.AddComponent<Text>();
                go4.GetComponent<Text>().text = "Condition";
                go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-6, 0);
                go4.GetComponent<Text>().font = font;
                go4.GetComponent<Text>().color = Color.black;
                go4.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                go4.GetComponent<Text>().resizeTextForBestFit = true;

                go1.AddComponent<LayoutElement>();
                go1.GetComponent<LayoutElement>().minHeight = 30;
                go1.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 30);
                go1.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
                go1.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
                go1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20);
            }
            string[] texts = DragHandler.item.GetComponent<DragHandler>().text.Split('#');
            for (i = 0; i < texts.Length; i++)
            {
                GameObject go2 = new GameObject();
                go2.transform.SetParent(go3.transform);
                go2.transform.name = "text";
                if (i == 0) go2.transform.SetAsFirstSibling();
                go2.AddComponent<Text>();
                go2.GetComponent<Text>().text = texts[i];
                go2.GetComponent<Text>().font = font;
                go2.GetComponent<Text>().color = Color.black;
                go2.AddComponent<LayoutElement>();
                go2.GetComponent<LayoutElement>().minHeight = 30;
                go2.GetComponent<RectTransform>().pivot = new Vector2(i, 1);
                go2.GetComponent<RectTransform>().anchorMin = new Vector2(i, 1);
                go2.GetComponent<RectTransform>().anchorMax = new Vector2(i, 1);
                go2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                go2.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 30);
                go2.GetComponent<Text>().resizeTextForBestFit = true;
            }


            // code pour Interpreter
            createTool(go);
            GameplayMenuSetup.orderCount++;


            GameObject go5 = new GameObject();
            go5.transform.SetParent(par.transform);
            go5.transform.name = "button";
            go5.AddComponent<Image>();
            go5.GetComponent<Image>().sprite = UIsprite;
            go5.GetComponent<Image>().type = Image.Type.Sliced;
            go5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go5.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);

            GameObject go6 = new GameObject();
            go6.transform.SetParent(go5.transform);
            go6.transform.name = "close";
            go6.AddComponent<Button>();
            go6.AddComponent<Image>();
            go6.GetComponent<Image>().sprite = Closesprite;
            go6.GetComponent<Image>().type = Image.Type.Sliced;
            go6.GetComponent<Button>().targetGraphic = go5.GetComponent<Image>();
            go6.GetComponent<Button>().onClick.AddListener(() => this.Close(par));
            go6.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go6.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go6.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            go6.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go6.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go6.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);
        }
    }

    public void Close(GameObject parent)
    {
        
        parent.transform.parent.GetComponent<Slot>().used = false;
        
        foreach (ConditionScript o in interpreterParent.transform.GetComponentsInChildren<ConditionScript>())
        {
            if (o.order == parent.transform.GetChild(0).GetComponent<Slot>().dropNumber)
            {
                Destroy(o.gameObject);
            }
        }
        Destroy(parent);
    }

    public void PlaceVariable()
    {
        if(type == 1 && !GetComponent<Slot>().used)
        {
            GetComponent<Slot>().used = true;
            GameObject var2 = Instantiate(DragHandler.item);
            var2.transform.SetParent(transform);
            var2.GetComponent<DragHandler>().DragWindow = GameObject.Find("DragWindow");
            var2.GetComponent<DragHandler>().type = 1;
            var2.GetComponent<DragHandler>().dragID = "var";
            var2.transform.GetChild(0).GetComponent<Image>().sprite = DragHandler.item.transform.GetChild(0).GetComponent<Image>().sprite;
            var2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            var2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            var2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            var2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            var2.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -10);

            GameObject go5 = new GameObject();
            go5.transform.SetParent(transform);
            go5.transform.name = "button";
            go5.AddComponent<Image>();
            go5.GetComponent<Image>().sprite = UIsprite;
            go5.GetComponent<Image>().type = Image.Type.Sliced;
            go5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go5.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);

            GameObject go4 = new GameObject();
            go4.transform.SetParent(go5.transform);
            go4.transform.name = "close";
            go4.AddComponent<Button>();
            go4.AddComponent<Image>();
            go4.GetComponent<Image>().sprite = Closesprite;
            go4.GetComponent<Image>().type = Image.Type.Sliced;
            go4.GetComponent<Button>().targetGraphic = go5.GetComponent<Image>();
            go4.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject a = null;
                foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
                {
                    if (y.order == GetComponent<Slot>().dropNumber) a = y.gameObject;
                }
                foreach (Transform o in a.transform.GetChild(0))
                {
                    if (o.gameObject.name == "var") Destroy(o.gameObject);
                }
                var2.transform.parent.GetComponent<Slot>().used = false;
                Destroy(var2);
                Destroy(go5);
            });
            go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);

            //code Interpreter
            GameObject var = new GameObject("var");
            GameObject temp = null;
            foreach(ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
            {
                if (y.order == GetComponent<Slot>().dropNumber) temp = y.gameObject;
            }
            var.transform.SetParent(temp.transform.GetChild(0));
            var.AddComponent<VariableId>().varId = var2.GetComponent<DragHandler>().varDragId.ToString();
            var.tag = "var";
        }
    }

    public void PlaceVariableToAssign()
    {

        // code Interpreter
        GameObject value = new GameObject("value");
        GameObject temp = null;
        foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
        {
            if (y.order == GetComponent<Slot>().dropNumber) temp = y.gameObject;
        }
        value.transform.SetParent(temp.transform.GetChild(0));
        value.AddComponent<Value>();
        value.tag = "value";


        if (type == 1 && !GetComponent<Slot>().used)
        {
            if (DragHandler.item.transform.GetChild(0).GetComponent<Image>().sprite.name != "bool")
            {
                GetComponent<Slot>().used = true;
                GameObject var2 = new GameObject();
                var2.transform.SetParent(transform);
                var2.AddComponent<InputField>();
                if (DragHandler.item.transform.GetChild(0).GetComponent<Image>().sprite.name == "nombre")
                {
                    var2.GetComponent<InputField>().characterValidation = InputField.CharacterValidation.Decimal;
                }


                GameObject text2 = new GameObject();
                text2.transform.SetParent(var2.transform);
                text2.AddComponent<Text>();
                text2.GetComponent<Text>().font = font;
                text2.GetComponent<Text>().fontSize = 40;
                text2.GetComponent<Text>().color = Color.black;
                text2.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                text2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                text2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                text2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                text2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                text2.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, -20);
                var2.AddComponent<Image>();
                var2.GetComponent<Image>().sprite = UIsprite;
                var2.GetComponent<InputField>().targetGraphic = var2.GetComponent<Image>();
                var2.GetComponent<InputField>().textComponent = text2.GetComponent<Text>();
                var2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                var2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                var2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                var2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                var2.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -10);

                var2.GetComponent<InputField>().onValueChanged.AddListener(delegate
                {
                    GameObject.Find("GUI").GetComponent<GameplayMenuSetup>().enabled = false;
                });

                var2.GetComponent<InputField>().onEndEdit.AddListener(delegate
                {
                    // interpreter
                    if (var2.GetComponent<InputField>().characterValidation == InputField.CharacterValidation.Decimal)
                    {
                        value.GetComponent<Value>().valueFloat = float.Parse(text2.GetComponent<Text>().text);
                    }
                    GameObject.Find("GUI").GetComponent<GameplayMenuSetup>().enabled = true;
                });

                GameObject go5 = new GameObject();
                go5.transform.SetParent(transform);
                go5.transform.name = "button";
                go5.AddComponent<Image>();
                go5.GetComponent<Image>().sprite = UIsprite;
                go5.GetComponent<Image>().type = Image.Type.Sliced;
                go5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go5.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                go5.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                go5.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                go5.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);

                GameObject go4 = new GameObject();
                go4.transform.SetParent(go5.transform);
                go4.transform.name = "close";
                go4.AddComponent<Button>();
                go4.AddComponent<Image>();
                go4.GetComponent<Image>().sprite = Closesprite;
                go4.GetComponent<Image>().type = Image.Type.Sliced;
                go4.GetComponent<Button>().targetGraphic = go5.GetComponent<Image>();
                go4.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameObject a = null;
                    foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
                    {
                        if (y.order == GetComponent<Slot>().dropNumber) a = y.gameObject;
                    }
                    foreach (Transform o in a.transform.GetChild(0))
                    {
                        if (o.gameObject.name == "value") Destroy(o.gameObject);
                    }
                    var2.transform.parent.GetComponent<Slot>().used = false;
                    Destroy(var2);
                    Destroy(go5);
                });
                go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                go4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);

            }
            else
            {
                GetComponent<Slot>().used = true;
                GameObject var2 = new GameObject();
                var2.transform.SetParent(transform);
                var2.AddComponent<Toggle>();
                GameObject var3 = new GameObject();
                var3.transform.SetParent(var2.transform);
                var3.AddComponent<Image>();
                var3.GetComponent<Image>().sprite = UIsprite;
                var3.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                var3.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                var3.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                var3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                var3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                var3.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
                GameObject var4 = new GameObject();
                var4.transform.SetParent(var3.transform);
                var4.AddComponent<Image>();
                var4.GetComponent<Image>().sprite = Checksprite;
                var4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                var4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                var4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                var4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                var4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                var4.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                var2.GetComponent<Toggle>().targetGraphic = var3.GetComponent<Image>();
                var2.GetComponent<Toggle>().graphic = var4.GetComponent<Image>();
                var2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                var2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                var2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                var2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                var2.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -10);

                GameObject go5 = new GameObject();
                go5.transform.SetParent(transform);
                go5.transform.name = "button";
                go5.AddComponent<Image>();
                go5.GetComponent<Image>().sprite = UIsprite;
                go5.GetComponent<Image>().type = Image.Type.Sliced;
                go5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go5.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                go5.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                go5.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                go5.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);

                GameObject go4 = new GameObject();
                go4.transform.SetParent(go5.transform);
                go4.transform.name = "close";
                go4.AddComponent<Button>();
                go4.AddComponent<Image>();
                go4.GetComponent<Image>().sprite = Closesprite;
                go4.GetComponent<Image>().type = Image.Type.Sliced;
                go4.GetComponent<Button>().targetGraphic = go5.GetComponent<Image>();
                go4.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameObject a = null;
                    foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
                    {
                        if (y.order == GetComponent<Slot>().dropNumber) a = y.gameObject;
                    }
                    foreach(Transform o in a.transform.GetChild(0))
                    {
                        if (o.gameObject.name == "value") Destroy(o.gameObject);
                    }
                    var2.transform.parent.GetComponent<Slot>().used = false;
                    Destroy(var2);
                    Destroy(go5);
                });
                go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                go4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);

                var2.GetComponent<Toggle>().onValueChanged.AddListener(delegate
                {
                    value.GetComponent<Value>().valueBool = var2.GetComponent<Toggle>().isOn;
                });

                value.GetComponent<Value>().valueBool = var2.GetComponent<Toggle>().isOn;
            }

        }
    }

    public void PlaceComp()
    {
        if (type == 2 && !GetComponent<Slot>().used)
        {
            GetComponent<Slot>().used = true;
            GameObject var2 = Instantiate(DragHandler.item);
            var2.transform.SetParent(transform);
            var2.GetComponent<DragHandler>().DragWindow = GameObject.Find("DragWindow");
            var2.GetComponent<DragHandler>().type = 1;
            var2.GetComponent<DragHandler>().dragID = "comp";
            var2.transform.GetChild(0).GetComponent<Image>().sprite = DragHandler.item.transform.GetChild(0).GetComponent<Image>().sprite;
            var2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            var2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            var2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            var2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            var2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            GameObject go5 = new GameObject();
            go5.transform.SetParent(transform);
            go5.transform.name = "button";
            go5.AddComponent<Image>();
            go5.GetComponent<Image>().sprite = UIsprite;
            go5.GetComponent<Image>().type = Image.Type.Sliced;
            go5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go5.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);

            GameObject go4 = new GameObject();
            go4.transform.SetParent(go5.transform);
            go4.transform.name = "close";
            go4.AddComponent<Button>();
            go4.AddComponent<Image>();
            go4.GetComponent<Image>().sprite = Closesprite;
            go4.GetComponent<Image>().type = Image.Type.Sliced;
            go4.GetComponent<Button>().targetGraphic = go5.GetComponent<Image>();
            go4.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject a = null;
                foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
                {
                    if (y.order == GetComponent<Slot>().dropNumber) a = y.gameObject;
                }
                foreach (Transform o in a.transform.GetChild(0))
                {
                    if (o.gameObject.name == "sign") Destroy(o.gameObject);
                }
                var2.transform.parent.GetComponent<Slot>().used = false;
                Destroy(var2);
                Destroy(go5);
            });
            go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);


            //code Interpreter
            GameObject sign = new GameObject("sign");
            GameObject temp = null;
            foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
            {
                if (y.order == GetComponent<Slot>().dropNumber) temp = y.gameObject;
            }
            sign.transform.SetParent(temp.transform.GetChild(0));
            string s = "";
            switch(var2.GetComponent<DragHandler>().varDragId)
            {
                case 12:
                    s = "=";
                    break;
                case 13:
                    s = "<";
                    break;
                case 14:
                    s = ">";
                    break;
                case 15:
                    s = "!=";
                    break;
            }
            sign.AddComponent<Sign>().sign = s;
            sign.tag = "sign";
        }
    }

    public void PlaceFonction()
    {
        if (type == 0 && !GetComponent<Slot>().used)
        {
            GetComponent<Slot>().used = true;

            GameObject temp = new GameObject();
            temp.transform.SetParent(transform);
            temp.AddComponent<RectTransform>();
            temp.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            temp.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            temp.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            GameObject var2 = Instantiate(DragHandler.item);
            var2.transform.SetParent(temp.transform);
            var2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            var2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            var2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            var2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            var2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            var2.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
            var2.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            var2.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            var2.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            var2.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.height, 0);

            GameObject go5 = new GameObject();
            go5.transform.SetParent(temp.transform);
            go5.transform.name = "button";
            go5.AddComponent<Image>();
            go5.GetComponent<Image>().sprite = UIsprite;
            go5.GetComponent<Image>().type = Image.Type.Sliced;
            go5.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go5.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go5.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);

            GameObject go4 = new GameObject();
            go4.transform.SetParent(go5.transform);
            go4.transform.name = "close";
            go4.AddComponent<Button>();
            go4.AddComponent<Image>();
            go4.GetComponent<Image>().sprite = Closesprite;
            go4.GetComponent<Image>().type = Image.Type.Sliced;
            go4.GetComponent<Button>().targetGraphic = go5.GetComponent<Image>();
            go4.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject a = null;
                GameObject b = null;
                foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
                {
                    if (y.order == GetComponent<Slot>().dropNumber) a = y.gameObject;
                }

                foreach(Transform c in interpreterParent.transform)
                {
                    if (c.tag == "freeFunc" && c.GetComponent<FunctionId>().order == GetComponent<Slot>().dropNumber)
                        Destroy(c.gameObject);
                }
                if(a != null && a.transform.childCount > 0)
                {
                    foreach (Transform o in a.transform.GetChild(1))
                    {
                        if (o.gameObject.tag == "func") Destroy(o.gameObject);
                    }
                }
                var2.transform.parent.parent.GetComponent<Slot>().used = false;
                Destroy(temp);
            });
            go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);

            //code Interpreter
            GameObject func = new GameObject("func");
            GameObject t = null;
            foreach (ConditionScript y in interpreterParent.GetComponentsInChildren<ConditionScript>())
            {
                if (y.order == GetComponent<Slot>().dropNumber) t = y.gameObject;
            }
            if (var2.transform.parent.parent.gameObject.GetComponent<Slot>().child_index == 0)
            {
                func.tag = "freeFunc";
                func.transform.SetParent(interpreterParent.transform);
                func.AddComponent<FunctionId>().functionId = var2.GetComponent<DragHandler>().varDragId.ToString();
                func.GetComponent<FunctionId>().order = GetComponent<Slot>().dropNumber;
            }
            else
            {
                func.tag = "func";
                func.transform.SetParent(t.transform.GetChild(1));
                func.AddComponent<FunctionId>().functionId = var2.GetComponent<DragHandler>().varDragId.ToString();
                func.GetComponent<FunctionId>().order = GetComponent<Slot>().dropNumber;
            }


        }
    }

    void createTool(GameObject obj)
    {
        string id = obj.GetComponent<Slot>().ID;

        GameObject tool = new GameObject();
        tool.tag = "tools";
        tool.AddComponent<ConditionScript>();
        tool.GetComponent<ConditionScript>().order = GameplayMenuSetup.orderCount;
        if (child_index == 0)
            tool.transform.SetParent(interpreterParent.transform);
        else
        {
            int num = obj.transform.parent.parent.gameObject.GetComponent<Slot>().dropNumber;
            GameObject parent = findParent(num);
            tool.transform.SetParent(parent.transform);
        }


        GameObject conditions = new GameObject("conditions");
        conditions.transform.SetParent(tool.transform);
        conditions.tag = "condition";

        GameObject functions = new GameObject("functions");
        functions.transform.SetParent(tool.transform);
        functions.tag = "function";

        switch (id)
        {
            case "if":
                tool.transform.name = "if";
                tool.GetComponent<ConditionScript>().toolId = "0";
                break;
            case "else":
                tool.name = "else";
                tool.GetComponent<ConditionScript>().toolId = "1";
                break;
            case "elsif":
                tool.name = "else if";
                tool.GetComponent<ConditionScript>().toolId = "2";
                break;
            case "while":
                tool.name = "while";
                tool.GetComponent<ConditionScript>().toolId = "3";
                break;
        }
    }

    GameObject findParent(int numId)
    {
        GameObject parent = null;
        GameObject o = interpreterParent;
        List<GameObject> childList = new List<GameObject>(o.transform.childCount);
        foreach (Transform child in o.transform)
        {
            if (child.tag == "tools" && child.GetComponent<ConditionScript>().order == numId)
                parent = child.gameObject;
        }
        return parent;
    }

}