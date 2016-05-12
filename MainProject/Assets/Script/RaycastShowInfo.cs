using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaycastShowInfo : MonoBehaviour {

    private bool notredo = false;
    public bool interacted = false;
    private Ray ray;

	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                if (!interacted && hit.collider.gameObject.GetComponent<Interaction>())
                {
                    if (Input.GetKeyDown("e"))
                    {
                        interacted = true;
                        hit.collider.gameObject.GetComponent<Interaction>().Interagir();
                    }
                }

                if (!notredo)
                {
                    GameObject.Find("Text").GetComponent<Text>().text = hit.collider.gameObject.GetComponent<ObjectInfo>().ObjectName;
                    notredo = true;
                }
            }
            else
            {
                GameObject.Find("Text").GetComponent<Text>().text = "";
                notredo = false;
            }
        }
        else
        {
            GameObject.Find("Text").GetComponent<Text>().text = "";
            notredo = false;
        }
    }

}
