using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitAxis : MonoBehaviour
{

    private SceneControl theControl;

    void Start()
    {
        //cogemos el script sceneControl del objeto SceneControl de la escena 
        theControl = GameObject.Find("SceneControl").GetComponent<SceneControl>();
    }


    void LateUpdate()
    {
        //multiplicamos cada eje de la animacion por el multiplicador
        transform.position = new Vector3(
         transform.position.x * theControl.currentMultiplier.x,  // transform.position.x * multiplier.x,
         transform.position.y * theControl.currentMultiplier.y, // transform.position.y * multiplier.y,
         transform.position.z * theControl.currentMultiplier.z // transform.position.z * multiplier.z
            );
    }

    /* public void SetMultiplier(Vector3 newMultiplier)
     {
         multiplier = newMultiplier;

     }*/
}
