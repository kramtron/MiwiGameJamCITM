using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamagable
{


    public NavMeshAgent enemie;
    [SerializeField] Transform target;
    [SerializeField] float health = 100f;

    [SerializeField] Transform temp;

    private bool wandering;
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wandering = true;
    }

    private void Wander()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wandering = false;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wandering = true;
        }
    }

    private void FixedUpdate()
    {
        if (wandering)
        {

            enemie.SetDestination(temp.position);

        }
        else if (!wandering)
        {
            enemie.SetDestination(target.position);

        }

    }


}
