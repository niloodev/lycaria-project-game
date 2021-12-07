using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBlockScript : MonoBehaviour
{
    public Dictionary<string, Sprite> ImageResources;
    CanvasGroup Blocked;
    public int skillId = 1000;
    bool st = false;

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        Blocked = this.transform.GetChild(0).GetComponent<CanvasGroup>();
    }

    void TaskOnClick()
    {
        if (skillId == 1000) return;
        // Envia pra uma classe específica da habilidade para iniciar o CHECKACTION.
        ClientManager.ClientManagerObj.SendMessage("VerifyPhase", skillId);
    }

    void Set()
    {
        Image ImageComponent = this.GetComponent<Image>();
        ClientManager.Character.skills[skillId].OnChange += (changes) =>
        {
            ImageComponent.sprite = ImageResources[ClientManager.Character.skills[skillId]._icon];   
        };

        ImageComponent.sprite = ImageResources[ClientManager.Character.skills[skillId]._icon];
    }

    void Update()
    {
        if (skillId == 1000) return;
        var x = 1;
        if (ClientManager.Character.skills[skillId].enabled) x = 0;
        ClientManager.Character.skills[skillId].blocked.ForEach((string key, bool value) =>
        {
            if (value == true) x = 1;
        });
        Blocked.alpha = x;
        if (st == true) return;
        Set();
        st = true;
    }
}
