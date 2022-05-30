using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 120f;
    public bool isAOE = false;
    public bool isStun = false;

    private float hitRange = 1f;
    private int damage = 50;
    private float aoeRange = 10f;
    private float aoeDMGmultiplier = 0.5f;

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Get the direction towards the target/enemy
        Vector3 dir = target.position - transform.position;
        //normalizing the directionvector
        Vector3 dirNorm = dir.normalized;
        //move in the direction
        transform.position += dirNorm * speed * Time.deltaTime;
        //rotate
        RotateBullet(dir);
        //Check if we are in hitrange
        if (Vector3.Distance(transform.position, target.position) < hitRange)
        {
            HitTarget(target);
        }

   
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(int dmgAmount)
    {
        damage = dmgAmount;
    }
    private void RotateBullet(Vector3 dir)
    {
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = lookRotation;

    }

    private void HitTarget(Transform target)
    {
        //if our bullet can damage the surrounding enemy
        if (isAOE)
        {
            //Get all coliders in range
            Collider[] colArray = Physics.OverlapSphere(transform.position, aoeRange);
            foreach (Collider collider in colArray)
            {
                //Get only our Enemys and damage them
                if (collider.tag == "Enemy")
                    Damage(collider.transform,(int)(damage*aoeDMGmultiplier));
            }
        }
        //damaging the enemy target
        Damage(target,damage);
        //destroy it self on hit
        Destroy(gameObject);
    }

    private void Damage (Transform _enemy,int damage)
    {
        Enemy enemy = _enemy.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            if (isStun)
                enemy.GetStunned();
        }
               
    }
}
