using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionObjects : MonoBehaviour
{
    private GameObject water;
    private GameObject glassObject;
    private GameObject particles;
    public static float quantity;
    public static bool overflow;
    public static int toolUsed; // 0 = if, 1 = while, etc...
    private string[] modeNames; // paramètres de remplissage
    public static int fillMode; // mode de remplissage
    public static bool allowEmpty;
    public static bool allowFill;

    void Start()
    {
        quantity = 0;
        modeNames = new string[4];
        modeNames[0] = "Water" + 1.ToString();
        modeNames[1] = "Water" + 2.ToString();
        modeNames[2] = "Water" + 3.ToString();
        fillMode = 0;
        allowEmpty = false;
        allowFill = false;
        water = GameObject.Find("water_in_glass");
        glassObject = GameObject.Find("Final_glass_water");
        particles = GameObject.Find("Particle_holder");
    }

    void Update()
    {
        quantity = water.GetComponent<RectTransform>().localScale.y;
        GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = translateQuantity(quantity);

    }

    public IEnumerator fillGlass()
    {



        if (allowFill)
        {
            if (overflow)
            {
                particles.GetComponent<ParticleSystem>().Clear();
                particles.GetComponent<ParticleSystem>().Play();
                overflow = false;
            }


            if (water.GetComponent<RectTransform>().localScale.y < 1)
            {
                switch (toolUsed)
                {
                    case 0: //if
                        ifFillMode(fillMode);
                        break;
                    case 3:
                        whileFillMode(fillMode);
                        break;
                }

            }
            else
            {
                particles.GetComponent<ParticleSystem>().Clear();
                particles.GetComponent<ParticleSystem>().Play();
            }
        }
        yield return null;
    }

    public void ifFillMode(int mode)
    {
        switch (mode)
        {
            case 0:
                break;
            case 1:
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[0]);

                break;
            case 2:
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[1]);

                break;
            case 3:
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[2]);
                break;
            case 4:
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[2]);
                particles.GetComponent<ParticleSystem>().Clear();
                particles.GetComponent<ParticleSystem>().Play();
                break;
        }
    }
    public void whileFillMode(int mode)
    {
        int i = 0;
        while (i <= mode)
        {
            switch (mode)
            {
                case 0:
                    break;
                case 1:
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[0]);
                    break;
                case 2:
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[0]);
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[1]);
                    break;
                case 3:
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[0]);
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[1]);
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[2]);
                    break;
                case 4:
                    glassObject.GetComponent<Animator>().SetTrigger(modeNames[2]);
                    particles.GetComponent<ParticleSystem>().Clear();
                    particles.GetComponent<ParticleSystem>().Play();
                    break;
            }
        }
    }

    public void emptyGlass()
    {
        if (water.GetComponent<RectTransform>().localScale.y > 0)
        {
            glassObject.GetComponent<Animator>().SetTrigger("Drink");
            fillMode = 0;
            GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = "0";
            particles.GetComponent<ParticleSystem>().Stop();
        }
    }

    public string translateQuantity(float q)
    {
        if (q == 0)
            return "0";
        else if (q < 0.4)
            return "1/3";
        else if (q < 0.7)
            return "2/3";
        else if (q <= 1)
            return "3/3";

        return "error";
    }
}