using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityBarScript : MonoBehaviour
{
    bool goTo = false;
    public GameObject Entity;

    GameObject HealthBar;
    GameObject ManaBar;
    Text LevelText;
    Text NickNameText;
    RectTransform Health;
    float H;
    float M;
    RectTransform Mana;
    string entityId;

    Image HealthC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Start_()
    {
        HealthBar = this.transform.GetChild(0).gameObject;
        ManaBar = this.transform.GetChild(1).gameObject;
        Health = HealthBar.GetComponent<RectTransform>();
        Mana = ManaBar.GetComponent<RectTransform>();
        NickNameText = this.transform.GetChild(3).GetComponent<Text>();
        HealthC = HealthBar.GetComponent<Image>();

        LevelText = this.transform.GetChild(2).GetChild(0).GetComponent<Text>();

        entityId = Entity.name;

        H = ClientManager.GameRoom.State.entities[entityId].CharStatus.hp;
        M = ClientManager.GameRoom.State.entities[entityId].CharStatus.mana;

        ClientManager.GameRoom.State.entities[entityId].CharStatus.OnChange += (changes) =>
        {
            var x = false;
            var y = false;
            changes.ForEach((obj) =>
            {
                if (obj.Field == "hp") x = true;
                if (obj.Field == "mana") y = true;
            });

            if (x)
            {
                StopCoroutine("BarTransitionHp");
                StartCoroutine("BarTransitionHp");
            }

            if (y)
            {
                StopCoroutine("BarTransitionMana");
                StartCoroutine("BarTransitionMana");
            }
        };

        if(ClientManager.GameRoom.State.entities[entityId].team == "N")
        {
            HealthC.color = new Color32(255, 255, 255, 255);
        } else if (ClientManager.GameRoom.State.entities[entityId].team != ClientManager.Character.team)
        {
            HealthC.color = new Color32(188, 93, 93, 255);
        } else if (ClientManager.GameRoom.State.entities[entityId].team == ClientManager.Character.team)
        {
            HealthC.color = new Color32(149, 217, 146, 255);
        }

        if (ClientManager.Character.entityId == ClientManager.GameRoom.State.entities[entityId].entityId)
        {
            LevelText.color = new Color32(255, 255, 155, 255);
            NickNameText.color = new Color32(255, 255, 155, 255);
        }

        if(ClientManager.GameRoom.State.entities[entityId].playerId != "")
        {
            NickNameText.text = ClientManager.GameRoom.State.player[ClientManager.GameRoom.State.entities[entityId].playerId].nickName;
        } else
        {
            NickNameText.text = ClientManager.GameRoom.State.entities[entityId].id;
        }
        

        goTo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goTo) return;

        this.GetComponent<RectTransform>().position = new Vector3(Entity.transform.position.x, Entity.transform.position.y + Entity.GetComponent<SpriteRenderer>().bounds.size.y - 0.3f, -75);

        Health.sizeDelta = new Vector2(0.8f* (H / ClientManager.GameRoom.State.entities[entityId].CharStatus.maxHp), 0.07f);
        Mana.sizeDelta = new Vector2(0.8f * (M / ClientManager.GameRoom.State.entities[entityId].CharStatus.maxMana), 0.03f);

        LevelText.text = ClientManager.GameRoom.State.entities[entityId].level.ToString();
    }

    IEnumerator BarTransitionHp()
    {
        float index = 0;
        float acl = 0;
        float HpPres = H;
        while (index < 1)
        {
            index += acl;
            if (index > 1) index = 1;
            acl += Time.deltaTime / 3;
            H = Mathf.Lerp(HpPres, ClientManager.GameRoom.State.entities[entityId].CharStatus.hp, index);
            yield return null;
        }
    }

    IEnumerator BarTransitionMana()
    {
        float index = 0;
        float acl = 0;
        float ManaPres = M;
        while (index < 1)
        {
            index += acl;
            if (index > 1) index = 1;
            acl += Time.deltaTime / 3;
            M = Mathf.Lerp(ManaPres, ClientManager.GameRoom.State.entities[entityId].CharStatus.mana, index);
            yield return null;
        }
    }
}
