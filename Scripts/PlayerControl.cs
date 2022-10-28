using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform moveOrientation;
    public float playerSpeed = 10.0f;
    public float xLimit = 20.0f, yLimit = 11.0f;
    public GameObject theShield;
    public float turnTime = 1.0f;
    public Vector3 shipRot;
    public string enemyTag = "EnemyShip";
    public string powerUpTag = "powerUP";
   // public string sceneTag = "Wall";
    public GameObject explosion;
    public GameObject startCanon;
    public int maxPlayerEnergy = 10;
    public int currentEnergy;
    public HealthBarScript healthBar;

    private float xPos, yPos;
    private Vector3 originalRot;
    private bool shieldActive;
    private float shieldCounter;
    private GameObject currentCanon;

    // Start is called before the first frame update
    void Start()
    {
        theShield.SetActive(false);
        originalRot = transform.eulerAngles;
        SetCanon(startCanon);

        currentEnergy = maxPlayerEnergy;
        healthBar.SetMaxHealth(maxPlayerEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        ShieldControl();
    }

    void ShieldControl()
    {
        //Esta activado el escudo?
        if(shieldActive)
        {
            //El contador de tiempo ira de 0 a 1 siempre. por tanto, dividimos por el tiempo total
            shieldCounter += Time.deltaTime / turnTime;

            if(shieldCounter>=1.0f)
            {
                shieldActive = false;
                shieldCounter = 1.0f;
                theShield.SetActive(false);
            }
            transform.rotation = Quaternion.Euler(originalRot + shipRot * shieldCounter);
        }
        else
        {
            if(Input.GetButtonDown("Jump"))
            {
                shieldActive = true;
                shieldCounter = 0.0f;
                theShield.SetActive(true);
            }
        }
    }

    void UpdatePosition()
    { 
        //Calculamos la posicion de la nave en ejes locales de MoveOrientation
        xPos += Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        yPos += Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        //Añadir limitaciones al movimiento
        xPos = Mathf.Clamp(xPos, -xLimit, +xLimit);
        yPos = Mathf.Clamp(yPos, -yLimit, +yLimit);

        //Convertimos la posición en coordenadas locales a coordenadas globales
        transform.position = moveOrientation.right * xPos + moveOrientation.up * yPos;
    }



    //es una funcion de cambiar el cañon.
    //tiene dos partes: 1.Quitar el cañon el antigou    2.Poner el cañon nuevo, colgado a mi nave
    public void SetCanon(GameObject newCanon)
    {
        if (currentCanon != null) Destroy(currentCanon);
        currentCanon = Instantiate(newCanon, transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {
            currentEnergy--;
            healthBar.SetHealth(currentEnergy);
            if (currentEnergy <= 0)
            {
                //Crear la explusion
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (other.tag == powerUpTag)
        {
            Destroy(gameObject);
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == sceneTag)
        {
            currentEnergy--; 
        }
    }*/
}
