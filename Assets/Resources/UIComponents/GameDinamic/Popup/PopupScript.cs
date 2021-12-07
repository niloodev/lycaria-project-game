using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour
{
    Text t;

    public void Start_(string value, string type, int[] color_)
    {
        t = this.GetComponent<Text>();
        t.text = value;

        switch (type)
        {
            case "md":
                t.color = new Color32(160, 103, 255, 255);
                break;
            case "pd":
                t.color = new Color32(255, 149, 139, 255);
                break;
            case "td":
                t.color = new Color32(96, 255, 71, 255);
                break;
            case "h":
                t.color = new Color32(107, 255, 93, 255);
                break;
            case "th":
                t.color = new Color32(255, 255, 255, 255);
                break;
            case "custom":
                t.color = new Color(color_[0]/255, color_[1]/255, color_[2]/255, color_[3]/255);
                break;

        }
    }

    void Update()
    {
        this.transform.position += new Vector3(0, 1.5f, 0) * Time.deltaTime;
        t.color -= new Color(0, 0, 0, 1.5f) * Time.deltaTime;

        if (t.color.a <= 0) Destroy(this.gameObject);

    }
}
