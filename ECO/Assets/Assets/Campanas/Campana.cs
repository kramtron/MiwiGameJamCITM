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
    private GameObject player;
    private Rigidbody playerRB;

    [SerializeField] Animator lightAnim;
    [SerializeField] Animator bellAnim;

    private bool hit;
    private Note note;

    private Light campana;

    [Header("Attraction")]
    [SerializeField] private float attractionForce = 45f;
    private float multiplier = 100f;

    [Header("Timer")]
    private float hitTimer;
    [SerializeField] private float activeTime = 2f;

    [SerializeField] AudioSource DoHitSound;
    [SerializeField] AudioSource ReHitSound;
    [SerializeField] AudioSource MiHitSound;

    Vector3 bellPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        campana = GetComponentInChildren<Light>();
        playerRB = player.GetComponent<Rigidbody>();

        Transform temp;
        temp = gameObject.transform;
        while(temp.GetComponentInParent<Transform>() != null)
        {
            temp = transform.parent;
            bellPos += temp.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hitTimer > activeTime)
        {
            hitTimer = 0;
            hit = false;
            lightAnim.SetBool("Hit", false);

        }

        if (hit == false)
        {
            bellAnim.SetBool("Hit", false);

        }
        switch (note)
        {
            case Note.DO:
                if (hitTimer > activeTime)
                {
                    lightAnim.SetBool("Hit", false);

                }
                else if(hit)
                {
                    hitTimer = 0;

                    hitTimer += Time.deltaTime;
                    lightAnim.SetBool("Hit", true);
                    hit = false;
                    DoHitSound.Play();
                    bellAnim.SetBool("Hit", true);


                }
                break;
            case Note.RE:
                if (hiddenObject.activeSelf && hitTimer > activeTime)
                {
                    hiddenObject.SetActive(true);
                    hit = false;

                }
                else if (hit )
                {
                    hitTimer = 0;

                    hitTimer += Time.deltaTime;
                    hiddenObject.SetActive(false);
                    ReHitSound.Play();
                    hit = false;
                    bellAnim.SetBool("Hit", true);

                }
                break;
            case Note.MI:
                if (hit)
                {
                    playerRB.AddForce((bellPos - player.transform.position).normalized * attractionForce * multiplier, ForceMode.Impulse);
                    hit = false;
                    MiHitSound.Play();
                    bellAnim.SetBool("Hit", true);


                }
                break;

        }
        hitTimer += Time.deltaTime;
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Do")
        {
            Debug.Log("Do hit");
            hit = true;
            note = Note.DO;
        }
        else if (collision.gameObject.tag == "Re")
        {
            Debug.Log("Re hit");
            hit = true;
            note = Note.RE;
        }
        else if (collision.gameObject.tag == "Mi")
        {
            Debug.Log("Mi hit");
            hit = true;
            note = Note.MI;
        }
    }
}