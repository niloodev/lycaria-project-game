using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUpdater : MonoBehaviour
{
    bool alreadyStarted = false;

    Text Level;
    Image CharPic;

    GameObject HealthBar;
    Text HealthText;
    Text HealthShadow;
    GameObject HealthFrontBar;

    GameObject ManaBar;
    Text ManaText;
    Text ManaShadow;
    GameObject ManaFrontBar;

    Text ApText;
    Text AdText;
    Text VitText;
    Text PmText;
    Text PfText;
    Text MvpText;
    Text DmText;
    Text DfText;
    Text VampText;

    GameObject TopSide;
    Text TextB;
    Text TextBShadow;

    void Start()
    {
        Level = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        CharPic = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

        HealthBar = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        HealthText = HealthBar.transform.GetChild(3).GetChild(1).GetComponent<Text>();
        HealthShadow = HealthBar.transform.GetChild(3).GetChild(0).GetComponent<Text>();
        HealthFrontBar = HealthBar.transform.GetChild(2).gameObject;

        ManaBar = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(4).gameObject;
        ManaText = ManaBar.transform.GetChild(3).GetChild(1).GetComponent<Text>();
        ManaShadow = ManaBar.transform.GetChild(3).GetChild(0).GetComponent<Text>();
        ManaFrontBar = ManaBar.transform.GetChild(2).gameObject;

        ApText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetComponent<Text>();
        AdText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(3).GetComponent<Text>();
        VitText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(6).GetComponent<Text>();
        PmText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>();
        PfText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(4).GetComponent<Text>();
        MvpText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(7).GetComponent<Text>();
        DmText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(2).GetComponent<Text>();
        DfText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(5).GetComponent<Text>();
        VampText = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(8).GetComponent<Text>();

        TopSide = GameObject.Find("TopText");
        TextB = TopSide.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        TextBShadow = TopSide.transform.GetChild(1).GetChild(0).GetComponent<Text>();
    }

    void Start_()
    {
        CharPic.sprite = Resources.Load<Sprite>("Characters/" + ClientManager.Character.id + "/Render/" + ClientManager.Character.skin + "/icon");
    }

    void Update()
    {
        if (ClientManager.User == null || ClientManager.Character == null) return;
        if(alreadyStarted == false)
        {
            Start_();
            alreadyStarted = true; 
        }

        // Atualização de Level
        Level.text = Mathf.Round(ClientManager.Character.level).ToString();

        // Atualizações da Barra de Vida
        HealthText.text = Mathf.Round(ClientManager.Character.CharStatus.hp).ToString() + " / " + Mathf.Round(ClientManager.Character.CharStatus.maxHp).ToString();
        HealthShadow.text = Mathf.Round(ClientManager.Character.CharStatus.hp).ToString() + " / " + Mathf.Round(ClientManager.Character.CharStatus.maxHp).ToString();

        // Atualizações da Barra de Mana
        ManaText.text = Mathf.Round(ClientManager.Character.CharStatus.mana).ToString() + " / " + Mathf.Round(ClientManager.Character.CharStatus.maxMana).ToString();
        ManaShadow.text = Mathf.Round(ClientManager.Character.CharStatus.mana).ToString() + " / " + Mathf.Round(ClientManager.Character.CharStatus.maxMana).ToString();

        // Atualização dos Status
        ApText.text = Mathf.Round(ClientManager.Character.CharStatus.ap).ToString() + " AP";
        AdText.text = Mathf.Round(ClientManager.Character.CharStatus.ad).ToString() + " AD";
        VitText.text = Mathf.Round(ClientManager.Character.CharStatus.vitality).ToString() + " VIT";
        PmText.text = Mathf.Round(ClientManager.Character.CharStatus.mrPainPlane).ToString() + " / " + Mathf.Round(ClientManager.Character.CharStatus.mrPainPorc).ToString() + "% PM";
        PfText.text = Mathf.Round(ClientManager.Character.CharStatus.arPainPlane).ToString() + " / " + Mathf.Round(ClientManager.Character.CharStatus.arPainPorc).ToString() + "% PF";
        MvpText.text = Mathf.Round(ClientManager.Character.CharStatus.mvp).ToString() + " MVP";
        DmText.text = Mathf.Round(ClientManager.Character.CharStatus.mr).ToString() + " DM";
        DfText.text = Mathf.Round(ClientManager.Character.CharStatus.ar).ToString() + " DF";
        VampText.text = Mathf.Round(ClientManager.Character.CharStatus.vitality).ToString() + "% VAMP";

        /////// ATUALIZAÇÃO DE NOMENCLATURA DE ROUNDS
        switch (ClientManager.GameRoom.State.turnPriority)
        {
            case "off":
                TextB.text = "JOGO COMEÇANDO";
                TextBShadow.text = "JOGO COMEÇANDO";
                break;
            case "preturn":
                TextB.text = "PRE-TURNO";
                TextBShadow.text = "PRE-TURNO";
                break;
            case "posturn":
                TextB.text = "POS-TURNO";
                TextBShadow.text = "POS-TURNO";
                break;
            case "round":
                TextB.text = "INDO PARA O PROXIMO ROUND";
                TextBShadow.text = "INDO PARA O PROXIMO ROUND";
                break;
            default:
                if(ClientManager.GameRoom.State.turnPriority == ClientManager.Character.entityId)
                {
                    TextB.text = "SUA VEZ!";
                    TextBShadow.text = "SUA VEZ!";
                } else
                {
                    if(ClientManager.GameRoom.State.entities[ClientManager.GameRoom.State.turnPriority].playerId == "")
                    {
                        TextB.text = "VEZ DE " + ClientManager.GameRoom.State.entities[ClientManager.GameRoom.State.turnPriority].id;
                        TextBShadow.text = "VEZ DE " + ClientManager.GameRoom.State.entities[ClientManager.GameRoom.State.turnPriority].id;
                    }
                    else
                    {
                        TextB.text = "VEZ DE " + ClientManager.GameRoom.State.player[ClientManager.GameRoom.State.entities[ClientManager.GameRoom.State.turnPriority].playerId].nickName;
                        TextBShadow.text = "VEZ DE " + ClientManager.GameRoom.State.player[ClientManager.GameRoom.State.entities[ClientManager.GameRoom.State.turnPriority].playerId].nickName;
                    }
                }
                break;
        }
    }
}
