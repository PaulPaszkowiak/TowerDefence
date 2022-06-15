using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;
    private string enemyTag = "Enemy";
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform transformFirePoint;
    private float startRepeating = 0f;
    private float updateRate = 1f;

    //atributes
    public float range = 100f;  
    public float fireRate = 1f;
    public int dmgAmount = 50;
    public int critChance = 5;
    public int critDamage = 200;
    public int buildCost = 10;
    public int upgradeCost = 10;

    void Start()
    {
        updateRate = updateRate / fireRate;
        InvokeRepeating("UpdateTarget", startRepeating, updateRate);
    }


    void UpdateTarget()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemyArray)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    
    void Update()
    {       
        //dont have a target
        if (target == null)
            return;

        //shoot at the target with a certin speed
        if(fireCountdown <= 0f)
        {           
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGameObject =(GameObject)Instantiate(bulletPrefab, transformFirePoint.position, transformFirePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        if(bullet != null)
        {
            bool isCriticalHit = UnityEngine.Random.Range(0, 100) < critChance;
            float critMulti = 1f;
            if (isCriticalHit)
            {
                critMulti = critDamage / 100;
            }
            else
            {
                critMulti = 1f;
            }
            bullet.SetDamage((int)(dmgAmount*critMulti),isCriticalHit);
            bullet.SetTarget(target);
        }
    }

    public void UpgradeRange()
    {
        if (Player.Gold >= upgradeCost)
        {
            range += 5;
            GameEvents.instance.PlayerGoldUpdate(-1 * upgradeCost);
            GameEvents.instance.PopUp(upgradeCost.ToString(), transform.position, Color.yellow, 0);
        }

    }

    public void UpgradeDamage()
    {
        if(Player.Gold >= upgradeCost)
        {
            dmgAmount += 5;
            GameEvents.instance.PlayerGoldUpdate(-1 * upgradeCost);
            GameEvents.instance.PopUp(upgradeCost.ToString(),transform.position,Color.yellow,0);
        }
       
    }

    public float GetRange()
    {
        return range;
    }

    public int GetDMG()
    {
        return dmgAmount;
    }
    private void OnMouseEnter()
    {
        SelectTower.Show_UI(gameObject);      
    }
      
}
