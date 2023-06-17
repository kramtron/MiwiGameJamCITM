using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public enum BulletType
    {
        Do,
        Re,
        Mi
    }

    public BulletType bulletType;

    [SerializeField]  private GunData gunData;
    [SerializeField]  Transform muzzle;
    [SerializeField]  GameObject DoPrefab;
    [SerializeField]  GameObject RePrefab;
    [SerializeField]  GameObject MiPrefab;

    public GameObject gOHited;

    public float pulseHitForce;
    private float timeSinceLastShot;
    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.changeDoInput += DoChanger;
        PlayerShoot.changeReInput += ReChanger;
        PlayerShoot.changeMiInput += MiChanger;
        Bullet.enemieHited += EnemieHited;
        bulletType = BulletType.Do;
        
    }

    private void EnemieHited()
    {
        if (bulletType == Gun.BulletType.Do)
        {
            Debug.Log("Enemigo golpeado  con Do");
            Debug.Log(gOHited.name);

            IDamagable damagable = gOHited.transform.GetComponent<IDamagable>();
            damagable?.Damage(gunData.damage);

        }
        if (bulletType == Gun.BulletType.Re)
        {
            Debug.Log("Enemigo golpeado  con Re");
            Debug.Log(gOHited.name);


        }
        if (bulletType == Gun.BulletType.Mi)
        {
            Debug.Log("Enemigo golpeado  con Mi");
            Debug.Log(gOHited.name);

            var pulseForce = gOHited.transform.right * pulseHitForce * 100;
            Rigidbody rb = gOHited.GetComponent<Rigidbody>();
            rb.AddForce(pulseForce, ForceMode.Impulse);
            //rb.for
        }
    }

    private bool CanShoot() =>  timeSinceLastShot > 1f / (gunData.fireRate / 60);
    public void Shoot()
    {
        if (CanShoot()) 
        {
            switch (bulletType)
            {
                case BulletType.Do:
                    var bulletDo = Instantiate(DoPrefab, muzzle.position, muzzle.rotation);
                    bulletDo.GetComponent<Rigidbody>().velocity = muzzle.forward * gunData.bulletSpeed;
                    break;

                case BulletType.Re:
                    var bulletRe = Instantiate(RePrefab, muzzle.position, muzzle.rotation);
                    bulletRe.GetComponent<Rigidbody>().velocity = muzzle.forward * gunData.bulletSpeed;
                    break;

                case BulletType.Mi:
                    var bulletMi = Instantiate(MiPrefab, muzzle.position, muzzle.rotation);
                    bulletMi.GetComponent<Rigidbody>().velocity = muzzle.forward * gunData.bulletSpeed;
                    break;
            }
            
          
            
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

    public void DoChanger()
    {
        bulletType = BulletType.Do;

    }
    public void ReChanger()
    {
        bulletType = BulletType.Re;

    } 
    public void MiChanger()
    {
        bulletType = BulletType.Mi;

    }





}
