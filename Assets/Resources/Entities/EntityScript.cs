using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityScript : MonoBehaviour
{
    public string entityId = "null";

    public Animator Animator_;
    public RuntimeAnimatorController SkinController;

    float step;
    Vector3 initialPos;
    Vector3 targetPos;

    class AnimListener
    {
        public string type;
        public string anim;
        public bool bool_;
        public float float_;
    }

    private void Start()
    {
        initialPos = this.transform.position;
        targetPos = this.transform.position;
    }

    public void Start_()
    {
        Animator_ = this.gameObject.GetComponent<Animator>();
        SkinController = Resources.Load("Entities/" + ClientManager.GameRoom.State.entities[entityId].id + "/Textures/" + ClientManager.GameRoom.State.entities[entityId].skin + "/AnimController_" + ClientManager.GameRoom.State.entities[entityId].id) as RuntimeAnimatorController;
        Animator_.runtimeAnimatorController = SkinController;

        var type = Type.GetType(ClientManager.GameRoom.State.entities[entityId].id + "EntityScript");
        var entitySpecificScript = this.gameObject.AddComponent(type);

        GameObject EntitiesCanvas = GameObject.Find("EntitiesCanvas");

        GameObject x = Instantiate(ClientManager.EntityBarPrefab, EntitiesCanvas.transform, true) as GameObject;
        x.name = entityId + "_StatusBar";
        EntityBarScript y = x.GetComponent<EntityBarScript>();
        y.Entity = this.gameObject;
        y.Start_();

        //iTween.MoveTo(this.gameObject, iTween.Hash("time", ClientManager.GameRoom.State.entities[entityId].vector.speed, "position", new Vector3(ClientManager.GameRoom.State.entities[entityId].goal.x, ClientManager.GameRoom.State.entities[entityId].goal.y, ClientManager.GameRoom.State.entities[entityId].goal.y * 2 + this.transform.parent.transform.position.z), "easetype", "linear"));
        ClientManager.GameRoom.State.entities[entityId].goal.OnChange += (changes) =>
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("time", ClientManager.GameRoom.State.entities[entityId].vector.speed, "position", new Vector3(ClientManager.GameRoom.State.entities[entityId].goal.x, ClientManager.GameRoom.State.entities[entityId].goal.y, ClientManager.GameRoom.State.entities[entityId].goal.y * 2 + this.transform.parent.transform.position.z), "easetype", "linear"));
        };

        // Iniciar o Listener das Animações
        ClientManager.GameRoom.OnMessage<AnimListener>(entityId + "_Animate", (serverInfo)=>{
            switch (serverInfo.type)
            {
                case "Trigger":
                    Animator_.SetTrigger(serverInfo.anim);
                    break;
                case "Bool":
                    Animator_.SetBool(serverInfo.anim, serverInfo.bool_);
                    break;
                case "Float":
                    Animator_.SetFloat(serverInfo.anim, serverInfo.float_);
                    break;
            }
        });
    }
    public void StartAnim()
    {
        Animator_.SetTrigger("Start");
    }

    // Update is called once per frame
    public void Update()
    {
        if (entityId == "null") return;

        Animator_.SetBool("Action", (ClientManager.GameRoom.State.turnPriority == entityId)?true:false);
    }
}
