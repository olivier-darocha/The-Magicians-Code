using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{

    private GameObject panel0;

    private GameObject panel1;

    private GameObject panel2;

    private GameObject[] panels = new GameObject[20];
    private bool[] showPanels = new bool[20];

    // Use this for initialization
    void Start()
    {
        panel0Init();
        panel1Init();
        panel2Init();
        panels[0] = panel0;
        panels[1] = panel1;
        panels[2] = panel2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            showPanels[0] = !showPanels[0];
            panelTrigger(showPanels[0], 0);
            hideAll();
        }

    }

    public void buttonTrigger(int i)
    {
        switch (i)
        {
            case 1:
                if (!showPanels[1])
                {
                    hideAll();
                    showPanels[1] = !showPanels[1];
                }
                else hideAll();
                panelTrigger(showPanels[1], 1);
                break;
            case 2:

                if (!showPanels[2])
                {
                    hideAll();
                    showPanels[2] = !showPanels[2];
                }
                else hideAll();
                panelTrigger(showPanels[2], 2);
                break;
            default:
                break;
        }

    }

    private void panelTrigger(bool show, int i)
    {
        switch (i)
        {
            case 0:
                panel0.GetComponent<Image>().enabled = show;
                foreach (Transform child in panel0.transform)
                {
                    child.gameObject.SetActive(show);
                }
                break;
            case 1:
                panel1.GetComponent<Image>().enabled = show;
                foreach (Transform child in panel1.transform)
                {
                    child.gameObject.SetActive(show);
                }
                break;
            case 2:
                panel2.GetComponent<Image>().enabled = show;
                foreach (Transform child in panel2.transform)
                {
                    child.gameObject.SetActive(show);
                }
                break;
        }

    }

    private void hideAll()
    {

        for (int j = 1; j < panels.Length; j++)
        {
            panelTrigger(false, j);
            showPanels[j] = false;
        }


    }

    private void panel0Init()
    {
        panel0 = GameObject.Find("Panel 0");
        showPanels[0] = false;
        panel0.GetComponent<Image>().enabled = false;
        foreach (Transform child in panel0.transform)
        {
            child.gameObject.SetActive(false);
        }
    }



    private void panel1Init()
    {
        panel1 = GameObject.Find("Panel 1");
        showPanels[1] = false;
        panel1.GetComponent<Image>().enabled = false;
        foreach (Transform child in panel1.transform)
        {
            child.gameObject.SetActive(false);
        }
    }


    private void panel2Init()
    {
        panel2 = GameObject.Find("Panel 2");
        showPanels[2] = false;
        panel2.GetComponent<Image>().enabled = false;
        foreach (Transform child in panel2.transform)
        {
            child.gameObject.SetActive(false);
        }
    }





}

