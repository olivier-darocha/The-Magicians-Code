using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConditionButton : MonoBehaviour {

    public string ID1;
    public string comp;
    public string ID2;
    public Font font;
    public Sprite UIsprite;
    public Sprite Closesprite;
    public Sprite Checksprite;
    public GameObject cond;

    public void Clicked(GameObject cond_slot) {
        if (cond_slot.transform.childCount > 1 && !cond_slot.transform.GetChild(1).gameObject.activeSelf)
        {
            cond_slot.transform.GetChild(1).gameObject.SetActive(true);
            cond_slot.transform.GetChild(1).SetParent(GameObject.Find("Programming").transform);
        }
        else
        {
            cond = cond_slot;
            GameObject go0 = new GameObject();
            go0.transform.SetParent(GameObject.Find("Programming").transform);
            go0.transform.name = "BoutonCondition";
            go0.AddComponent<RawImage>();
            go0.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
            go0.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go0.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go0.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            go0.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go0.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go0.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            GameObject go1 = new GameObject();
            go1.transform.SetParent(go0.transform);
            go1.transform.name = "Darken";
            go1.AddComponent<RawImage>();
            go1.GetComponent<RawImage>().color = new Color(0, 0, 0, 0.85f);
            go1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            go1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go1.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            GameObject go = new GameObject();
            go.transform.SetParent(go0.transform);
            go.transform.name = "ID1";
            go.AddComponent<Image>();
            go.GetComponent<Image>().sprite = UIsprite;
            go.GetComponent<Image>().type = Image.Type.Sliced;
            go.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            go.transform.tag = "SlotLayout";
            go.AddComponent<Slot>();
            go.GetComponent<Slot>().UIsprite = UIsprite;
            go.GetComponent<Slot>().Closesprite = Closesprite;
            go.GetComponent<Slot>().Checksprite = Checksprite;
            go.GetComponent<Slot>().font = font;
            go.GetComponent<Slot>().type = 1;
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, 150, 0);

            GameObject go2 = new GameObject();
            go2.transform.SetParent(go0.transform);
            go2.transform.name = "comp";
            go2.AddComponent<Image>();
            go2.GetComponent<Image>().sprite = UIsprite;
            go2.GetComponent<Image>().type = Image.Type.Sliced;
            go2.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            go2.transform.tag = "SlotLayout";
            go2.AddComponent<Slot>();
            go2.GetComponent<Slot>().UIsprite = UIsprite;
            go2.GetComponent<Slot>().Closesprite = Closesprite;
            go2.GetComponent<Slot>().Checksprite = Checksprite;
            go2.GetComponent<Slot>().font = font;
            go2.GetComponent<Slot>().type = 2;
            go2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            GameObject go3 = new GameObject();
            go3.transform.SetParent(go0.transform);
            go3.transform.name = "ID2";
            go3.AddComponent<Image>();
            go3.GetComponent<Image>().sprite = UIsprite;
            go3.GetComponent<Image>().type = Image.Type.Sliced;
            go3.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            go3.transform.tag = "SlotLayout";
            go3.AddComponent<Slot>();
            go3.GetComponent<Slot>().UIsprite = UIsprite;
            go3.GetComponent<Slot>().Closesprite = Closesprite;
            go3.GetComponent<Slot>().Checksprite = Checksprite;
            go3.GetComponent<Slot>().font = font;
            go3.GetComponent<Slot>().type = 1;
            go3.GetComponent<RectTransform>().localPosition = new Vector3(0, -150, 0);

            GameObject go5 = new GameObject();
            go5.transform.SetParent(go0.transform);
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
            go4.GetComponent<Button>().onClick.AddListener(() => this.Close(go0));
            go4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            go4.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go4.GetComponent<RectTransform>().sizeDelta = new Vector2(-5, -5);
        }
    }

    public void Close(GameObject go)
    {
        go.transform.SetParent(cond.transform);
        go.SetActive(false);
    }
}
