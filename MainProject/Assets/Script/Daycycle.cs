using UnityEngine;
using System.Collections;

public class Daycycle : MonoBehaviour {

    public float speed;
    public Material[] skies;

	void Update () {
        transform.rotation = Quaternion.Euler(Time.time*speed%360,270,0);
        if ((name == "Sun" && transform.rotation.eulerAngles.x > 180) ||(name == "Moon" && transform.rotation.eulerAngles.x <= 180))
        {
            transform.GetChild(0).GetComponent<Light>().intensity = 0.05f;
        }
        else transform.GetChild(0).GetComponent<Light>().intensity = 2f;

        if (name == "Sun" && transform.rotation.eulerAngles.y == 270 && transform.rotation.eulerAngles.x < 30)
        {
            RenderSettings.skybox = skies[4];
        }
        else if (name == "Sun" && ((transform.rotation.eulerAngles.y == 270 && transform.rotation.eulerAngles.x > 30 && transform.rotation.eulerAngles.x <= 90) || (transform.rotation.eulerAngles.y == 90 && transform.rotation.eulerAngles.x > 30 && transform.rotation.eulerAngles.x <= 90)))
        {
            RenderSettings.skybox = skies[0];
        }
        else if (name == "Sun" && transform.rotation.eulerAngles.y == 90 && transform.rotation.eulerAngles.x <= 30 && transform.rotation.eulerAngles.x > 10)
        {
            RenderSettings.skybox = skies[1];
        }
        else if (name == "Sun" && transform.rotation.eulerAngles.y == 90 && ((transform.rotation.eulerAngles.x <= 10 && transform.rotation.eulerAngles.x >= 0) || (transform.rotation.eulerAngles.x <= 360 && transform.rotation.eulerAngles.x > 350)))
        {
            RenderSettings.skybox = skies[2];
        }
        else if (name == "Sun" && transform.rotation.eulerAngles.y == 90 && transform.rotation.eulerAngles.x <= 350)
        {
            RenderSettings.skybox = skies[3];
        }
    }
}
