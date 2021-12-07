using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarScript : MonoBehaviour
{
    bool hasStarted = false;
    RectTransform Rect;

    float Mana = 0;

    // Start is called before the first frame update
    void Start()
    {
        Rect = this.GetComponent<RectTransform>();
    }

    void Start_()
    {
        Mana = ClientManager.Character.CharStatus.mana;

        ClientManager.Character.CharStatus.OnChange += (changes) =>
        {
            var x = false;
            changes.ForEach((obj) =>
            {
                if (obj.Field == "mana") x = true;
            });

            if (x)
            {
                StopAllCoroutines();
                StartCoroutine("BarTransition");
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (ClientManager.User == null || ClientManager.Character == null) return;
        if (hasStarted == false)
        {
            Start_();
            hasStarted = true;
        }

        Rect.sizeDelta = new Vector2(Mana / ClientManager.Character.CharStatus.maxMana * 590, 15);
    }

    IEnumerator BarTransition()
    {
        float index = 0;
        float acl = 0;
        float ManaPres = Mana;
        while (index < 1)
        {
            index += acl;
            if (index > 1) index = 1;
            acl += Time.deltaTime / 3;
            Mana = Mathf.Lerp(ManaPres, ClientManager.Character.CharStatus.mana, index);
            yield return null;
        }
    }
}
