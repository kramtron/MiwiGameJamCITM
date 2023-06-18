using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        //EnemieAttack.damagePlayer += DamagePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //Muere
            Destroy(gameObject);
        }
    }
}
