using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{

    public Camera cam;
    //private Vector3 mousePos;


   
    float lookAngle;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;


    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 100;

        mousePos -= new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        mousePos = cam.ScreenToViewportPoint(mousePos);

        

        //Debug.Log(mousePos);

        lookAngle = MathF.Atan2(mousePos.y, mousePos.x) * (360f / (MathF.PI * 2f));
        lookAngle = (lookAngle + 360f) % 360f;
        transform.rotation = Quaternion.Euler(-lookAngle, 90, 0);

        

    }
}
