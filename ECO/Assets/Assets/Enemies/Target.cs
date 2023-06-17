using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamagable
{


    public NavMeshAgent enemie;
    [SerializeField] Transform target;
    [SerializeField] float health = 100f;
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
    }

    
    private void FixedUpdate()
    {
        enemie.SetDestination(target.position);

    }


}
