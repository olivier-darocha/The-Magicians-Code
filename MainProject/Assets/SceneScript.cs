using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour {

	public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
    }
}
