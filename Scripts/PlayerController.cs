using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Usar();
}

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    //Áudios
    [SerializeField] AudioSource passos;

    // inputs
    private float h;
    private float v;
    private bool _puloInput;
    private bool _correrInput;
    private bool _interacaoInput;

    // movimento
    const float Gravidade = -9.81f;
    private Vector3 aceleramento;
    [SerializeField] float velBase = 10f;
    [SerializeField] float velAndar;
    private float corrida=1;
    public float velocorrida;

    [SerializeField] private float alturaPulo = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGround; //!Usar isGrounded do charcontrol?

    // interacao
    [SerializeField] private Transform mira;
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask layerIndex;
    [SerializeField] private float pegarAlcance = 1f;

    [SerializeField] private scrCamera camScript;

    // pausa
    bool _pausado;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Debug.Log("inicio");
        _pausado = false;
    }

    // private void FixedUpdate()
    // {
    //     Mover();

    //     isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    // }

    void Update()
    {
        // pausa
        // if (Input.GetButtonDown("Cancel"))
        // {
        //     if (_pausado)
        //     {
        //         Resumir();
        //     }
        //     else Pausar();
        // }

        // inputs
        // if (!_pausado)
        // {
            //movimento        
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            Mover();


            isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            //pulo
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("pulo");
                _puloInput = true;
            }

            Pular();

            //interagir
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("click");
                _interacaoInput = true;
            } 

            Interagir();

            //Correr 
            if (Input.GetButtonDown("Correr"))
            {
                _correrInput = true;
            }
            if (Input.GetButtonUp("Correr"))
            {
                _correrInput = false;
            }

        // }

     
 
        
    }

    void Mover()
    {
        //tirar tudo isso
        corrida = 1;

        Debug.Log("vertical: " + v);
        Debug.Log("horizontal: " + h);


        if (v <= 0)
        {
            passos.Play();
        }
        velAndar = velBase * corrida;
        //tirar tudo isso

        Vector3 move = transform.right * h + transform.forward * v;
        characterController.Move(Time.deltaTime * velAndar * move.normalized);
    }

    void Pular()
    {
        if (isGround && aceleramento.y < 0)  // reseta aceleracao do cair
        {
            aceleramento.y = -2f;
            Debug.Log("no chao");
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

    void Interagir()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(mira.position, cam.forward * pegarAlcance, Color.red);

        if (Physics.Raycast(mira.position, cam.TransformDirection(Vector3.forward), out hitInfo, pegarAlcance, layerIndex))
        {
            Debug.Log("Interagivel");

            if (_interacaoInput)
            {
                  Debug.Log("interagiu");

                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interagivel))
                {
                  
                    interagivel.Usar();
                }
            } 
            // _interacaoInput = false;
        }
                _interacaoInput = false;

    }


    // public void Pausar()
    // {
    //     Time.timeScale = 0f;
    //     //camScript.currentMouseDelta = new Vector2(0, 0); //TODO: melhor travar camera com componente camera
    //     _pausado = true;
    // }

    // public void Resumir()
    // {
    //     Time.timeScale = 1f;
    //     _pausado = false;
    // }
}