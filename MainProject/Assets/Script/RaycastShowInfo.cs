using UnityEngine;
using UnityEngine.UI;


public class RaycastShowInfo : MonoBehaviour
{

    private bool notredo = false;
    public bool interacted = false;
    public static Ray ray;
    public static RaycastHit hit;

    private GameObject FPSController;
    public static bool isPaused;
    public static GameObject interactedObject;
    

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
        RaycastHit hit;

        if (!isPaused && Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.gameObject.tag == "Interactable" || hit.collider.gameObject.tag == "Programmable")
            {
                if (!interacted && hit.collider.gameObject.GetComponent<Interaction>() && Input.GetKeyDown("e"))
                {
                        interacted = true;
                        hit.collider.gameObject.GetComponent<Interaction>().Interagir();
                }

                if (!notredo)
                {
                    GameObject.Find("Object_aimed").GetComponent<Text>().text = hit.collider.gameObject.GetComponent<ObjectInfo>().ObjectName;
                    notredo = true;
                }
            }
            else
            {
                GameObject.Find("Object_aimed").GetComponent<Text>().text = "";
                notredo = false;
            }
        }
        else
        {
            GameObject.Find("Object_aimed").GetComponent<Text>().text = "";
            notredo = false;
        }
    }

}


