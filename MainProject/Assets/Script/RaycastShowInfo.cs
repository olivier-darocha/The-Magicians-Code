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
    public static bool isPaused;
    public static GameObject interactedObject;


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
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (!isPaused && Physics.Raycast(ray, out hit, 3))
            {
                if (hit.collider.gameObject.tag == "Programmable")
                {
                    interactedObject = hit.collider.gameObject;
                    // desactive le déplacement du joueur et de la camera (isPau
                    Screen.lockCursor = isPaused;
                    FPSController.GetComponent<FirstPersonController>().enabled = isPaused;
                    isPaused = !isPaused;
                }
            }
            else if(isPaused)
            {
                Screen.lockCursor = isPaused;
                FPSController.GetComponent<FirstPersonController>().enabled = isPaused;
                isPaused = !isPaused;
            }
        }

        else
        {
            if (!isPaused && Physics.Raycast(ray, out hit, 3))
            {
                if (hit.collider.gameObject.tag == "Interactable" || hit.collider.gameObject.tag == "Programmable")
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
}


