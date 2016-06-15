using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

    public static GameObject fireInHeater;
    public static GameObject logsInHeater;

    // Use this for initialization
    void Start () {
        fireInHeater = GameObject.Find("Fire");
        logsInHeater = GameObject.Find("Logs_fire");
        fireInHeater.SetActive(false);
        logsInHeater.SetActive(false);
    }

}
