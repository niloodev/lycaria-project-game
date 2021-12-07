using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eAegisIceAAProjectileElementScript : MonoBehaviour
{
    public string elementId;
    ParticleSystem particles;
    public bool destroy_ = false;

    // Start is called before the first frame update
    void Start()
    {
        elementId = this.transform.name;
        particles = this.transform.GetChild(1).GetComponent<ParticleSystem>();

        iTween.MoveTo(this.gameObject, iTween.Hash("time", ClientManager.GameRoom.State.elements[elementId].vector.speed, "position", new Vector3(ClientManager.GameRoom.State.elements[elementId].goal.x, ClientManager.GameRoom.State.elements[elementId].goal.y, ClientManager.GameRoom.State.elements[elementId].goal.y * 2 + this.transform.parent.transform.position.z), "easetype", "linear"));
        ClientManager.GameRoom.State.elements[elementId].goal.OnChange += (changes) =>
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("time", ClientManager.GameRoom.State.elements[elementId].vector.speed, "position", new Vector3(ClientManager.GameRoom.State.elements[elementId].goal.x, ClientManager.GameRoom.State.elements[elementId].goal.y, ClientManager.GameRoom.State.elements[elementId].goal.y * 2 + this.transform.parent.transform.position.z), "easetype", "linear"));
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy_ == true && particles.particleCount == 0) Destroy(this.gameObject);
        if (destroy_ == true) return;
        particles.startSpeed = 0.1f * Mathf.Sign(ClientManager.GameRoom.State.elements[elementId].vector.scaleX);
    }

    private void ComeDestroy()
    {
        destroy_ = true;
        particles.emissionRate = 0;
    }
}
