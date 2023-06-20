using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBullet : MonoBehaviour
{
    public float lifeTime = 3;
    [SerializeField] int damage = 10;

    private void Start()
    {

    }
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {


            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().DamagePlayer(damage);
            Debug.Log("Attacando a Player");

        }
        if (collision.gameObject.tag != ("Enemie"))
        {
            Destroy(gameObject);

        }
    }
}
