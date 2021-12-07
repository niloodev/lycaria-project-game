using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IceSpikeScript : MonoBehaviour
{
    Animator a;
    void Start()
    {
        a = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ended()
    {
        Destroy(this.gameObject);
    }
}
