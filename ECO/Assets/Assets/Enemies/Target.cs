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
    [SerializeField] Transform wanderPoint1;
    [SerializeField] Transform wanderPoint2;
    private Transform actualWanderPoint;


    private bool wandering;

    [SerializeField] float lookingTime=1.5f;
    private float lookingTimer=0;

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
        actualWanderPoint = wanderPoint1;

        wandering = true;
       
    }
    private void Update()
    {
        if (wandering)
        {
            Wander();
        }
    }
    private void Wander()
    {

        var dist = Vector3.Distance(transform.position, actualWanderPoint.position);
        if (dist < 3)
        {
            lookingTimer += Time.deltaTime;
            enemie.isStopped = true;
            if (lookingTimer >= lookingTime)
            {

                if (actualWanderPoint == wanderPoint1)
                {
                    actualWanderPoint = wanderPoint2;
                    enemie.isStopped = false;

                }
                else
                {
                    actualWanderPoint = wanderPoint1;
                    enemie.isStopped = false;

                }
                lookingTimer = 0;
            }
        }

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
            actualWanderPoint = wanderPoint1;

        }
    }

    private void FixedUpdate()
    {
        if (wandering)
        {

            enemie.SetDestination(actualWanderPoint.position);

        }
        else if (!wandering)
        {
            enemie.SetDestination(target.position);

        }

    }


}
