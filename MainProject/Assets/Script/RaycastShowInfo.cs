using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class RaycastShowInfo : MonoBehaviour
{

    private bool notredo = false;
    public bool interacted = false;
    private Ray ray;

    private GameObject FPSController;
    private static bool isPaused;
    public static bool IsPaused
    { get { return isPaused; } set { isPaused = value; } }
    private static GameObject interactedObject;
    public static GameObject InteractedObject
    {
        get { return interactedObject; }
        set { interactedObject = value; }
    }

    void Start()
    {
        FPSController = GameObject.Find("FPSController");
        isPaused = false;
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Bloque les raycast quand le jeu se met en pause avec les menus contextuels.
        if (Input.GetKeyUp(KeyCode.Mouse1) && Physics.Raycast(ray, out hit, 3))
        {
            interactedObject = hit.collider.gameObject;
            isPaused = !isPaused;
            Screen.lockCursor = !isPaused;
            FPSController.GetComponent<FirstPersonController>().enabled = !isPaused;
        }

        else
        {
            if (!isPaused && Physics.Raycast(ray, out hit, 3))
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
}


