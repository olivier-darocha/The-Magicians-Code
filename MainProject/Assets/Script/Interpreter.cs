using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

public class Interpreter : MonoBehaviour
{

    //TODO interpret drag-and-drop program with tools identifiers
    private string userInputSequence;
    private bool confirmInput;
    private bool success;
    private int count;
    private string fillGlassSequence; // initialiser en fonction de la fonction choisie
    private string testEndMessage;
    private string subSequence;
    private GameObject programmingWindow;
    private string toolsTag;

    // Use this for initialization
    void Start()
    {
        confirmInput = false;
        success = false;
        count = 0;
        testEndMessage = "";
        subSequence = "";
        toolsTag = "Tool";
        programmingWindow = GameObject.Find("Programming");
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmInput)
        {
            StartCoroutine("getUserInputSequence");
            string[] parameters = new string[2] { userInputSequence, fillGlassSequence};
            StartCoroutine("longestCommonSubstring", parameters);

            
            if (fillGlassSequence.Length == count)
            {
                testEndMessage = "Succès !";
                success = true;
            }
            else
            {
                if (Math.Abs(fillGlassSequence.Length - count) > 3)
                {
                    testEndMessage = "Complétement faux";
                    success = false;
                }
                else if (Math.Abs(fillGlassSequence.Length - count) <= 2)
                {
                    testEndMessage = "C'est presque ça !";
                    success = false;
                }
            }
        }

    }

    IEnumerator longestCommonSubstring(string[] str)
    {
        
        subSequence = string.Empty;
        if (String.IsNullOrEmpty(str[0]) || String.IsNullOrEmpty(str[1]))
            yield return 0;

        int[,] num = new int[str[0].Length, str[1].Length];
        int maxlen = 0;
        int lastSubsBegin = 0;
        StringBuilder sequenceBuilder = new StringBuilder();

        for (int i = 0; i < str[0].Length; i++)
        {
            for (int j = 0; j < str[1].Length; j++)
            {
                if (str[0][i] != str[1][j])
                    num[i, j] = 0;
                else
                {
                    if ((i == 0) || (j == 0))
                        num[i, j] = 1;
                    else
                        num[i, j] = 1 + num[i - 1, j - 1];

                    if (num[i, j] > maxlen)
                    {
                        maxlen = num[i, j];
                        int thisSubsBegin = i - num[i, j] + 1;
                        if (lastSubsBegin == thisSubsBegin)
                        {
                            sequenceBuilder.Append(str[0][i]);
                        }
                        else
                        {
                            lastSubsBegin = thisSubsBegin;
                            sequenceBuilder.Length = 0;
                            sequenceBuilder.Append(str[0].Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                        }
                    }
                }
            }
        }
        subSequence = sequenceBuilder.ToString();
        count = maxlen;
        yield return null; 
    }

    IEnumerator getUserInputSequence()
    {
        List<GameObject> childList = new List<GameObject>();
        foreach (Transform child in programmingWindow.transform)
        {
            if (child.tag == toolsTag)
            {
                childList.Add(child.gameObject);
            }
        }

        string inputSequence = "";


        foreach(GameObject obj in childList)
        {
            inputSequence += obj.GetComponent<ToolId>().id;
        }

        userInputSequence = inputSequence;
        yield return null;
    }
}
