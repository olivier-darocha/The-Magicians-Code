using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

    private bool ouvert = false;

    public void Interagir()
    {
        if(GetComponent<ObjectInfo>().ObjectName == "Porte")
        {
            if (!ouvert)
            {
                transform.parent.GetComponent<Animator>().SetBool("Ferme", false);
                transform.parent.GetComponent<Animator>().SetBool("Ouvre",true);
                GameObject.Find("FPSController").GetComponent<RaycastShowInfo>().interacted = false;
                ouvert = true;
            }
            else
            {
                transform.parent.GetComponent<Animator>().SetBool("Ouvre", false);
                transform.parent.GetComponent<Animator>().SetBool("Ferme",true);
                GameObject.Find("FPSController").GetComponent<RaycastShowInfo>().interacted = false;
                ouvert = false;
            }
        }
    }
}
