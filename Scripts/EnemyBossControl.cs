using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossControl : MonoBehaviour
{
    public float attackTime = 10.0f;
    public bool isAttacking;
    public GameObject[] rocks;
    public string rockTag = "PlayerShip";
    //public EnemyBCannonControl bossCannon;
    public float minFireTime = 2.0f, maxFireTime = 10.0f;

    int nextFire;

    private float attackCounter;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackCounter += Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        attackCounter += Time.deltaTime;
        if(attackCounter >= attackTime)
        {
            isAttacking = true;
            //animación
            animator.SetBool("Attack", true);

            //tirar rocas
            ShootBullet();

            //reset attackCounter
            attackCounter = 0.0f;
        }
        else
        {
            isAttacking = false;
            animator.SetBool("Attack", false);
        }
    }

    public void ShootBullet()
    {
        //nextFire = Time.deltaTime + Random.Range(minFireTime, maxFireTime);
        nextFire = Random.Range(0, rocks.Length);
        Instantiate(rocks[nextFire], transform.position, transform.rotation);
    }
}
