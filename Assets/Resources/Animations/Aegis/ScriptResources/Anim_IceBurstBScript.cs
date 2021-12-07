using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IceBurstBScript : MonoBehaviour
{
    ParticleSystem s;
    void Start()
    {
        s = this.GetComponent<ParticleSystem>();

        s.loop = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (s.isPlaying == false) Destroy(this.gameObject);
    }
}
