using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject item;    // i changed itembeigdraged to item.
    public Vector2 Size;
    public string dragID;
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
        transform.position = Input.mousePosition;
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
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent != startParent) Destroy(gameObject);
    }

}
