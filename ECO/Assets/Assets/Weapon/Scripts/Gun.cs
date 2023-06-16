using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    enum BulletType
    {
        Do,
        Re,
        Mi
    }


    [SerializeField]  private GunData gunData;
    [SerializeField]  Transform muzzle;
    [SerializeField]  GameObject DoPrefab;
    [SerializeField]  GameObject RePrefab;
    [SerializeField]  GameObject MiPrefab;

    private float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        
    }

    private bool CanShoot() =>  timeSinceLastShot > 1f / (gunData.fireRate / 60);
    public void Shoot()
    {
        if (CanShoot()) 
        {
            var bullet = Instantiate(DoPrefab, muzzle.position, muzzle.rotation);
            bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * gunData.bulletSpeed;
           /* Debug.Log("Shoot");
            if(Physics.Raycast(muzzle.transform.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                Debug.Log("Si entra");
                IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                damagable?.Damage(gunData.damage);
                
            }*/
            
            timeSinceLastShot = 0;
            Shooting();

        }
    }

    
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }
    private void Shooting()
    {
        
    }

    
   

   
}
