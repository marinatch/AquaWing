using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossControl : MonoBehaviour
{
    public Transform moveOrientation;
    public float playerSpeed = 10.0f;
    public float xLimit = 20.0f, yLimit = 11.0f;
    public string enemyTag = "EnemyShip";
    public int maxPlayerEnergy = 10;
    public int currentEnergy;
    public GameObject explosion;
    public HealthBarScript healthBar;

    private float xPos, yPos;

    void Update()
    {
        UpdatePosition();
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
    }

        /*public float forwardSpeed, sideSpeed, hoverSpeed;
        public float mouseRotSpeed = 90.0f;

        //para volar
        private float activeForwardSpeed, activeSideSpeed, activeHoverSpeed;
        //para cambiar la velocidad de forma más suave
        private float forwardAcceleration = 2.5f, sideAcceleration = 2.0f, hoverAcceleration = 2.0f;
        private Vector2 mouseInput, screenCenter, mouseDistance;

        private float rollInput;
        public float rollSpeed = 90.0f, rollAcceleration = 3.0f;

        void Start()
        {
            //difinir el centro de la pantalla
            screenCenter.x = Screen.width * 0.5f;
            screenCenter.y = Screen.height * 0.5f;

            Cursor.lockState = CursorLockMode.Confined;
        }

        // Update is called once per frame
        void Update()
        {
            //para saber donde esta el raton
            mouseInput.x = Input.mousePosition.x;
            mouseInput.y = Input.mousePosition.y;

            //saber la distancia del centro de la pantalla
            mouseDistance.x = (mouseInput.x - screenCenter.x) / screenCenter.y;
            mouseDistance.y = (mouseInput.y - screenCenter.y) / screenCenter.y;


            mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1.0f);

            rollInput = Mathf.Lerp(rollInput, Input.GetAxis("Roll"), rollAcceleration * Time.deltaTime);

            //para aplicar la rotacion
            transform.Rotate(-mouseDistance.y * mouseRotSpeed * Time.deltaTime,
                              mouseDistance.x * mouseRotSpeed * Time.deltaTime, 
                              rollInput* rollSpeed * Time.deltaTime, Space.Self);

            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration*Time.deltaTime);
            activeSideSpeed = Mathf.Lerp(activeSideSpeed, Input.GetAxisRaw("Horizontal") * sideSpeed, sideAcceleration*Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration*Time.deltaTime);

            transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
            transform.position += (transform.right * activeSideSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
        }*/
    }
