using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesControl : MonoBehaviour
{
    public EnemyWave[] enemyWaves;
    public float rebootTime;
    public float timeFactor = 1.25f;
   // public GameObject finalBoss;
   // public float finalBossTime;

    private float timeCounter;
    private float timeMultiplier = 1.0f;
    // private bool finalBossCreated;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0.0f;
        Time.timeScale = timeMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;  // *timeMultiplier;

        if (timeCounter > rebootTime) RebootWaves();
        WavesUpdate();
    }

    void RebootWaves()
    {
        //multiplicar el tiempo
        timeMultiplier *= timeFactor;

        Time.timeScale = timeMultiplier;

        //ponemos el contador de tiempo a 0 
        timeCounter = 0.0f;

        //ponemos todos los hasStarted y hasFinished a falso
        for(int i = 0; i < enemyWaves.Length; i++)
        {
            enemyWaves[i].hasFinished = enemyWaves[i].hasStarted = false;
            enemyWaves[i].currentEnemy = 0;
        }
    }

    void WavesUpdate()
    {
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            //si hemos acabado no hacemos nada
            if (!enemyWaves[i].hasFinished)
            {
                if (enemyWaves[i].hasStarted)
                {
                    //creamos un enamigo cuando llega el momento
                    if (timeCounter > enemyWaves[i].nextEnemyTime)
                    {
                        Instantiate(enemyWaves[i].enemyType);
                        enemyWaves[i].nextEnemyTime = timeCounter + enemyWaves[i].spawnTime;
                        enemyWaves[i].currentEnemy++;

                        if (enemyWaves[i].currentEnemy >= enemyWaves[i].numberOfEnemies)
                        {
                            enemyWaves[i].hasFinished = true;
                        }
                    }
                }
                else
                {
                    //Empezamos en cuando llega el momentp
                    if (timeCounter > enemyWaves[i].creationTime)
                    {
                        enemyWaves[i].hasStarted = true;
                        enemyWaves[i].nextEnemyTime = timeCounter;
                    }
                }
            }
        }
    }
}

//Serealizar la clase para poder usarla como variable
[System.Serializable]

public class EnemyWave
{
    //variables del editor
    public float creationTime;
    public int numberOfEnemies;
    public float spawnTime;
    public GameObject enemyType;

    //vareables Internas
    [HideInInspector] //Las vareables privadas solo se puede accederlas desde la misma clase
    public bool hasStarted, hasFinished;
    [HideInInspector]
    public int currentEnemy = 0;
    [HideInInspector]
    public float nextEnemyTime;
}
