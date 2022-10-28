using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBCannonControl : MonoBehaviour
{
    public GameObject rocks;
    public Transform target;

    public float minFireTime = 2.0f, maxFireTime = 10.0f;

    private bool inSight;
    private float nextFire;
    private Vector3 directionToTarget;

    void Start()
    {
        
    }

    void Update()
    {
        checkForPlayer();
        ShootBullet();
    }

    private void checkForPlayer()
    {
        directionToTarget = target.position - transform.position;

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, directionToTarget.normalized, out hitInfo))
        {
            inSight = hitInfo.transform.CompareTag("PlayerShip");
        }
    }

    

    public void ShootBullet()
    {
        if(inSight)
        {
            LookAtTarget();
            Instantiate(rocks, transform.position, transform.rotation);
        }

        /*nextFire = Time.deltaTime + Random.Range(minFireTime, maxFireTime);
       
        Instantiate(rocks, transform.position, transform.rotation);*/
    }
    private void LookAtTarget()
    {
        Vector3 lookDirection = directionToTarget;
        lookDirection.y = 0.0f;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
    }



}
