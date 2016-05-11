using UnityEngine;
using System.Collections;

public class mainGUI : MonoBehaviour {
    public GUISkin mainSkin;
  
    private GUI.WindowFunction windowFunction, option1Function;

    private bool toggleBTN1;
    private bool window0Open;
    public Rect windowRect0 = new Rect(550, 300, 200, 320);
    public Rect windowRect1 = new Rect(770, 400, 200, 320);

    void Start()
    {
        toggleBTN1 = false;
        window0Open = true;
        windowFunction = DoMainWindow;
        option1Function = DoOption1Window;
    }

    void OnGUI()
    {
        GUI.skin = mainSkin;
       if(window0Open) windowRect0 = GUI.Window(0, windowRect0, windowFunction, "");
        if (toggleBTN1)
        {
            windowRect1 = GUI.Window(1, windowRect1, option1Function, "");
            Debug.Log(windowRect0.x);
        }
    }

    public void DoMainWindow(int ID)
    {
        
        GUILayout.BeginVertical();
        GUILayout.Space(50);
        if (GUILayout.Button("Option 1"))
        {
            window0Open = !window0Open;
            toggleBTN1 = !toggleBTN1;
        }
        GUILayout.Button("Option 2");
        GUILayout.Button("Option 3");
        GUILayout.EndVertical();


    }

    public void DoOption1Window(int ID)
    {
        GUILayout.BeginVertical();
        GUILayout.Space(50);
        GUILayout.Button("jhjhOption 1");
        GUILayout.Button("Ojjhption 2");
        GUILayout.Button("Ojhjhption 3");
        GUILayout.EndVertical();
    }



}
