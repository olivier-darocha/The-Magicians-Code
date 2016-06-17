using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    public Sprite UIsprite;
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
    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            DragHandler.item.transform.SetParent(transform);
            switch (DragHandler.item.GetComponent<DragHandler>().dragID)
            {
                case "if":
                    CreateBox(DragHandler.item.GetComponent<DragHandler>().text,true,3);
                    break;
                case "elsif":
                    if(transform.childCount > 1) if(transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>() && transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "if" || transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "elsif") CreateBox(DragHandler.item.GetComponent<DragHandler>().text,true,3);
                    break;
                case "else":
                    if(transform.childCount > 1) if(transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>() && transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "if" || transform.GetChild(transform.childCount - 2).GetChild(0).GetComponent<Slot>().ID == "elsif") CreateBox(DragHandler.item.GetComponent<DragHandler>().text,false,2);
                    break;
                case "while":
                    CreateBox(DragHandler.item.GetComponent<DragHandler>().text,true,3);
                    break;
                default:
                    break;
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
        if (transform.tag == "SlotLayout")
        {
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
            go.GetComponent<Slot>().child_index = GetComponent<Slot>().child_index + 1;
            go.GetComponent<Slot>().colors = colors;
            go.GetComponent<Slot>().ID = DragHandler.item.GetComponent<DragHandler>().dragID;
            go.GetComponent<Slot>().font = font;
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
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -80);
            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
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
                go1.AddComponent<Image>();
                go1.GetComponent<Image>().sprite = UIsprite;
                go1.GetComponent<Image>().type = Image.Type.Sliced;
                go1.GetComponent<Button>().targetGraphic = go1.GetComponent<Image>();
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
        }
    }
}