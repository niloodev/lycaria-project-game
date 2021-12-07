using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegisEntityScript : MonoBehaviour
{
    public string entityId;
    Character this_;

    Animator Animator_;
    void Start()
    {
        entityId = this.gameObject.name;
        this_ = ClientManager.GameRoom.State.entities[entityId];

        Animator_ = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animator_.SetBool("Ice", this_.skills[0]._booleans[0]);
    }
}
