using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class scrHidder : MonoBehaviour, IInteractable
{
    public NavMeshAgent navMesh;

    GameObject hud;
    ScrSurvivorIcon scrSurvivorIcon;
    public Animator anim;
    private GameObject model;
    public bool idleAnimar=true;
    public bool escapou;

    scrEsconderijo scriptEsconderijo;
    scrJanela scriptJanela;
    public float margemAlvo = 1f;
    public float waitTime = 100000; //melhorar tempo de abrir janela
    public float timeRotate = 2;
    public float walkSpeed = 6;

    //provisorio achar player, melhor fazer por layermask
    public Transform jogador;
    public float distanciaJogador= 4.0f;

    //estados
    public bool escondido;
    public bool escapando;
    public bool perigo;
    public bool naJanela;

    // public float radius=15;
    public float angulo = 90;
    public LayerMask windowMask;
    public LayerMask obstacleMask;

    //
    public Transform[] janelas;
    public GameObject[] posJanelas;
    // public Transform janela;
    int alvoJanela;
    public int numJanelas = 0;

    Vector3 janelaPosition = Vector3.zero;
    Vector3 m_janelaPosition;

    // public Vector3 oPosition;

    public float m_waitTime; //tempo pra abrir janela
    float m_timeRotate;
    bool m_janelaPerto; //m_playerInRange
    bool m_proxJanela; //mPlayer_Near
    bool m_fugindo; //m_ispatrol
    bool m_achouJanela; //m_caught

    //fugindo
    public GameObject[] rotas;
    int seguirRota;
    public int numRotas;
    int varia;

    private GameObject central;
    scrGeral geralScript;

    //TODO: funcionar para mais de um escondedor
    //TODO: prioridade entre ir para janela ou se esconder depois de fugir se baseando no quanto falta para abrir a janela
    //TODO: cada um ter um renderer diferente
    //todo: SISTEMA DE DETECÇÃO POR SALA
    //todo: SISTEMA DE DETECÇÃO POR SOM


    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        model= gameObject.transform.GetChild(0).gameObject;
        
        //janelas= new Transform[numJanelas];
    }

    // Start is called before the first frame update
    void Start()
    {

        hud = GameObject.Find("HUD");
        escapou = false;
        // int i=0;
        // janelas=GameObject.FindWithTag("Janela").transform;

        //escondido=true;//desativar colisor, aparecer indicação e desativar mesh

        // oPosition=transform.position;
        jogador = GameObject.FindWithTag("Player").transform; //usar só GameObject.Find
        posJanelas = GameObject.FindGameObjectsWithTag("Janela");
        central= GameObject.FindGameObjectWithTag("Central");
        geralScript= central.GetComponent<scrGeral>();
        foreach (GameObject item in posJanelas) //numJanelas = posJanelas.Length; ?
        {
            numJanelas++;
        }

        rotas= GameObject.FindGameObjectsWithTag("Rota");
        foreach (GameObject item in rotas)
        {
            numRotas++;
        }

        m_waitTime = waitTime;
        //Achar janela mais proxima, se não estivr ameaçado ir até janela
        //    alvoJanela=0;

        perigo= false;
        naJanela= false;
        escondido=false;
        escapando=false;

        navMesh.isStopped = false;
        navMesh.speed = walkSpeed;
        CalcularDistancia();
        scrSurvivorIcon = hud.GetComponent<ScrSurvivorIcon>();
        //    navMesh.SetDestination(waypoint[currentWaypointIndex].position);

        anim= model.GetComponent<Animator>();
    }


    void Update()
    {
        // Estado();
        // navMesh.SetDestination(janela.position);
        if(!perigo )
        {
        ProcurarJanela();
        }

        if (escapando)
        {
        if (Vector3.Distance(transform.position, rotas[seguirRota].transform.position) <= margemAlvo)//chegou no waypoint
        {
            //TODO: fazer procurar esconderijo baseado na sala
            Stop();
            escondido=true;
            Debug.Log("chegou no waypoint");
            Esconder();


        }
        Debug.Log("cade o waypoint");
        }




        // DetectarJogador();
        // Esconder();
        // CalcularDistancia();// BIG-0 NOTATION , parar de calcular quando encontrar alvo
    }

    void CalcularDistancia() //identifica janela mais proxima
    {

        float[] atualDist = new float[numJanelas];
        int indexDist;
        float menorDist;
        // for (int i = 0; i < janelas.Length; i++)
        // {
        //     atualDist[i]= Vector3.Distance(transform.position,janelas[6++i].position);
        //     if (atualDist[i]<anteriorDist)
        //     {
        //         menorDist=atualDist[i];
        //     }else
        //     {
        //     menorDist=anteriorDist;
        //     anteriorDist=atualDist[i];
        //     }
        //     Debug.Log("Teste" + atualDist);
        // }
        // alvoJanela=menorDist;



        for (int i = 0; i < posJanelas.Length; i++)
        {
            atualDist[i] = Vector3.Distance(transform.position, posJanelas[i].transform.position); //adiciona janelas no array atualDist 
            Debug.Log("atualDist = " + atualDist[i]);
        }
        menorDist = atualDist[0];
        indexDist = 0;

        for (int i = 0; i < atualDist.Length; i++) //menor distancia
        {
            if (atualDist[i] < menorDist)
            {
                menorDist = atualDist[i];
                indexDist = i;
                //TODO: tirar essa opção da lista
                //Debug.Log("o menor é o segundo " + menorDist);
            }
        }
        Debug.Log("menor= " + menorDist);
        alvoJanela = indexDist; //deixar automatica pegar index
        // posJanelas[alvoJanela].transform.GetChild(1).gameObject.SetActive(true);//setar ativo filho immagem da janela escolhida, Alvo la
        scriptJanela=posJanelas[alvoJanela].GetComponent<scrJanela>();//pega script
        Debug.Log("alvo = " + alvoJanela);

    }

    void ProcurarJanela() //vai até a janela alvo
    {
        if (!naJanela)
        {
                 navMesh.SetDestination(posJanelas[alvoJanela].transform.GetChild(0).transform.position); 
       
        }       
        if (Vector3.Distance(transform.position, posJanelas[alvoJanela].transform.GetChild(0).transform.position) <= margemAlvo)//locar na janela alvo e nao parar se estiver sendo perseguido
        {
            // Stop();

            //NA JANELA
            if (scriptJanela.abriu) //ABRIU JANELA
            {
                Debug.Log("Abriu janela"); //janela desaparece
                // Move(walkSpeed);
                // navMesh.SetDestination(janelas[alvoJanela].position);
                // ProxJanela();//ir atras da proxima janela, apagar
                posJanelas[alvoJanela].SetActive(false);
                geralScript.todosNpcs--;
                geralScript.pontos=geralScript.pontos-75;
                // StartCoroutine(ReloadScene()); //!APAGAR
                Destroy(gameObject);
                escapou = true;

                if(escapou){

                    Debug.Log("ESCAPOU!");
                    scrSurvivorIcon.Fugir();
                    escapou = false;
                }
                // >>>>>>>>>>> COLOCAR FUGIU AQUI <<<<<<<<<<

                //TODO: Se autodestruir

                m_waitTime = waitTime;

            }
            else //ABRINDO JANELA
            {
                if(!perigo){
                Stop();
                // m_waitTime -= Time.deltaTime; //começar a contagem
                scriptJanela.abrindo=true; //janela começa contagem de progreção
                naJanela=true;
                //tornar o abrindo da alvojanela true
                // Debug.Log("abrindo: " + m_waitTime + "%");
                }
            }
        }
    }
    void Move(float speed)
    {
        // navMesh.enabled=true;

        // navMesh.Resume();
        navMesh.ResetPath();
        navMesh.isStopped = false;
        navMesh.speed = speed;

        anim.SetBool("Parado", false);


        Debug.Log("nao parado");
    }

    void Stop() //parar agente
    {
        navMesh.speed = 0;
        navMesh.velocity = Vector3.zero; //talvez esse seja o erro
        navMesh.isStopped = true;
        // navMesh.Stop();


        // navMesh.ResetPath();
        anim.SetBool("Parado",true);
        // navMesh.SetDestination(transform.position); //!possivel solução provisorio
        // navMesh.enabled=false;


        Debug.Log("Parado");
    }

   
    // IEnumerator SairEsconderijo() //!Apagar
    // {
    //     varia= Random.Range(10,20);
    //     int tempo = varia;
    //     yield return new WaitForSeconds(tempo);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    // }

    // void DetectarJogador()
    // {
    //     float detectavel= Vector3.Distance(transform.position, jogador.transform.position);
    //     Debug.Log("Distancia jogador: " + detectavel);

    //     //fugir do jogador
    //     if (detectavel < distanciaJogador)
    //     {
        
    //         Debug.Log("FUGINDO!!");
    //         perigo=true;

    //         Vector3 dirJogador= transform.position - jogador.transform.position;
    //         Vector3 irFuga= transform.position + dirJogador;

    //         navMesh.SetDestination(irFuga);
    //     }
    //     else perigo=false;
    // }

    // void DetectarJogador()
    // {

    //     float limite= Vector3.Distance(transform.position, jogador.position);

    //     Debug.Log("Distancia do player: " + limite);

    //     if(limite < distanciaJogador) //
    //     {
    //         Debug.Log("player proximo");

    //         naJanela=false;
    //         scriptJanela.abrindo=false; //passar pro ontrigger

    //         // Move(8f); //se ele tava parado, nao ta mais

    //         //sistemas de fuga
    //         // Fugir();

    //         //ir pra direçao contrario do player e manter o limite de distancia, mudar pra ponto aleatorio ou rotas de fuga
    //         // Vector3 direcPlayer= transform.position - jogador.position;

    //         // Vector3 newPos= transform.position + direcPlayer;

    //         // navMesh.SetDestination(newPos); //!analisar pq ele parece continuar com a janela como alvo 


    //     }
    //     // else
    //     // {
    //         //TODO: se não estiver na janela e não estiver escondido procurar esconderijo
    //     // }
    // }

    void Fugir()
    {
        Move(8f);
        BuscarRota();
       Debug.Log("Fugindo");
        
        navMesh.SetDestination(rotas[seguirRota].transform.position);  
            
        

        escapando=true;
    }

    void Esconder()
    {
        escapando=false;
        scriptEsconderijo.usado=true;
        scriptEsconderijo.invocar=true;
        // scriptEsconderijo.hidder= this.gameObject;
        Debug.Log("voltou pra toca escondido");
        Destroy(gameObject);
        //!destruir npc e deixar o escoderijo especifico como usado, e ele spawnar o carinha dnv 
        //desaparecer
        //verificar com checksphere se player ta proximo
        //se sim fica quieto e reseta
        //se não calcula distancia, perigo falso, reaparece e vai pra janela

        // if (escondido)
        // {
        //     // sumir npc no ponto do esconderijo, desabilitar mesh
        //     //  verificar se player ta proximo, se n faz a contagem e volta
        //     // CalcularDistancia
        //     if (!perigo)
        //     {
        //         esperar um tepo
        //         ir na caça
        //         esconder=falso;
        //     }
        // }else
        // {
        //     aparecer npc
        // }
    }



    //TODO: melhorar metodo de buscar rota
    void BuscarRota()
    {
        float[] fugaDist = new float[numRotas];
        int indexRota;
        float rotaMenor;
        // for (int i = 0; i < janelas.Length; i++)
        // {
        //     atualDist[i]= Vector3.Distance(transform.position,janelas[6++i].position);
        //     if (atualDist[i]<anteriorDist)
        //     {
        //         menorDist=atualDist[i];
        //     }else
        //     {
        //     menorDist=anteriorDist;
        //     anteriorDist=atualDist[i];
        //     }
        //     Debug.Log("Teste" + atualDist);
        // }
        // alvoJanela=menorDist;



        for (int i = 0; i < rotas.Length; i++)
        {
            fugaDist[i] = Vector3.Distance(transform.position, rotas[i].transform.position); //adiciona janelas no array atualDist 
            Debug.Log("Distancia para Rota= " + fugaDist[i]);
        }
        rotaMenor = fugaDist[0];
        indexRota = 0;

        for (int i = 0; i < fugaDist.Length; i++)
        {
            if (fugaDist[i] < rotaMenor)
            {
                rotaMenor = fugaDist[i];
                indexRota = i;
                //TODO: tirar essa rota da lista
            }
        }

        Debug.Log("menor rota= " + rotaMenor);
        seguirRota = indexRota; //deixar automatica pegar index
        scriptEsconderijo=rotas[seguirRota].GetComponent<scrEsconderijo>(); //!pegar script
        Debug.Log("rota escolhida = " + seguirRota);
    }

    public void Usar()
    {
        if (scrSurvivorIcon != null){//JR

            scrSurvivorIcon.SerMorto();
            //scrSurvivorIcon.vitima = 1;
            //scrSurvivorIcon.verificaEstado = true;
            Debug.Log("foi pego");
        //scrSurvivorIcon.recuperaUsos();
       }else{

            Debug.Log("Não tem ícone");

       }

        //Debug.Log("foi pego");
        geralScript.todosNpcs--;
        geralScript.pontos=geralScript.pontos+100;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "Player")
        if(other.gameObject.CompareTag("Player"))
        {
            perigo=true;
            Debug.Log("perigo");
            Fugir();
            naJanela=false;
            scriptJanela.abrindo=false; //passar pro ontrigger

            //Fuga
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))//player ta longe da colisao
        {
            // perigo=false; condição um de tirar perigo
            Debug.Log("sem perigo");
            //procurar esconderijo
        }
    }

    //    void Ambiente()
    //     {
    //         Collider[] janelaInRange = Physics.OverlapSphere(transform.position, radius, windowMask);

    //         for (int i = 0; i < janelaInRange.Length; i++)
    //         {
    //             Transform janela= janelaInRange[i].transform;
    //             Vector3 irJanela= (janela.position - transform.position).normalized;
    //             if (Vector3.Angle(transform.forward,irJanela) < angulo / 2)
    //             {
    //                 float distanceJanela= Vector3.Distance(transform.position, janela.position);
    //                 if (!Physics.Raycast(transform.position,irJanela,distanceJanela,obstacleMask))
    //                 {
    //                     m_janelaPerto=true;
    //                     m_fugindo=false;
    //                 }
    //                 else
    //                 {
    //                     m_janelaPerto=false; //m_playerInrange
    //                 }
    //             }
    //             if (Vector3.Distance(transform.position, janela.position) > angulo)//viewradius
    //             {
    //                 m_janelaPerto=false;
    //             }

    //             if (m_janelaPerto)
    //             {
    //                 m_janelaPosition= janela.transform.position;
    //             }
    //     }
    //     }
}
