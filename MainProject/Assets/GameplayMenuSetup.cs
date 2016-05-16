using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameplayMenuSetup : MonoBehaviour
{
    private GameObject FPSController;
    private bool isPaused;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
        FPSController = GameObject.Find("FPSController");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isPaused = !isPaused;
            Screen.lockCursor = false;
            FPSController.GetComponent<FirstPersonController>().enabled = !isPaused;
        }
    }
}