using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamagable
{
    public static Action attackRange;

    public NavMeshAgent enemie;
    [SerializeField] Transform target;
    [SerializeField] float health = 100f;

    [SerializeField] Transform temp;
    [SerializeField] Transform wanderPoint1;
    [SerializeField] Transform wanderPoint2;
    private Transform actualWanderPoint;


    public bool wandering;

    [SerializeField] float lookingTime=1.5f;
    private float lookingTimer=0;

    private bool dead = false;
    private float deadTimer = 0;
    [SerializeField] Animator anim;
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            dead = true;
        }
    }

    private void Start()
    {
        actualWanderPoint = wanderPoint1;

        wandering = true;
       
    }

    [Obsolete]
    private void Update()
    {
        if (wandering)
        {
            Wander();
        }
        if (dead)
        {
            deadTimer += Time.deltaTime;
            anim.SetBool("Dead", true);
            enemie.isStopped = true;
            if (deadTimer >= 5)
            {
                Destroy(gameObject);

            }

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
            if (!dead)
            {
                wandering = false;
                var dist = Vector3.Distance(transform.position, target.position);

                if (dist < 6)
                {
                    attackRange?.Invoke();
                }
            }
        }

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!dead)
            {
                wandering = false;
                var dist = Vector3.Distance(transform.position, target.position);
                anim.SetBool("Attack", false);

                if (dist < 7)
                {
                    attackRange?.Invoke();
                }
            }
            
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
        if (!dead)
        {
            if (wandering)
            {
                //enemie.speed = 3.5f;
                enemie.SetDestination(actualWanderPoint.position);

            }
            else if (!wandering)
            {
                enemie.isStopped = false;

                enemie.SetDestination(target.position);
                //enemie.speed = 5f;

            }
        }
        else
        {
            enemie.SetDestination(transform.position);
        }
        

    }


}
