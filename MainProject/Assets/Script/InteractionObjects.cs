using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionObjects : MonoBehaviour
{

    private GameObject glassObject;
    private GameObject particles;
    public static bool empty = true;
    public static bool allowFill;
    public static bool allowEmpty;

    void Start()
    {
        allowFill = false;
        allowEmpty = false;
        glassObject = GameObject.Find("Final_glass_water");
        particles = GameObject.Find("Particle_holder");
    }

    public void fillGlass()
    {
        if (empty)
        {
            glassObject.GetComponent<Animator>().SetBool("Glass_fill", true);
            empty = false;
            GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = "Rempli";
        }

        particles.GetComponent<ParticleSystem>().Clear();

        if (!allowFill)
            particles.GetComponent<ParticleSystem>().Play();
    }

    public void emptyGlass()
    {
        if (allowEmpty)
        {
            glassObject.GetComponent<Animator>().SetBool("Glass_fill", false);
            empty = true;
            GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = "Vide";
            particles.GetComponent<ParticleSystem>().Stop();
        }
    }
}