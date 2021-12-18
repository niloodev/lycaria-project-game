using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IceAutoDestruct : MonoBehaviour
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
        if (s.particleCount == 0) Destroy(this.gameObject);
    }
}
