using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionGlass : MonoBehaviour
{

    private GameObject glassObject;
    private GameObject particles;
    public static bool filled = false;
    public static bool allowFill;
    void Start()
    {
        allowFill = false;
        glassObject = GameObject.Find("Final_glass_water");
        particles = GameObject.Find("Particle_holder");
}
    public void fillGlass()
    {

        if (!filled)
        {
            glassObject.GetComponent<Animator>().SetBool("Glass_fill", true);
            filled = true;
            GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = "Rempli";
        }
        if (!allowFill)
        {
            particles.GetComponent<ParticleSystem>().Clear();
            particles.GetComponent<ParticleSystem>().Play();
        }
    }

    public void emptyGlass()
    {
        glassObject.GetComponent<Animator>().SetBool("Glass_fill", false);
        filled = false;
        GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = "Vide";
        particles.GetComponent<ParticleSystem>().Stop();
    }
}