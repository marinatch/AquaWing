using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public float moveSpeed;
    public float offsetDist;

    private float zPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movemos el escenario en direccion z a velocidad moveSpeed
        zPos -= moveSpeed * Time.deltaTime;

        //Si nos hemos pasado movemos el escenario hacia adelante
        if(zPos<-offsetDist)
        {
            zPos += offsetDist;
        }

        transform.position = new Vector3(0.0f, 0.0f, zPos);
    }
}
