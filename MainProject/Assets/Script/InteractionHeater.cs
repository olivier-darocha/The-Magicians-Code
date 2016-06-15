using UnityEngine;
using System.Collections;

public class InteractionHeater : MonoBehaviour
{

    private GameObject[] logs;
    public static int logNum;
    private GameObject heater;
    private GameObject fire;
    private GameObject logsInFire;
    private GameObject snow;
    private string[] triggers;
    public static int triggerNum;
    public static bool fireState;

    void Start()
    {
        fireState = false;
        triggerNum = 0;
        triggers = triggersInit();
        fire = Initializer.fireInHeater;
        logsInFire = Initializer.logsInHeater;
        snow = GameObject.Find("snow");
        heater = GameObject.Find("Chauffage");
        logNum = 0;
        logs = getLogsInScene();
    }

    public IEnumerator addLog()
    {
        int i = 250;
        if (logNum < logs.Length && logNum != 0)
        {
            GameObject.Destroy(logs[logNum]);
            showFire();

        }
        if (snow.transform.localScale.y > 0)            
            StartCoroutine("triggerAnimation");

        while (i != 0)
        {
            i--;
            yield return null;
        }

        disableFire();
        yield return null;
    }

    string[] triggersInit()
    {
        string[] s = new string[4];
        for (int i = 0; i < s.Length; i++)
        {
            s[i] = "Melt" + (i+1);

        }
        return s;
    }

    GameObject[] getLogsInScene()
    {

        GameObject[] l = GameObject.FindGameObjectsWithTag("Logs");
        GameObject[] a = new GameObject[l.Length + 1];
        a[0] = null;
        for (int i = 0; i < l.Length; i++)
        {
            a[i + 1] = l[i];
        }

        return a;
    }

    IEnumerator triggerAnimation()
    {
        switch (triggerNum)
        {
            case 0:
                break;
            case 1:
                resetAllTriggers();
                snow.GetComponent<Animator>().SetTrigger(triggers[0]);
                break;
            case 2:
                resetAllTriggers();
                snow.GetComponent<Animator>().SetTrigger(triggers[1]);
                break;
            case 3:
                resetAllTriggers();
                snow.GetComponent<Animator>().SetTrigger(triggers[2]);
                break;
            case 4:   
                resetAllTriggers();
                snow.GetComponent<Animator>().SetTrigger(triggers[3]);
                break;
        }

        GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[2] = (4 - triggerNum).ToString(); ;
        yield return null;
    }

    void resetAllTriggers()
    {
        for(int i = 0; i< triggers.Length; i++)
        {
            snow.GetComponent<Animator>().ResetTrigger(triggers[i]); 
        }
    }

    void showFire()
    {
        fireState = true;
        fire.SetActive(true);
        logsInFire.SetActive(true);
        GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[1] = "Allumé";
    }

    void disableFire()
    {
        fireState = false;
        fire.SetActive(false);
        logsInFire.SetActive(false);
        GameObject.Find("Variables_List").GetComponent<VariablesInfo>().VariablesValue[1] = "Eteint";

    }
}
