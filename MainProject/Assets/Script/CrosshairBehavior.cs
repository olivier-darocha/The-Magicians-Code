using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrosshairBehavior : MonoBehaviour {

    private GameObject crosshair;
    private GameObject textOverCrosshair;

	// Use this for initialization
	void Start () {
        crosshair = GameObject.Find("Crosshair");
        textOverCrosshair = GameObject.Find("Object_aimed");
    }
	
	// Update is called once per frame
	void Update () {
        if (textOverCrosshair.GetComponent<Text>().text.Equals(""))
        {
            if (RaycastShowInfo.isPaused)
            {
                Debug.Log("a");
                crosshair.GetComponent<Image>().enabled = false;
            }
            else
            {
                Debug.Log("b");
                crosshair.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            Debug.Log(textOverCrosshair.GetComponent<Text>().text);
            crosshair.GetComponent<Image>().enabled = false;
        }
    }
}
