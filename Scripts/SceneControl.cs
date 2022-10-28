using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public Transform mainCamera, moveOrientation;
    public Transform[] theCams;
    public Vector3[] theMultipliers;
    public Vector3 currentMultiplier;
    public bool keyboardChange = true;
    public float changeTime = 2.0f;  //es el tiempo que va a durar la animación entra una posición y la otra
    public GameObject enemyShip;
    public GameObject playerShip;

    public int levelToLoad = 1;
    public Animator transition;
    public float transitionTime;

    public float timeToWait;
    public GameObject gameOverText;
    public int gameOverCam = 2;
    public float gameOverCounter = 0.0f;


    public float nextLevelWait;
    public GameObject nextLevelText;
    public float nextLevelCounter = 0.0f;

    public Text scoreText, hiScoreText;
    private int currentScore, hiScore;


    private int currentCam;
    private bool changing; // para saber si estoy cambiando de cameras
    private float counter;
    // para saber donde estoy y a donde me voy hace falta crear dos variables y dos rotaciones
    private Vector3 oldPos, newPos;
    private Quaternion oldRot, newRot;
    private Vector3 oldMultiplier, newMultiplier;

    private float nextLevelTimer = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        currentCam = 0;
        mainCamera.position = theCams[currentCam].position;
        moveOrientation.rotation = mainCamera.rotation = theCams[currentCam].rotation;
        currentMultiplier = theMultipliers[currentCam];
        gameOverText.SetActive(false);
        nextLevelText.SetActive(false);

        //actualizamos la puntuacion
        currentScore = PlayerPrefs.GetInt("currentScore", 0);

        //actualizamos el record
        hiScore = PlayerPrefs.GetInt("hiScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
        GameOverControl();
        NextLevel();
        UpdateInterface();
    }

    public int GetCurrentCam ()
    {
        return (currentCam);
    }

    public void IncreaseScore(int increaseValue)
    {
        currentScore += increaseValue;

        //comprobamos si la puntuacion actual es un record
        if (currentScore > hiScore)
        {

            hiScore = currentScore;
            PlayerPrefs.SetInt("hiScore", hiScore);
        }

        //Actualizamos la puntuacion actual
        PlayerPrefs.SetInt("currentScore", currentScore);
    }

    private void GameOverControl()
    {
        //si mi nave ha sido destruida, pongo en marcha el contador y al llegar a 2 vuelvo al menu
        if(playerShip==null)
        {
            gameOverText.SetActive(true);
            gameOverCounter += Time.deltaTime;
            if (gameOverCounter > timeToWait) StartCoroutine(LoadLevel(0));

            if (currentCam != gameOverCam) SetCam(gameOverCam);
        }
    }

    private void NextLevel()
    {
        nextLevelTimer += Time.deltaTime;
        if (nextLevelTimer >= 120.0f)
        {
            nextLevelText.SetActive(true);
            nextLevelCounter += Time.deltaTime;
            if (nextLevelCounter > nextLevelWait)
            {
                StartCoroutine(LoadLevel(levelToLoad));
                nextLevelText.SetActive(false);
            }
        }
        
    }

    private void CameraControl()
    {
        if (keyboardChange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SetCam(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SetCam(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SetCam(2);
        }

        //Estamos en transición? poner changing a true y empezar el contador
        if (changing)
        {
            counter += Time.deltaTime / changeTime; //Me enteresa que el counter vaya de zero a uno, para hacer bien las transiciones. Quiere dicir que quiero dividir entre el tiempo total. Porque cuando más grande sea el tiempo total (Al Makam) más pequeño sera el contador()
            if (counter >= 1.0f)
            {
                counter = 1.0f; //en 2 segundos llega el contador a uno. Lo pongo difinitivamente a uno para evitar problemas
                changing = false;//decir a la programa que se ha acabado la transición
            }
            //actualicar la transicion en cada fotograma
            newPos = theCams[currentCam].position;
            newRot = theCams[currentCam].rotation;

            //Lerp: Lineal Interpolation. Significa que tenemos que poner 2 posiciones (a, b) y un numero de 0 a 1.
            //el 0 quiere decir ponme en la posición a, el 1 en la posición b, 0.5 entre a y b y así
            //hacemos una transicion suave
            mainCamera.position = Vector3.Lerp(oldPos, newPos, counter);
            moveOrientation.rotation = mainCamera.rotation = Quaternion.Lerp(oldRot, newRot, counter);
            currentMultiplier = Vector3.Lerp(oldMultiplier, newMultiplier, counter);
        }

        else
        {
            //cuando no estamos en transición, cogemos la posicion y rotacion de la camera actual
            mainCamera.position = theCams[currentCam].position;
            mainCamera.rotation = theCams[currentCam].rotation;

        }
    }

    private void SetCam(int camToSet)
    {
        //cambiamos a la nueva cámara y activamos la animación
        currentCam = camToSet;
        changing = true;
        counter = 0.0f;
        oldPos = mainCamera.position; //de donde vengo
        newPos = theCams[currentCam].position;
        oldRot = mainCamera.rotation;
        newRot = theCams[currentCam].rotation;
        oldMultiplier = currentMultiplier;
        newMultiplier = theMultipliers[currentCam];

        //Versión antigua (cambio instaáneo)
        //mainCamera.position = theCams[camToSet].position;
        //  mainCamera.rotation = theCams[camToSet].rotation;

        //cambiar el multiplicador cuando cambio la camara(cambio instaáneo)
        // enemyShip.SendMessage("SetMultiplier", theMultipliers[currentCam]);

    }

    void UpdateInterface()
    {
        scoreText.text = "Score:" + currentScore;
        hiScoreText.text = "HiScore:" + hiScore;
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
