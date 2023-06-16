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

        if(hit)
        {
          switch(note)
          {
            case Note.DO:
                    if (campana.enabled)
                    {
                        campana.enabled = false;
                    }
                    else
                    {
                        campana.enabled = true;
                    }
                    break;
            case Note.RE:
                    if(hiddenObject.activeSelf)
                    {
                        hiddenObject.SetActive(false);
                    }
                    else
                    {
                        hiddenObject.SetActive(true);
                    }
                break;
            case Note.MI:
                    if (freezeCollider.gameObject.activeSelf)
                    {
                        freezeCollider.gameObject.SetActive(false);
                    }
                    else
                    {
                        freezeCollider.gameObject.SetActive(true);
                    }
                break;
            }
            hitTimer += Time.deltaTime;
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