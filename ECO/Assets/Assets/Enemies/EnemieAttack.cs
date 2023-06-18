using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieAttack : MonoBehaviour
{

    public static Action damagePlayer;
    [SerializeField] int damage = 0;

    public float attackSpeed;
    private float attackTimer;
    [SerializeField] Rigidbody rib;

    [SerializeField] NavMeshAgent enemie;
    [SerializeField] int hitForce;
    private bool atacando = false;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackSpeed;
        Target.attackRange += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        /*if (atacando)
        {
            enemie.isStopped = false;
            atacando = false;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CanAttack())
            {
                InRange();
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CanAttack())
            {

                InRange();

            }

        }
        
    }


    private void Attack()
    {

        //enemie.isStopped = true;
        Vector3 pulseForce = 100 * hitForce * transform.forward;
        rib.AddForce(pulseForce, ForceMode.Impulse);
        //atacando = true;

    }

    private void InRange()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().DamagePlayer(damage);
        Debug.Log("Attacando a Player");
        attackTimer = 0;
        //enemie.isStopped = false;

    }

    private bool CanAttack() => attackTimer >= attackSpeed;
}
