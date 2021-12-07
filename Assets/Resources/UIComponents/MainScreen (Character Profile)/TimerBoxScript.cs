using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimerBoxScript : MonoBehaviour
{
    bool Started = false;
    Text TimerText;
    Text TimerTextShadow;
    RectTransform Bar;

    float Timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        TimerText = this.transform.GetChild(2).GetComponent<Text>();
        TimerTextShadow = this.transform.GetChild(3).GetComponent<Text>();
        Bar = this.transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Start_()
    {
        ClientManager.GameRoom.State.OnChange += ((changes) =>
        {
            var x = false;
            changes.ForEach((obj) =>
            {
                if (obj.Field == "turnCount") x = true;
            });

            if(x == true)
            {
                StopAllCoroutines();
                StartCoroutine("Animate");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (ClientManager.Loaded == false) return;

        if(Started == false)
        {
            Start_();
            Started = true;
        }

        TimerText.text = ClientManager.GameRoom.State.turnCount.ToString();
        TimerTextShadow.text = ClientManager.GameRoom.State.turnCount.ToString();
        Bar.sizeDelta = new Vector2((Timer / 40) * 895, 3.99999f);

    }

    IEnumerator Animate()
    {
        float time = 0;
        while(Timer != ClientManager.GameRoom.State.turnCount)
        {
            time += Time.deltaTime;
            Timer = Mathf.Lerp(Timer, ClientManager.GameRoom.State.turnCount, time);
            yield return null;
        }
    }

}
