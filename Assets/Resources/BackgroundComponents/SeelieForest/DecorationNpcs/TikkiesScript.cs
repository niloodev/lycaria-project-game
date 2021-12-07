using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikkiesScript : MonoBehaviour
{
    Vector3[] Pathing;

    float createX;
    float createY;

    int RandomSign()
    {
        return Random.value < .5? 1 : -1;
    }

    void Start()
    {
        createX = this.transform.position.x;
        createY = this.transform.position.y;

        // Calcular os vetores para curva.
        int size = 7;
        Pathing = new Vector3[size + 1];

        float speed = 10 + Mathf.RoundToInt(Random.value * 5);

        for(var i = 0; i < size; i++)
        {
            float randomX = (createX + (0.25f * RandomSign())) + (Random.value * 2) * RandomSign(); 
            float randomY = (createY + (0.25f * RandomSign())) + (Random.value * 2) * RandomSign(); 

            Pathing[i] = new Vector3(randomX, randomY, this.transform.position.z);
            if (i == 0) Pathing[size] = new Vector3(createX, createY, this.transform.position.z);
        }

        iTween.MoveTo(
            this.transform.gameObject,
            iTween.Hash("path", Pathing, "time", speed, "loopType", "loop", "delay", 0, "easeType", iTween.EaseType.linear)
        );
    }

    void Update()
    {
     
    }
}
