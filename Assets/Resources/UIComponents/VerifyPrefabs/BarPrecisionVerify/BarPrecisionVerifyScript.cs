using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RSG;

public class BarPrecisionVerifyScript : MonoBehaviour
{
    GameObject Zone;

    RectTransform RedZone;
    RectTransform YellowZone;
    RectTransform GreenZone;
    RectTransform BlueZone;

    RectTransform Bar;

    float YellowRadius = 0f;
    float GreenRadius = 0f;
    float BlueRadius = 0f;

    float BarSpeed = 2f;
    float Direction = 1;

    Promise<string> VerifyResult;
    bool Started = false;

    public IPromise<string> Initialize(float YR, float GR, float BR, float speed)
    {
        VerifyResult = new Promise<string>();

        Zone = this.transform.GetChild(2).gameObject;
        RedZone = Zone.transform.GetChild(0).GetComponent<RectTransform>();
        YellowZone = Zone.transform.GetChild(1).GetComponent<RectTransform>();
        GreenZone = Zone.transform.GetChild(2).GetComponent<RectTransform>();
        BlueZone = Zone.transform.GetChild(3).GetComponent<RectTransform>();

        Bar = this.transform.GetChild(3).GetComponent<RectTransform>();

        YellowRadius = YR * 775;
        GreenRadius = GR * 775;
        BlueRadius = BR * 775;

        BarSpeed = speed;

        YellowZone.sizeDelta = new Vector2(YellowRadius, 85f);
        GreenZone.sizeDelta = new Vector2(GreenRadius, 85f);
        BlueZone.sizeDelta = new Vector2(BlueRadius, 85f);

        Started = true;

        return VerifyResult;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Started) return;

        float x_ = Bar.transform.localPosition.x;
        x_ += BarSpeed * 100f * Time.deltaTime * Direction;

        x_ = Mathf.Clamp(x_, -775 / 2, 775 / 2);

        if (x_ >= 775 / 2 || x_ <= -(775 / 2))
        {
            Direction *= -1;
        }

        Bar.transform.localPosition = new Vector3(x_, Bar.transform.localPosition.y, Bar.transform.localPosition.z);

        if (ClientManager.Character.entityId != ClientManager.GameRoom.State.turnPriority)
        {
            VerifyResult.Reject(new System.Exception());
            DestroyImmediate(this.gameObject);
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ClientManager.GameRoom.Send("RemoveSkillsBlock", "");
            VerifyResult.Reject(new System.Exception());
            DestroyImmediate(this.gameObject);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Started = false;

            if (Bar.transform.localPosition.x <= BlueRadius / 2 && Bar.transform.localPosition.x >= -BlueRadius / 2)
            {
                VerifyResult.Resolve("B");
                DestroyImmediate(this.gameObject);
                return;
            }
            if (Bar.transform.localPosition.x <= GreenRadius / 2 && Bar.transform.localPosition.x >= -GreenRadius / 2)
            {
                VerifyResult.Resolve("G");
                DestroyImmediate(this.gameObject);
                return;
            }
            if (Bar.transform.localPosition.x <= YellowRadius / 2 && Bar.transform.localPosition.x >= -YellowRadius / 2)
            {
                VerifyResult.Resolve("Y");
                DestroyImmediate(this.gameObject);
                return;
            }

            VerifyResult.Resolve("R");
            DestroyImmediate(this.gameObject);
            return;
            
        }
    }
}
