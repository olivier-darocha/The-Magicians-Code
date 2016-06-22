using UnityEngine;
using UnityEngine.UI;


public class RaycastShowInfo : MonoBehaviour
{

    private bool notredo = false;
    public bool interacted = false;
    private Ray ray;
    private RaycastHit hit;

    private GameObject FPSController;
    public static bool isPaused;
    private GameObject interactedObject;


    void Start()
    {
        FPSController = GameObject.Find("FPSController");
        isPaused = false;

    }

    void Update()
    {
        if (GameplayMenuSetup.interfaceProgramActive || GameplayMenuSetup.interfaceUseActive)
            isPaused = true;
        else
            isPaused = false;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!isPaused && Physics.Raycast(ray, out hit, 3))
        {
            Debug.Log(hit.collider.gameObject.GetComponent<ObjectInfo>().ObjectName);
            interactedObject = hit.collider.gameObject;
            if (interactedObject.tag == "Interactable")// || hit.collider.gameObject.tag == "Programmable")
            {

                if (!interacted && interactedObject.GetComponent<Interaction>() && Input.GetKeyDown("e"))
                {
                    interacted = true;
                    interactedObject.GetComponent<Interaction>().Interagir();
                }

                if (!notredo)
                {

                    GameObject.Find("Object_aimed").GetComponent<Text>().text = interactedObject.GetComponent<ObjectInfo>().ObjectName;
                    notredo = true;
                }
            }
            else
            {
                if (interactedObject != null)
                {

                    if (interactedObject.tag != "Interactable" && interactedObject.tag != "Programmable")
                        GameObject.Find("Object_aimed").GetComponent<Text>().text = "";
                }
                GameObject.Find("Object_aimed").GetComponent<Text>().text = "";
                notredo = false;
            }
        }
        else
        {
            if (interactedObject != null)
            {

                if (interactedObject.tag != "Interactable" && interactedObject.tag != "Programmable")
                    GameObject.Find("Object_aimed").GetComponent<Text>().text = "";
            }
            notredo = false;
        }
    }

}


