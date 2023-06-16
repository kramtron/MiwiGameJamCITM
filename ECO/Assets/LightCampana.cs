using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCampana : MonoBehaviour
{
    [SerializeField] private bool isOn;
    Light campana;
    // Start is called before the first frame update
    void Start()
    {
        campana = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            campana.enabled = true;
        }
        else
        {
            campana.enabled = false;
        }
    }
}