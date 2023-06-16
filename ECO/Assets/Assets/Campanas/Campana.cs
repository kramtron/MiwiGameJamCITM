using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campana : MonoBehaviour
{
    public enum Note
    {
        DO,
        RE,
        MI
    }

    public GameObject hiddenObject;


    private bool hit;
    private Note note;

    private SphereCollider freezeCollider;
    private Light campana;

    private float hitTimer;
    [SerializeField] float activeTime;
    // Start is called before the first frame update
    void Start()
    {
        campana = GetComponentInChildren<Light>();
        freezeCollider = GetComponent<SphereCollider>();


        campana.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitTimer > activeTime)
        {
            hitTimer = 0;
            hit = false;
            campana.enabled = false;
        }

        switch (note)
        {
            case Note.DO:
                if (campana.enabled && hitTimer > activeTime)
                {
                    campana.enabled = false;
                }
                else if(hit)
                {
                    hitTimer += Time.deltaTime;
                    campana.enabled = true;
                }
                break;
            case Note.RE:
                if (hiddenObject.activeSelf && hitTimer > activeTime)
                {
                    hiddenObject.SetActive(false);
                }
                else if (hit)
                {
                    hitTimer += Time.deltaTime;
                    hiddenObject.SetActive(true);
                }
                break;
            case Note.MI:
                if (freezeCollider.gameObject.activeSelf && hitTimer > activeTime)
                {
                    freezeCollider.gameObject.SetActive(false);
                }
                else if (hit)
                {
                    hitTimer += Time.deltaTime;
                    freezeCollider.gameObject.SetActive(true);
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Do")
        {
            hit = true;
            note = Note.DO;
        }
        else if (collision.gameObject.tag == "Re")
        {
            hit = true;
            note = Note.RE;
        }
        else if (collision.gameObject.tag == "Mi")
        {
            hit = true;
            note = Note.MI;
        }
    }
}