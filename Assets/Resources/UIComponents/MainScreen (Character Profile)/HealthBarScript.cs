using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    bool hasStarted = false;
    RectTransform Rect;

    float Hp = 0;

    void Start()
    {
        Rect = this.GetComponent<RectTransform>();
    }

    void Start_()
    {
        Hp = ClientManager.Character.CharStatus.hp;

        ClientManager.Character.CharStatus.OnChange += (changes) =>
        {
            var x = false;
            changes.ForEach((obj) =>
            {
                if (obj.Field == "hp") x = true;
            });

            if (x)
            {
                StopAllCoroutines();
                StartCoroutine("BarTransition");
            }
        };
    }

    void Update()
    {
        if (ClientManager.User == null || ClientManager.Character == null) return;
        if(hasStarted == false)
        {
            Start_();
            hasStarted = true;
        }

        Rect.sizeDelta = new Vector2(Hp/ClientManager.Character.CharStatus.maxHp*590,25);
    }

    IEnumerator BarTransition()
    {
        float index = 0;
        float acl = 0;
        float HpPres = Hp;
        while(index < 1)
        {
            index += acl;
            if (index > 1) index = 1;
            acl += Time.deltaTime/3;
            Hp = Mathf.Lerp(HpPres, ClientManager.Character.CharStatus.hp, index);
            yield return null;
        }
    }
}

