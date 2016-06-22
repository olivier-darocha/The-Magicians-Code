using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject item;    // i changed itembeigdraged to item.
    public Vector2 Size;
    public string dragID;
    public int type;
    public GameObject DragWindow;
    public string text;
    Transform startParent;
    Vector3 startPosition;
    //Sprite sprite;

    public void OnBeginDrag(PointerEventData eventData)
    {
        item = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        item.GetComponent<LayoutElement>().ignoreLayout = true;
        item.transform.SetParent(DragWindow.transform);
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(-GameObject.Find("GUICamera").GetComponent<Camera>().pixelWidth / 2 + GameObject.Find("GUICamera").GetComponent<Camera>().pixelWidth * Input.mousePosition.x/Screen.width, -1000 + -GameObject.Find("GUICamera").GetComponent<Camera>().pixelHeight / 2 + GameObject.Find("GUICamera").GetComponent<Camera>().pixelHeight * Input.mousePosition.y / Screen.height, 100);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        item.GetComponent<LayoutElement>().ignoreLayout = false;
        item = null;

        if (transform.parent == startParent || transform.parent == DragWindow.transform)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        else
        {
            GameObject go = Instantiate(gameObject);
            go.transform.SetParent(startParent);
            go.transform.position = startPosition;
            go.GetComponent<CanvasGroup>().blocksRaycasts = true;
            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -10);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent != startParent) Destroy(gameObject);
    }

}
