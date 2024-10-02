using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface IInteractable
// {
//     public void Usar();
// }

public class FPController : MonoBehaviour
{
    CharacterController characterController;

    // inputs
    private float h;
    private float v;
    private bool _puloInput;
    private bool _correrInput;
    private bool _interacaoInput;

    // movimento
    /*const*/
    float Gravidade = -9.81f;
    private Vector3 aceleramento;
    [SerializeField] float velBase = 10f;
    [SerializeField] float velAndar;
    private float corrida;
    public float velocorrida;

    [SerializeField] private float alturaPulo = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGround; //!Usar isGrounded do charcontrol?

    // intera��o
    [SerializeField] private Transform mira;
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask layerIndex;
    [SerializeField] private float pegarAlcance = 1f;

    // pausa
    bool _pausado;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _pausado = false;
    }

    private void FixedUpdate()
    {
        Mover();

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void Update()
    {
        // pausa
        if (Input.GetButtonDown("Cancel"))
        {
            if (_pausado)
            {
                Resumir();
            }
            else Pausar();
        }

        // inputs
        if (!_pausado)
        {
            //movimento        
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            //pulo
            if (Input.GetButtonDown("Jump"))
            {
                _puloInput = true;
            }

            //interagir
            if (Input.GetMouseButtonDown(1))
            {
                _interacaoInput = true;
            }

            //Mapa
            /*
            if (Input.GetButtonDown("Mapa"))
            {
                player._mapaInput = true;
            }*/

            //Correr 
            if (Input.GetButtonDown("Correr"))
            {
                _correrInput = true;
            }
            if (Input.GetButtonUp("Correr"))
            {
                _correrInput = false;
            }

        }

        // movimento
        Pular();
        // Interagir();
    }

    void Mover()
    {
        //correr bem basico
        if (_correrInput)
        {
            corrida = velocorrida;
        }
        else corrida = 1;

        if (v <= 0)
        {
            velAndar = (velBase * corrida) / 1.5f;
        }
        else velAndar = velBase * corrida;

        Vector3 move = transform.right * h + transform.forward * v;
        characterController.Move(move.normalized * velAndar * Time.deltaTime);
    }

    void Pular()
    {
        if (isGround && aceleramento.y < 0)  // reseta acelera��o do cair
        {
            aceleramento.y = -2f;
        }

        if (_puloInput)
        {
            if (isGround)
            {
                aceleramento.y = Mathf.Sqrt(alturaPulo * -2 * Gravidade);
            }
        }
        aceleramento.y += Gravidade * Time.deltaTime;
        characterController.Move(aceleramento * Time.deltaTime);
        _puloInput = false;
    }

    // void Interagir()
    // {
    //     RaycastHit hitInfo;
    //     Debug.DrawRay(mira.position, cam.forward * pegarAlcance, Color.red);

    //     if (Physics.Raycast(mira.position, cam.TransformDirection(Vector3.forward), out hitInfo, pegarAlcance, layerIndex))
    //     {
    //         if (_interacaoInput)
    //         {
    //             if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interagivel))
    //             {
    //                 interagivel.Usar();
    //             }
    //             _interacaoInput = false;
    //         }
    //     }
    // }


    public void Pausar()
    {
        Time.timeScale = 0f;
        //camScript.currentMouseDelta = new Vector2(0, 0); //TODO: melhor travar camera com componente camera
        _pausado = true;
    }

    public void Resumir()
    {
        Time.timeScale = 1f;
        _pausado = false;
    }
}
