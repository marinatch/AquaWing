using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public GameObject theBullet;
    public bool playerControlled = false;
   
    void Update()
    {
        if(playerControlled)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                ShootBullet();
            }
        }
    }

    void ShootBullet()
    {
        Instantiate(theBullet, transform.position, transform.rotation);
    }
}
