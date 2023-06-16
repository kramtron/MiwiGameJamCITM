using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

  
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
        
    }
}
