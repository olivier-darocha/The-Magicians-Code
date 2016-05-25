using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

public class Interpreter : MonoBehaviour
{

    //TODO interpret drag-and-drop program with tools identifiers
    private int[] userInputSequence;
    private bool confirmInput;
    private bool success;
    private int count;
    private int[] fillGlassSequence; // initialiser en fonction de la fonction choisie
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
            userInputSequence = getUserInputSequence();
            count = longestCommonSubstring(userInputSequence.ToString(), fillGlassSequence.ToString(), out subSequence);

            Debug.Log("answer = " + fillGlassSequence.ToString());
            Debug.Log("\nuser = " + userInputSequence.ToString());
            Debug.Log("\nsub sequence = " + subSequence);

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
                else if(Math.Abs(fillGlassSequence.Length - count) <= 2)
                {
                    testEndMessage = "C'est presque ça !";
                    success = false;
                }
            }
            Debug.Log("\n" + testEndMessage);
        }

    }

    public int longestCommonSubstring(string str1, string str2, out string sequence)
    {
        sequence = string.Empty;
        if (String.IsNullOrEmpty(str1) || String.IsNullOrEmpty(str2))
            return 0;

        int[,] num = new int[str1.Length, str2.Length];
        int maxlen = 0;
        int lastSubsBegin = 0;
        StringBuilder sequenceBuilder = new StringBuilder();

        for (int i = 0; i < str1.Length; i++)
        {
            for (int j = 0; j < str2.Length; j++)
            {
                if (str1[i] != str2[j])
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
                            sequenceBuilder.Append(str1[i]);
                        }
                        else
                        {
                            lastSubsBegin = thisSubsBegin;
                            sequenceBuilder.Length = 0;
                            sequenceBuilder.Append(str1.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                        }
                    }
                }
            }
        }
        sequence = sequenceBuilder.ToString();
        return maxlen;
    }

    public int[] getUserInputSequence()
    {
        List<GameObject> childList = new List<GameObject>();
        foreach (Transform child in programmingWindow.transform)
        {
            if (child.tag == toolsTag)
            {
                childList.Add(child.gameObject);
            }
        }

        int[] inputSequence = new int[childList.Count];
        for(int i = 0; i < childList.Count; i++)
        {
            inputSequence[i] = childList[i].GetComponent<ToolId>().id;
        }

        return inputSequence;
    }
}
