using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject item;    // i changed itembeigdraged to item.

    public GameObject DragWindow;
    Transform startParent;
    Vector3 startPosition;
    bool start = true;
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
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

}
