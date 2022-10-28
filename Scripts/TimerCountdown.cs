using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    public Transform mainCamera, moveOrientation;
    public GameObject timeText;
    public int secondsLeft = 30;
    public bool counterOn = false;

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
    private float nextLevelTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeText.GetComponent<Text>().text = "00:" + secondsLeft;

        moveOrientation.rotation = mainCamera.rotation;
        gameOverText.SetActive(false);
        nextLevelText.SetActive(false);
        //StartCoroutine(TimeReduce());
        //actualizamos la puntuacion
        currentScore = PlayerPrefs.GetInt("currentScore", 0);

        //actualizamos el record
        hiScore = PlayerPrefs.GetInt("hiScore", 0);

    }

    // Update is called once per frame
    void Update()
    {
        GameOverControl();
        UpdateInterface();
        NextLevel();
       
        if (counterOn == false && secondsLeft > 0)
        {
            StartCoroutine(TimeReduce());
            if (playerShip != null && secondsLeft <= 0) NextLevel();
        }
    }

    IEnumerator TimeReduce()
    {
        counterOn = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            timeText.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            timeText.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        counterOn = false;
    }

    private void GameOverControl()
    {
        //si mi nave ha sido destruida, pongo en marcha el contador y al llegar a 2 vuelvo al menu
        if (playerShip == null)
        {
            gameOverText.SetActive(true);
            gameOverCounter += Time.deltaTime;
            if (gameOverCounter > timeToWait) StartCoroutine(LoadLevel(0));
        }
    }

    private void NextLevel()
    {
        nextLevelTimer += Time.deltaTime;
        if (nextLevelTimer >= 30.0f)
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
