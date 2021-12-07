using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CameraScript : MonoBehaviour
{
    float screenRatio, targetRatio, differenceInSize;
    public SpriteRenderer rink;
    public SpriteRenderer allRink;
    public float orthoBase;
    public float maxOrtho;
    Func<int> Cb;

    void defineCameraOrtho(){
        screenRatio = (float)Screen.width / (float)Screen.height;
        targetRatio = rink.bounds.size.x / rink.bounds.size.y; // 1920 / 1080, resolução base.

        if(screenRatio >= targetRatio){ // Ajusta por largura
            differenceInSize = targetRatio / screenRatio;
            orthoBase = rink.bounds.size.y / 2 * differenceInSize;

        } else { // Ajusta por altura
            orthoBase = rink.bounds.size.y / 2;
        }
    }

    void defineCameraMaxOrtho()
    {
        screenRatio = (float)Screen.width / (float)Screen.height;
        targetRatio = allRink.bounds.size.x / allRink.bounds.size.y; // 1920 / 1080, resolução base.

        if (screenRatio >= targetRatio)
        { // Ajusta por largura
            differenceInSize = targetRatio / screenRatio;
            maxOrtho = (allRink.bounds.size.y / 2 * differenceInSize)*0.95f;
        }
        else
        { // Ajusta por altura
            maxOrtho = (allRink.bounds.size.y / 2)*0.95f;
        }
    }

    void setUpCamera(){
        var deltaPos = 30f;
        var speedPos = 8;

        float halfWidth =  Camera.main.aspect * Camera.main.orthographicSize;

        float trueX = halfWidth;
        float trueY = Camera.main.orthographicSize;

        var x_ = Camera.main.transform.position.x;
        var y_ = Camera.main.transform.position.y;

        Camera.main.transform.position = new Vector3(x_, y_, Camera.main.transform.position.z);
        if (Input.mousePosition.x >= Screen.width - deltaPos){
            x_ += speedPos * Time.deltaTime;
        } else if (Input.mousePosition.x <= deltaPos){
            x_ += -speedPos * Time.deltaTime;
        }

        if(Input.mousePosition.y >= Screen.height - deltaPos){
            y_ += speedPos * Time.deltaTime;
        } else if (Input.mousePosition.y <= deltaPos){
            y_ += -speedPos * Time.deltaTime;
        }

        var _x = Mathf.Clamp(x_, -9.45f + trueX, 9.45f - trueX);
        var _y = Mathf.Clamp(y_, -5.25f + trueY, 5.25f - trueY);
        Camera.main.transform.position = new Vector3(_x, _y, Camera.main.transform.position.z);
    }

    public void goToCamera(float x, float y, float speed)
    {
        float halfWidth = Camera.main.aspect * Camera.main.orthographicSize;

        float trueX = x + (halfWidth * Mathf.Sign(x));
        float trueY = y + (Camera.main.orthographicSize * Mathf.Sign(y));

        var x_ = Mathf.Clamp(trueX, -9.45f, 9.45f) - (halfWidth * Mathf.Sign(x));
        var y_ = Mathf.Clamp(trueY, -5.25f, 5.25f) - (Camera.main.orthographicSize * Mathf.Sign(y));

        iTween.MoveTo(Camera.main.gameObject, new Vector3(x_, y_, Camera.main.transform.position.z), speed);
        
    }

    public void zoomMan(float some)
    {
        Camera.main.orthographicSize = some;
        if (some == maxOrtho) Cb();
    }

    public void zoomManB(float some)
    {
        Camera.main.orthographicSize = some;
        if (some == orthoBase) Cb();
    }

    public void zoomIn(Func<int> x)
    {
        float orthoActual = Camera.main.orthographicSize;
        Hashtable ht = iTween.Hash("from", orthoActual, "to", orthoBase, "time", 0.5f, "onupdate", "zoomManB");

        Cb = x;
        iTween.ValueTo(this.gameObject, ht);
    }

    public void zoomOut(Func<int> x)
    {
        float orthoActual = Camera.main.orthographicSize;
        Hashtable ht = iTween.Hash("from", orthoActual, "to", maxOrtho, "time", 0.5f, "onupdate", "zoomMan");

        Cb = x;
        iTween.ValueTo(this.gameObject, ht);
    }

    void Start()
    {
        defineCameraOrtho();
        defineCameraMaxOrtho();

        Camera.main.orthographicSize = maxOrtho;
    }

    void Update()
    {
        setUpCamera();
    }
}
