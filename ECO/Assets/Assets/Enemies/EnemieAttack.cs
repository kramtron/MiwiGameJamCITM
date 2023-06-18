using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieAttack : MonoBehaviour
{

    public static Action damagePlayer;
    [SerializeField] int damage = 0;

    public float attackSpeed;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CanAttack())
            {
                other.gameObject.GetComponent<PlayerStats>().DamagePlayer(damage);
                Debug.Log("Attacando a Player");
                attackTimer = 0;
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CanAttack())
            {
                other.gameObject.GetComponent<PlayerStats>().DamagePlayer(damage);
                Debug.Log("Attacando a Player");
                attackTimer = 0;

            }

        }
    }

    private bool CanAttack() => attackTimer >= attackSpeed;
}
