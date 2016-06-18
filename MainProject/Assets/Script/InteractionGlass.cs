using UnityEngine;
using System.Collections;


public class InteractionGlass : MonoBehaviour
{
    private GameObject water;
    private GameObject glassObject;
    private GameObject particles;
    public static float quantity;
    public static bool overflow;
    private string[] modeNames; // paramètres de remplissage
    public static int fillMode; // mode de remplissage
    public static bool allowEmpty;
    public static bool allowFill;

    void Start()
    {
        modeNames = new string[3];
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

        GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[0] = translateQuantity(quantity);
        

    }

    public IEnumerator fillGlass()
    {
        if (allowFill)
        {
            resetAllTriggers();
            fill(fillMode);
            /*switch (toolUsed)
            {
                case 0: //if
                    ifFillMode(fillMode);
                    break;
                case 3: // while
                    whileFillMode(fillMode);
                    break;
            }*/
        }
        yield return null;
    }

    public void fill(int mode)
    {
        Debug.Log(quantity);
        switch (mode)
        {

            case 0: // état non-remplissage
                quantity = 0;
                
                break;
            case 1:
                quantity = (1f/3f);
                
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[0]);
                break;
            case 2:
                quantity = (2f/3f);
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[1]);
                break;
            case 3:
                quantity = 1;
                glassObject.GetComponent<Animator>().SetTrigger(modeNames[2]);
                break;
            case 4:
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
                case 0: // état non-remplissage
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
                    particles.GetComponent<ParticleSystem>().Clear();
                    particles.GetComponent<ParticleSystem>().Play();
                    break;
            }
            i++;
        }
    }

    public void emptyGlass()
    {
        quantity = 0;
        if (water.GetComponent<RectTransform>().localScale.y > 0)
        {
            resetAllTriggers();
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
        else if (q == 1f/3f)
            return "1/3";
        else if (q == 2f/3f)
            return "2/3";
        else if (q == 1)
            return "3/3";
        return q.ToString();
    }

    void resetAllTriggers()
    {
        for (int i = 0; i < modeNames.Length; i++)
        {
            glassObject.GetComponent<Animator>().ResetTrigger(modeNames[i]);
        }
        glassObject.GetComponent<Animator>().ResetTrigger("Drink");
    }
}