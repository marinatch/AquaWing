using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCanon : MonoBehaviour
{
    public float powerUpSpeed = 1.0f;
    public string targetTag = "PlayerShip";
    public GameObject theCanon;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * powerUpSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            //le decimos al playership que cambie de cañon
            other.gameObject.GetComponent<PlayerControl>().SetCanon(theCanon);

            Destroy(gameObject);
        }
    }
}
