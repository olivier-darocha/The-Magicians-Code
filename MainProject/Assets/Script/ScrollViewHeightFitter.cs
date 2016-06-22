using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollViewHeightFitter : MonoBehaviour {

    public GameObject Scrollbar;
    public float contentSize;
    public float contentPadding;
    public float contentSpacing;
    private int done;

    void Start()
    {
        if(transform.name == "Content")
        {
            done = (int)(this.GetComponent<RectTransform>().rect.width / contentSize);
            int i = transform.childCount;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, (i * contentSize + 2 * contentPadding + (i - 1) * contentSpacing + 80)/2);
        }
        else if(transform.name == "ProgContent")
        {
            int i = transform.GetChild(0).childCount;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, i*contentSize + 2*contentPadding + (i-1)*contentSpacing + 20);
        }

    }

    public void ChangeSize()
    {
        
        if (transform.name == "Content")
        {/*
            int i = transform.childCount;
            if (contentSize * 4 + 2 * contentPadding + 3 * contentSpacing > this.GetComponent<RectTransform>().rect.width && done == 4)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, this.GetComponent<RectTransform>().sizeDelta.y * (done / (done - 1)));
                done = 3;
            }
            else if (contentSize * 3 + 2 * contentPadding + 2 * contentSpacing > this.GetComponent<RectTransform>().rect.width && done == 3)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, this.GetComponent<RectTransform>().sizeDelta.y * (done / (done - 1)));
                done = 2;
            }
            else if (contentSize * 2 + 2 * contentPadding + contentSpacing > this.GetComponent<RectTransform>().rect.width && done == 2)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, this.GetComponent<RectTransform>().sizeDelta.y * (done / (done - 1)));
                done = 1;
            }
            else if (done == 1 && contentSize * 2 + 2 * contentPadding + contentSpacing <= this.GetComponent<RectTransform>().rect.width)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, this.GetComponent<RectTransform>().sizeDelta.y / ((done + 1) / done));
                done = 2;
            }
            else if (done == 2 && contentSize * 3 + 2 * contentPadding + 2 * contentSpacing <= this.GetComponent<RectTransform>().rect.width)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, this.GetComponent<RectTransform>().sizeDelta.y / ((done + 1) / done));
                done = 3;
            }
            else if (done == 3 && contentSize * 4 + 2 * contentPadding + 3 * contentSpacing <= this.GetComponent<RectTransform>().rect.width)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(-20, this.GetComponent<RectTransform>().sizeDelta.y / ((done + 1) / done));
                done = 4;
            }*/
        }
        else if (transform.name == "ProgContent")
        {
            float i = 0;
            int j;
            for(j = 0; j < transform.GetChild(0).childCount; j++)
            {
                i += transform.GetChild(0).GetChild(j).GetComponent<LayoutElement>().minHeight;
            }
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, i + 2 * contentPadding + (transform.GetChild(0).childCount - 1) * contentSpacing + 20);
        }
    }
}
