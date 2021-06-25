using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CnControls;

/// <summary>
/// Clase que permite desplazar al personaje principal por la escena de juego
/// mediante la pulsación de los controles adecuados.
/// </summary>
public class PlayerController : MonoBehaviour {

    // Variables que ajustan el movimiento del personaje
    public float moveSpeed = 5.0f;
    public float turnSpeed = 60.0f;
    public float gravity = 9.81f;
    public Camera mainCamera;
    private Vector3 movePlayer;
    public float speed=0.3f;

    // Tiempo transcurrido entre pasos
    public float timeBetweenSteps = 0.5f;

    private Vector3 movement = Vector3.zero;
    private CharacterController myController;
    private AudioSource audioSource;



    private float nextStep;

    /// <summary>
    /// Método para inicializar variables que se llama una única vez al iniciar el script.
    /// </summary>
    void Start () {
        myController = gameObject.GetComponent<CharacterController>();
        audioSource = GetComponents<AudioSource>()[1];

    }

    /// <summary>
    /// Método para actualizar variables que se llama una vez por frame.
    /// </summary>
    void Update () {

        // Almacenamos los valores al pulsar las teclas indicadas.
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //Controles para dispositivo móvil
#if UNITY_ANDROID
        horizontalAxis = CnInputManager.GetAxis("Horizontal") * 0.03f;
        verticalAxis = CnInputManager.GetAxis("Vertical") * 0.05f;
#endif

        // Aplicamos la rotación del personaje.
        if (horizontalAxis > 0.1f)
        {
            //transform.Rotate(0, turnSpeed*Time.deltaTime, 0);
            movement = transform.up * horizontalAxis * moveSpeed * Time.deltaTime;
        } else if(horizontalAxis < -0.1f)
        {
            //transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            movement = transform.up * horizontalAxis * moveSpeed * Time.deltaTime;
        }
        
        //// Provocamos el movimiento hacia delante del personaje.
        //movement = transform.forward * verticalAxis * moveSpeed * Time.deltaTime;

        movement = new Vector3(horizontalAxis, 0, verticalAxis);

        if (verticalAxis > 0.1f)
        {
            //transform.Rotate(0, turnSpeed*Time.deltaTime, 0);
            movePlayer = movement.x * mainCamera.transform.right + movement.z * mainCamera.transform.forward;
        }
        else if (verticalAxis < -0.1f)
        {
            //transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            movePlayer = movement.x * mainCamera.transform.right + movement.z * mainCamera.transform.up;
        } else
        {
            movePlayer = movement.x * mainCamera.transform.right + movement.z * mainCamera.transform.forward;
        }


        // Aplicamos el efecto de la gravedad
        //movement.y -= gravity * Time.deltaTime;

        // Aplicamos el movimiento al CharacterController en la dirección calculada.
        myController.Move(movePlayer*speed);

        // Establecemos un tiempo entre pisadas para que el sonido parezca más natural.
        if (Mathf.Abs(verticalAxis) > 0.1f && Time.time > nextStep)
        {
            nextStep = Time.time + timeBetweenSteps;
            audioSource.Play();

        }

    }

}
