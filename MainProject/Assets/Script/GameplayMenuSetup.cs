using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameplayMenuSetup : MonoBehaviour
{

    //Interacting

    private GameObject particles;
    private GameObject panel;
    private bool showPanel;


    //Programming
    public GameObject BAO;

    // Use this for initialization
    void Start()
    {
        interactionGlassInit();
    }

    void Update()
    {
        if (RaycastShowInfo.isPaused)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                switch (RaycastShowInfo.interactedObject.name)
                {
                    case "Final_glass_water":
                        interactionGlass();
                        break;
                    default:
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                BAO.SetActive(!BAO.activeSelf);
            }
        }

        else
        {
            hideInteraction();
        }
    }

    //interacting part
    void interactionGlassInit()
    {
        panel = GameObject.Find("Use_Glass");
        particles = GameObject.Find("Particle_holder");
        particles.GetComponent<ParticleSystem>().Stop();
        particles.GetComponent<ParticleSystem>().emissionRate = 200;
        hideInteraction();
    }

    void interactionGlass()
    {
        showPanel = !showPanel;
        panel.GetComponent<Image>().enabled = showPanel;
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(showPanel);
        }
    }

    void hideInteraction()
    {
        showPanel = false;
        panel.GetComponent<Image>().enabled = showPanel;
        foreach (Transform child in panel.transform)
        {
            child.gameObject.SetActive(showPanel);
        }
    }
}