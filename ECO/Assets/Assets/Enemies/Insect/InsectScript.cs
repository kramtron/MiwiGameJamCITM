using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InsectScript : MonoBehaviour, IDamagable
{
    

    public NavMeshAgent enemie;
    [SerializeField] Transform target;
    [SerializeField] float health = 100f;
    [SerializeField] float attackSpeed = 4;
    [SerializeField] float attackSpeedTimer = 0;

    [SerializeField] Transform temp;
    [SerializeField] Transform wanderPoint1;
    [SerializeField] Transform wanderPoint2;
    private Transform actualWanderPoint;
    [SerializeField] GameObject bulletPreFab;


    public bool wandering;

    [SerializeField] float lookingTime = 1.5f;
    private float lookingTimer = 0;

    private bool dead = false;
    private float deadTimer = 0;
    [SerializeField] Animator anim;

    [SerializeField] float bulletSpeed = 7;

    [SerializeField] bool freeze = false;
    [SerializeField] float freezeTime = 0;
    [SerializeField] float freezeTimer = 0;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
           
            var rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
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
        actualWanderPoint = wanderPoint1;

        wandering = true;
        attackSpeedTimer = attackSpeed;

    }

    
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
        attackSpeedTimer += Time.deltaTime;
        

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
    private bool CanAttack() => attackSpeedTimer >= attackSpeed;
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
                anim.SetBool("Attack", false);

                if (dist < 8 && CanAttack())
                {
                    anim.SetBool("Attack", true);
                    Vector3 direction = (target.position - transform.position).normalized;
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    GameObject bullet = Instantiate(bulletPreFab, transform.position, rotation);
                    Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
                    bulletRigidBody.velocity = direction * bulletSpeed;
                    attackSpeedTimer = 0;

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

                if (dist < 8 && CanAttack())
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    GameObject bullet = Instantiate(bulletPreFab, transform.position, rotation);
                    Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
                    bulletRigidBody.velocity = direction * bulletSpeed;
                    anim.SetBool("Attack", true);
                    attackSpeedTimer = 0;

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
                anim.SetBool("Move", true);

            }
            else if (!wandering)
            {
                enemie.isStopped = false;

                enemie.SetDestination(target.position);
                //enemie.speed = 5f;
                anim.SetBool("Move", true);

            }
        }
        else
        {
            enemie.SetDestination(transform.position);
            anim.SetBool("Move", false);

        }


    }
}
