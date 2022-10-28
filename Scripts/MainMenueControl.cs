using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenueControl : MonoBehaviour
{
    public int levelToLoad = 1;
    public Animator transition;
    public float transitionTime;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            PlayerPrefs.SetInt("currentScore", 0);

            StartCoroutine(LoadLevel(levelToLoad));
            //SceneManager.LoadScene(levelToLoad);
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //repreduce la animación
        transition.SetTrigger("Start");

        //Espera unos segundos
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
