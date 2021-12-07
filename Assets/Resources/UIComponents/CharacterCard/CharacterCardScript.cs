using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardScript : MonoBehaviour
{
    public string UserId;
    CanvasGroup Canvas;

    PlayerSchema player;
    Character character;

    Image StatusImage;

    public void Render()
    {
        Canvas = this.GetComponent<CanvasGroup>();

        Canvas.alpha = 0;

        player = ClientManager.GameRoom.State.player[UserId];
        character = ClientManager.GameRoom.State.entities[player.characterAttached];

        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Characters/" + character.id + "/Render/" + character.skin + "/splash");

        Transform trans = this.transform.GetChild(0);
        trans.GetChild(0).gameObject.GetComponent<Text>().text = player.nickName;
        if(ClientManager.User.id == player.id) trans.GetChild(0).gameObject.GetComponent<Text>().color = new Color32(255, 253, 174, 255);
        trans.GetChild(1).gameObject.GetComponent<Text>().text = character.id;

        StatusImage = trans.GetChild(2).gameObject.GetComponent<Image>();

        this.transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        while(Canvas.alpha < 1)
        {
            Canvas.alpha += Time.deltaTime / 0.75f;
            yield return null;
        }
    }

    private void Update()
    {
        if(player != null)
        {
            if (player.ready)
            {
                StatusImage.color = new Color32(158, 255, 148, 255);
            } else {
                StatusImage.color = new Color32(255, 168, 172, 255);
            }
        }
    }
}
