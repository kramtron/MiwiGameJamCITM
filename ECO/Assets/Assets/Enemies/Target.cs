using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamagable
{
    public static Action attackRange;

    public NavMeshAgent enemie;
    private Transform target;
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

    [SerializeField] bool freeze = false;
    [SerializeField] float freezeTime = 0;
    [SerializeField] float freezeTimer = 0;

    private GameObject player;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            dead = true;
        }
    }

    public void Freeze()
    {

        freeze = true;
        anim.enabled = false;

    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
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
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }

        }

        if (freeze)
        {
            freezeTimer += Time.deltaTime;
            enemie.SetDestination(transform.position);
            if (freezeTimer >= freezeTime)
            {
                freeze = false;
                freezeTimer = 0;
                anim.enabled = true;

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

                if (dist < 6)
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
        if (!dead || !freeze)
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
