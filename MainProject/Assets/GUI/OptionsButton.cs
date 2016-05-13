using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{

    private GameObject panel0;
    private GameObject panel1;
    private GameObject panel2;
    private GameObject panel3;

    private GameObject[] panels = new GameObject[20];
    private bool[] showPanels = new bool[20];

    // Use this for initialization
    void Start()
    {
        panel0Init();
        panel1Init();
        panel2Init();
        panel3Init();
        panels[0] = panel0;
        panels[1] = panel1;
        panels[2] = panel2;
        panels[3] = panel3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
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
            case 3:
                if (!showPanels[3])
                {
                    hideAll();
                    showPanels[3] = !showPanels[3];
                }
                else hideAll();
                panelTrigger(showPanels[3], 3);
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
            case 3:
                panel3.GetComponent<Image>().enabled = show;
                foreach (Transform child in panel3.transform)
                {
                    child.gameObject.SetActive(show);
                }
                break;
            default:
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
        panelTrigger(false, 0);
    }



    private void panel1Init()
    {
        panel1 = GameObject.Find("Panel 1");
        showPanels[1] = false;
        panelTrigger(false, 1);
    }


    private void panel2Init()
    {
        panel2 = GameObject.Find("Panel 2");
        showPanels[2] = false;
        panelTrigger(false, 2);
    }

    private void panel3Init()
    {
        panel3 = GameObject.Find("Panel 3");
        showPanels[3] = false;
        panelTrigger(false, 3);
    }
}

