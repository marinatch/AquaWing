using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonsChange : MonoBehaviour
{
    public GameObject[] theCanons;

    private SceneControl sceneControl;
    private int currentSide;

    void Start()
    {
        sceneControl = GameObject.Find("SceneControl").GetComponent<SceneControl>();
        UpdateCanons();
       
    }

    void Update()
    {
        //cuando cambia la current cam
       if(currentSide != sceneControl.GetCurrentCam())
       {
            currentSide = sceneControl.GetCurrentCam();
            UpdateCanons();
       }
    }
    void UpdateCanons()
    {
        for(int i=0; i<theCanons.Length; i++)
        {
            theCanons[i].SetActive(i == currentSide);
        }
    }
}
