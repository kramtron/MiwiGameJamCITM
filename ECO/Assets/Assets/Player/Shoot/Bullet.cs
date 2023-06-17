using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public static Action enemieHited;

    public float lifeTime = 3;

    
    private void Start()
    {
        
    }
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Enemie")
        {
            Gun gunHit = GameObject.Find("Weapon").GetComponent<Gun>();
            gunHit.gOHited = collision.gameObject;

            enemieHited?.Invoke();
            
        }
        
        Destroy(gameObject);
    }
}
