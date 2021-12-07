using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RSG;

public class SelectTargetScript : MonoBehaviour
{
    Text TextA;
    Text TextAShadow;
    Promise<string[]> TargetArray;
    string[] EntitiesArray = new string[66];
    string[] TeamsSupported;
    int T = 1000;

    RaycastHit2D beforeHit;
    RaycastHit2D[] beforeHit_;

    SpriteRenderer RadiusArea;
    float RadiusRadius;

    SpriteRenderer BoxArea;
    float BoxWidth;
    float BoxHeight;

    float MaxOrder;

    public IPromise<string[]> Initialize(int TargetType, string[] TeamsSup, float Radius = 1.5f, float BoxWidth_ = 6f, float BoxHeight_ = 2f, float MaxOrder_ = 2)
    {
        TargetArray = new Promise<string[]>();

        GameObject TopTextA = GameObject.Find("TopText A");

        TextA = TopTextA.transform.GetChild(1).GetComponent<Text>();
        TextAShadow = TopTextA.transform.GetChild(0).GetComponent<Text>();

        bool haveNeutral = false;
        bool haveEnemy = false;
        bool haveAllie = false;

        foreach(string team in TeamsSup)
        {
            if (team == "N") haveNeutral = true;
            else if (team == ClientManager.Character.team) haveAllie = true;
            else if (team != ClientManager.Character.team) haveEnemy = true;
        }

        if(haveEnemy && haveAllie)
        {
            TextA.color = new Color32(255, 255, 255, 255);
            TextA.text = "SELECIONE O(S) ALVO(S)";
            TextAShadow.text = "SELECIONE O(S) ALVO(S)";
        } else if (haveEnemy)
        {
            TextA.color = new Color32(255, 194, 194, 255);
            TextA.text = "SELECIONE O(S) ALVO(S) INIMIGO(S)";
            TextAShadow.text = "SELECIONE O(S) ALVO(S) INIMIGO(S)";
        } else if (haveAllie)
        {
            TextA.color = new Color32(170, 253, 158, 255);
            TextA.text = "SELECIONE O(S) ALVO(S) ALIADO(S)";
            TextAShadow.text = "SELECIONE O(S) ALVO(S) ALIADO(S)";
        } else if (haveNeutral)
        {
            TextA.color = new Color32(255, 255, 255, 255);
            TextA.text = "SELECIONE O(S) ALVO(S) NEUTRO(S)";
            TextAShadow.text = "SELECIONE O(S) ALVO(S) NEUTRO(S)";
        }

        TeamsSupported = TeamsSup;
        switch (TargetType)
        {
            case 0:
                // Start 
                T = TargetType;
                break;
            case 1:
                // Start
                RadiusArea = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
                RadiusRadius = Radius;
                T = TargetType;
                break;
            case 2:
                MaxOrder = MaxOrder_;
                T = TargetType;
                break;
            case 3:
                BoxArea = this.transform.GetChild(1).GetComponent<SpriteRenderer>();
                BoxWidth = BoxWidth_;
                BoxHeight = BoxHeight_;
                T = TargetType;
                break;
            
        }

        return TargetArray;
    }

    // id: 0
    void OneTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector3.zero, 0f);

        if (hit.collider != null)
        {
            var isPassable = false;

            foreach(string team in TeamsSupported)
            {
                if (ClientManager.GameRoom.State.entities[hit.collider.transform.name].team == team) isPassable = true;
            }

            if (isPassable)
            {
                hit.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(1, 1, 1, 1));
            }
        }

        if (beforeHit)
        {
            if (beforeHit.collider != hit.collider && beforeHit.collider != null)
            {
                var isPassable = false;

                foreach (string team in TeamsSupported)
                {
                    if (ClientManager.GameRoom.State.entities[beforeHit.collider.transform.name].team == team) isPassable = true;
                }

                if (isPassable)
                {
                    beforeHit.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
                }
            }
        }
        
        beforeHit = hit;

        if(Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            EntitiesArray[0] = hit.collider.transform.name;
            EntitiesArray[64] = Camera.main.ScreenToWorldPoint(Input.mousePosition).x.ToString().Replace(",",".");
            EntitiesArray[65] = Camera.main.ScreenToWorldPoint(Input.mousePosition).y.ToString().Replace(",",".");

            TargetArray.Resolve(EntitiesArray);
            Destroy(this.gameObject);
        }
    }

    // id: 1
    void Radius()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), RadiusRadius, Vector2.zero, Mathf.Infinity);

        RadiusArea.transform.localScale = new Vector3(RadiusRadius*2, RadiusRadius*2);
        RadiusArea.color = new Color(1, 1, 1, 0.4f);
        RadiusArea.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -75);
        Cursor.visible = false;

        foreach (RaycastHit2D h in hit)
        {
            var isPassable = false;

            foreach (string team in TeamsSupported)
            {
                if (ClientManager.GameRoom.State.entities[h.collider.transform.name].team == team) isPassable = true;
            }

            if (isPassable)
            {
                h.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(1, 1, 1, 1));
            }
        }

        if(beforeHit_ != null)
        {
            foreach (RaycastHit2D h in beforeHit_)
            {
                bool exists_ = false;

                foreach (RaycastHit2D t in hit)
                    if (h.collider == t.collider) exists_ = true;

                if (!exists_)
                {
                    var isPassable = false;

                    foreach (string team in TeamsSupported)
                    {
                        if (ClientManager.GameRoom.State.entities[h.collider.transform.name].team == team) isPassable = true;
                    }

                    if (isPassable)
                    {
                        h.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
                    }
                }

            }
        }

        beforeHit_ = hit;

        if (Input.GetMouseButtonDown(0) && hit.Length != 0)
        {
            int i = 0;
            foreach(RaycastHit2D h in hit)
            {
                EntitiesArray[i] = h.collider.transform.name;
                i++;
            }
            EntitiesArray[64] = Camera.main.ScreenToWorldPoint(Input.mousePosition).x.ToString().Replace(",", "."); 
            EntitiesArray[65] = Camera.main.ScreenToWorldPoint(Input.mousePosition).y.ToString().Replace(",", ".");

            TargetArray.Resolve(EntitiesArray);
            Destroy(this.gameObject);
        }
    }

    // id: 2
    void OrderTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector3.zero, 0f);

        if (hit.collider != null)
        {
            var isPassable = false;

            foreach (string team in TeamsSupported)
            {
                if (ClientManager.GameRoom.State.entities[hit.collider.transform.name].team == team) isPassable = true;
            }

            foreach (string id_ in EntitiesArray)
            {
                if (hit.collider.transform.name == id_) isPassable = false;
            }

            if (isPassable)
            {
                hit.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(1, 1, 1, 1));
            }
        }

        if (beforeHit)
        {
            if (beforeHit.collider != hit.collider && beforeHit.collider != null)
            {
                var isPassable = false;

                foreach (string team in TeamsSupported)
                {
                    if (ClientManager.GameRoom.State.entities[beforeHit.collider.transform.name].team == team) isPassable = true;
                }

                foreach (string id_ in EntitiesArray)
                {
                    if (beforeHit.collider.transform.name == id_) isPassable = false;
                }

                if (isPassable)
                {
                    beforeHit.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
                }
            }
        }

        beforeHit = hit;

        if (Input.GetMouseButton(0))
        {
            if (hit.collider != null)
            {
                var isPassable = false;

                foreach (string team in TeamsSupported)
                {
                    if (ClientManager.GameRoom.State.entities[hit.collider.transform.name].team == team) isPassable = true;
                }

                foreach (string id_ in EntitiesArray)
                {
                    if (hit.collider.transform.name == id_) isPassable = false;
                }

                if (isPassable)
                {
                    hit.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 3, 0, 3));
                    int i = 0;
                    foreach (string id in EntitiesArray)
                    {
                        if (EntitiesArray[i] == null && i < MaxOrder)
                        {
                            EntitiesArray[i] = hit.collider.transform.name;
                            break;
                        }
                        i++;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(EntitiesArray.Count(s => s != null) != 0)
            {
                int i = 0;
                foreach (string id in EntitiesArray)
                {
                    if (EntitiesArray[i] == null) break;
                    else
                    {
                        GameObject x = GameObject.Find(id);
                        x.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
                    }
                    i++;
                }
                TargetArray.Resolve(EntitiesArray);
                Destroy(this.gameObject);
            }
        }

    }

    // id: 3
    void CustomRadius()
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(BoxWidth, BoxHeight), 0f, Vector2.zero, Mathf.Infinity);

        BoxArea.transform.localScale = new Vector3(BoxWidth, BoxHeight);
        BoxArea.color = new Color(1, 1, 1, 0.4f);
        BoxArea.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -75);
        Cursor.visible = false;

        foreach (RaycastHit2D h in hit)
        {
            var isPassable = false;

            foreach (string team in TeamsSupported)
            {
                if (ClientManager.GameRoom.State.entities[h.collider.transform.name].team == team) isPassable = true;
            }

            if (isPassable)
            {
                h.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(1, 1, 1, 1));
            }
        }

        if (beforeHit_ != null)
        {
            foreach (RaycastHit2D h in beforeHit_)
            {
                bool exists_ = false;

                foreach (RaycastHit2D t in hit)
                    if (h.collider == t.collider) exists_ = true;

                if (!exists_)
                {
                    var isPassable = false;

                    foreach (string team in TeamsSupported)
                    {
                        if (ClientManager.GameRoom.State.entities[h.collider.transform.name].team == team) isPassable = true;
                    }

                    if (isPassable)
                    {
                        h.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
                    }
                }

            }
        }

        beforeHit_ = hit;

        if (Input.GetMouseButtonDown(0) && hit.Length != 0)
        {
            int i = 0;
            foreach (RaycastHit2D h in hit)
            {
                EntitiesArray[i] = h.collider.transform.name;
                i++;
            }
            EntitiesArray[64] = Camera.main.ScreenToWorldPoint(Input.mousePosition).x.ToString().Replace(",", ".");
            EntitiesArray[65] = Camera.main.ScreenToWorldPoint(Input.mousePosition).y.ToString().Replace(",", ".");

            TargetArray.Resolve(EntitiesArray);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) || ClientManager.GameRoom.State.turnPriority != ClientManager.Character.entityId)
        {
            if(TargetArray != null) TargetArray.Reject(new System.Exception());
            Destroy(this.gameObject);
            return;
        }

        switch (T)
        {
            case 0:
                OneTarget();
                break;
            case 1:
                Radius();
                break;
            case 2:
                OrderTarget();
                break;
            case 3:
                CustomRadius();
                break;

        }
    }

    private void OnDestroy()
    {  
        ClientManager.SelectTargetScreen = null;
        TextA.text = "";
        TextAShadow.text = "";

        if (beforeHit)
        {
            if(beforeHit.collider != null)
            {
                beforeHit.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
            }
        }

        if (beforeHit_ != null)
        {
            foreach (RaycastHit2D h in beforeHit_)
            {
                h.collider.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));            
            }
        }

        if (EntitiesArray.Count(s => s != null) != 0 && T == 2)
        {
            int i = 0;
            foreach (string id in EntitiesArray)
            {
                if (EntitiesArray[i] == null) break;
                else
                {
                    GameObject x = GameObject.Find(id);
                    x.transform.GetComponent<SpriteRenderer>().material.SetColor(Shader.PropertyToID("_White"), new Color(0, 0, 0, 1));
                }
                i++;
            }
            Destroy(this.gameObject);
        }

        if (Cursor.visible == false) Cursor.visible = true;
    }
}
