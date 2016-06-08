using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public Sprite UIsprite;
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
            if(transform.parent.tag == "SlotLayout")
            {
                Debug.Log(transform.childCount);
                if (transform.childCount <= 1)
                {
                    GameObject go1 = new GameObject();
                    go1.AddComponent<Image>();
                    go1.GetComponent<Image>().sprite = UIsprite;
                    go1.GetComponent<Image>().type = Image.Type.Sliced;
                    go1.transform.tag = "SlotLayout";
                    go1.AddComponent<Slot>();
                    go1.GetComponent<Slot>().UIsprite = UIsprite;
                    go1.transform.SetParent(transform.parent);
                    go1.AddComponent<LayoutElement>();
                    go1.GetComponent<LayoutElement>().minHeight = 40;
                    go1.AddComponent<GridLayoutGroup>();
                    go1.GetComponent<GridLayoutGroup>().padding = new RectOffset(5, 5, 5, 5);
                    go1.GetComponent<GridLayoutGroup>().spacing = new Vector2(10, 10);
                    go1.GetComponent<GridLayoutGroup>().cellSize = new Vector2(60, 60);
                    go1.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
                    GetComponent<LayoutElement>().minHeight += 60;
                }
                GameObject go = new GameObject();
                go.AddComponent<Image>();
                go.GetComponent<Image>().sprite = UIsprite;
                go.GetComponent<Image>().type = Image.Type.Sliced;
                go.transform.SetParent(transform);
                go.transform.tag = "SlotLayout";
                go.AddComponent<Slot>();
                go.GetComponent<Slot>().UIsprite = UIsprite;
                go.AddComponent<LayoutElement>();
                go.GetComponent<LayoutElement>().minHeight = 40;
            }
        }
    }
    #endregion


}