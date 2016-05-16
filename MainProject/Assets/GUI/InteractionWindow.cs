using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionWindow : MonoBehaviour
{

    private GameObject glassObject;
    private GameObject particles;
    private GameObject panel0;
    private GameObject FPSController;
    private bool showPanel0;
    private bool filled;
    private bool isPaused;

    // Use this for initialization
    void Start()
    {
        panel0Init();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            isPaused = !isPaused;
            showPanel0 = !showPanel0;
            panelTrigger(showPanel0);
            Screen.lockCursor = false;
            FPSController.GetComponent<FirstPersonController>().enabled = !isPaused;
        }
    }

    public void fillGlass()
    {
        if (!filled)
        {
            glassObject.GetComponent<Animator>().SetBool("Glass_fill", true);
            filled = true;
        }
        particles.GetComponent<ParticleSystem>().Play();
    }

    public void emptyGlass()
    {
        glassObject.GetComponent<Animator>().SetBool("Glass_fill", false);
        filled = false;
        particles.GetComponent<ParticleSystem>().Stop();
        }

    void panel0Init()
    {
        isPaused = false;
        panel0 = GameObject.Find("Use_Panel0");
        glassObject = GameObject.Find("Final_glass_water");
        particles = GameObject.Find("Particle_holder");
        particles.GetComponent<ParticleSystem>().Stop();
        particles.GetComponent<ParticleSystem>().emissionRate = 200;
        FPSController = GameObject.Find("FPSController");
        filled = false;
        showPanel0 = false;
        panelTrigger(false);
    }

    void panelTrigger(bool show)
    {
        panel0.GetComponent<Image>().enabled = show;
        foreach (Transform child in panel0.transform)
        {
            child.gameObject.SetActive(show);
        }
    }

}