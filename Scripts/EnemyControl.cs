using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int enemyEnergy = 10;
    public string bulletTag = "PlayerBullet";
    public int myPoints = 50;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == bulletTag)
        {
            enemyEnergy -= other.gameObject.GetComponent<PlayerBullet>().bulletPower;
            if (enemyEnergy <= 0)
            {
                GameObject.Find("SceneControl").GetComponent<SceneControl>().IncreaseScore(myPoints);
                //Crear la explusion
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
