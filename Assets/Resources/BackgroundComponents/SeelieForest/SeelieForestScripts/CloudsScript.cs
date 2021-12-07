using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScript : MonoBehaviour
{
    float endPos = -38.4f;
    float speed = 0.3f;

    void Start()
    {
    }

    void Update()
    {
        if (this.transform.position.x < endPos) this.transform.position = new Vector3(0, 0, this.transform.position.z);
        this.transform.position -= new Vector3(speed, 0f, 0f) * Time.deltaTime;
    }
}
