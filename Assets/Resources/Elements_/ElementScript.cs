using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementScript : MonoBehaviour
{
    public string elementId = "null";

    float step;
    Vector3 initialPos;
    Vector3 targetPos;
    private void Start()
    {
        initialPos = this.transform.position;
        targetPos = this.transform.position;
    }

    public void Start_()
    {
        var type = Type.GetType(ClientManager.GameRoom.State.elements[elementId].id + "ElementScript");
        var elementSpecificScript = this.gameObject.AddComponent(type);       
    }

    // Update is called once per frame
    public void Update()
    {
        if (elementId == "null") return;
    }
}
