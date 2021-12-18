using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eAegisIceAAProjectileElementScript : MonoBehaviour
{
    public string elementId;
    TrailRenderer trail;
    public bool destroy_ = false;

    // Start is called before the first frame update
    void Start()
    {
        elementId = this.transform.name;
        trail = this.transform.GetChild(1).GetComponent<TrailRenderer>();

        iTween.MoveTo(this.gameObject, iTween.Hash("time", ClientManager.GameRoom.State.elements[elementId].vector.speed, "position", new Vector3(ClientManager.GameRoom.State.elements[elementId].goal.x, ClientManager.GameRoom.State.elements[elementId].goal.y, ClientManager.GameRoom.State.elements[elementId].goal.y * 2 + this.transform.parent.transform.position.z), "easetype", "linear"));
        ClientManager.GameRoom.State.elements[elementId].goal.OnChange += (changes) =>
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("time", ClientManager.GameRoom.State.elements[elementId].vector.speed, "position", new Vector3(ClientManager.GameRoom.State.elements[elementId].goal.x, ClientManager.GameRoom.State.elements[elementId].goal.y, ClientManager.GameRoom.State.elements[elementId].goal.y * 2 + this.transform.parent.transform.position.z), "easetype", "linear"));
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (ClientManager.GameRoom.State.elements[elementId] == null) destroy_ = true;
             
        if (destroy_ == true)
        {
            this.GetComponent<Renderer>().enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(this.gameObject, 4);
            destroy_ = false;
        }
    }

    private void ComeDestroy()
    {
        destroy_ = true;
    }
}
