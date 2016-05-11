using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour {

    private bool showPanel1;

    private GameObject panel1;
	// Use this for initialization
	void Start () {
        panel1 = GameObject.Find("Panel 1");
        panel1Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void button0Trigger()
    {
        showPanel1 = !showPanel1;
        panel1Trigger(showPanel1);

    }

    private void panel1Init()
    {
        showPanel1 = false;
        panel1.SetActive(false);
        foreach (Transform child in panel1.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void panel1Trigger(bool show)
    {
        panel1.SetActive(show);
        foreach (Transform child in panel1.transform)
        {
            child.gameObject.SetActive(show);
        }
    }
}

