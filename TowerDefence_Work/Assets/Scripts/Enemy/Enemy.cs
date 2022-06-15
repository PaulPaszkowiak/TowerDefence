using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    //Enemy Stats
    public int startHealth = 100;
    public float speed = 10f;   
    public bool isFlying = false;
    public int goldAmount = 1;
    public int lifeAmount = -1;
    private int health;

    //Moving towards waypoints
    private Transform target;
    private int waypointIndex = 0;
    private float distOffset = 1f;
    private float rotationSpeed = 5f;
    private float speedTemp;   
    private float stunDuration = 0f;

    //UI
    public Image healthBar;
    public Image healthBarBackGround;   
    public GameObject dmgGameObject;
    private GameObject dmgUI;

    private void Start()
    {
        //init
        speedTemp = speed;
        health = startHealth;
        dmgUI = dmgGameObject;

        //If waypoints exist
        if (Waypoints.waypoints.Length > 0)
        {
            //Get the first waypoint
            target = Waypoints.waypoints[0];
        }
        else
        {
            //Waypoints not found, destroy your self
            Destroy(gameObject);           
        }
      
    }

    private void Update()
    {
        // if we are stunned dont move
        if(stunDuration > 0)
        {
            stunDuration -= Time.deltaTime;
            return;
        }
        //Move towards the waypoints
        MoveToWaypoint(isFlying);
    }

    public void SetFlying(bool _isFlying)
    {
        isFlying = _isFlying;
    }

    private void MoveToWaypoint(bool isFlying)
    {
        //If the enemy can fly dont folow the path
        if (this.isFlying)
        {
            //setting the last waypoint to our target
            int lastIndex = Waypoints.waypoints.Length - 1;
            target = Waypoints.waypoints[lastIndex];
        }

        //calculate movement direction
        Vector3 dir = target.position - transform.position;
        //move to the target/waypoint with a constant speed
        transform.Translate(dir.normalized * speedTemp * Time.deltaTime, Space.World);
        //rotate
        EnemyRotate(dir);
        //if we reach the target/waypoint get the next one
        if (Vector3.Distance(transform.position, target.position) <= distOffset)
        {
            GetNextWaypoint();
        }
        //Update healthbarrotation
        HealthbarRotation();
    }

    private void GetNextWaypoint()
    {
        //if we reached our endpoint
        if(waypointIndex >= Waypoints.waypoints.Length - 1)
        {         
            DestroyEnemy();
            return;
        }

        //Set the next waypoint
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    private void DestroyEnemy()
    {
        //Player.Lives--;
        GameEvents.instance.PlayerLiveUpdate(lifeAmount);
        WaveSpawner.EnemiesAlive--;        
        Destroy(gameObject);
    }
  
    private void KillEnemy()
    {       
        WaveSpawner.EnemiesAlive--;    
        GameEvents.instance.PlayerGoldUpdate(goldAmount);
        Destroy(gameObject);       
    }

    private void EnemyRotate(Vector3 dir)
    {
        
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //smothing out
        Vector3 rotation = Quaternion.Lerp(transform.rotation,lookRotation,Time.deltaTime* rotationSpeed).eulerAngles;
        //we only want to rotate on the y-axis
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    public void TakeDamage(int dmgAmount,bool isCriticalHit)
    {
        //reduce our health
        health -= dmgAmount;
        //Change Healthui
        healthBar.fillAmount = (float)health / (float)startHealth;
        //Display the Damage
         
        if(dmgUI != null)
        {
            DamagePoPUp popUp = Instantiate(dmgUI, transform.position, Quaternion.identity).GetComponent<DamagePoPUp>();
            popUp.SetDamageText(dmgAmount,isCriticalHit);            
        }
        else
        {
            Debug.Log("error");
        }
        
        //check if we are dead
        if (health <= 0)
        {
            KillEnemy();           
        }
                
    }

    public void GetStunned()
    {
        stunDuration = 0.1f;
    }

    public void IncreaseEnemyStregth(int strMult)
    {        
        speed = speed + speed*strMult*0.05f;
        startHealth = startHealth * strMult;
        goldAmount = goldAmount * strMult;
    }


    private void HealthbarRotation()
    {
        healthBar.transform.LookAt(2*transform.position - Camera.main.transform.position);
        healthBarBackGround.transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
