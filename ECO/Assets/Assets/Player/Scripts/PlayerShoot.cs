using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    public static Action changeDoInput;
    public static Action changeReInput;
    public static Action changeMiInput;

    [SerializeField] private KeyCode DoKey;
    [SerializeField] private KeyCode ReKey;
    [SerializeField] private KeyCode MiKey;
     
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
        if (Input.GetKey(DoKey)){

            changeDoInput?.Invoke();
        }
        if (Input.GetKey(ReKey)){

            changeReInput?.Invoke();
        }
        if (Input.GetKey(MiKey)){

            changeMiInput?.Invoke();
        }
        
    }
}
