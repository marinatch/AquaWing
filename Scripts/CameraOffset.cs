using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Transform followThis;
    public Vector3 multiplier;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void LateUpdate()
    {
        if (followThis != null)
        {
            //movemos la camara en funcion de la posicion de la nave del jugador, teniendo en cuenta el multiplicador
            transform.position = originalPos + new Vector3(
                followThis.position.x * multiplier.x,
                followThis.position.y * multiplier.y,
                followThis.position.z * multiplier.z
                );
            
        }
    }
}
