using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scrSpawn : MonoBehaviour
{

    // public bool usado; //nao usar

    // private float esperaTempo; //nao usar2
    // public float timer=30f; //nao usar
    public GameObject[] esconderijos;
    public GameObject[] npc;
    // public float raio=10f; //nao usar
    // public bool seguro=true; //nao usar


    // public GameObject[] indicacoes; //so debug de onde vai ser escondido
    // public GameObject[] alvo; //debug de qual o alvo ativo

    int rand;
    // GameObject esconder; //nao usar 2
    scrEsconderijo EsconderijoScript;

    public int escondedores;
    // public GameObject hidder;
    // private IEnumerator co; //nao usar2

    // public bool invocar=true; //ativar com outro script

    // Start is called before the first frame update
    void Start()
    {
        // usado=true;
        //Xesperar um tempoX e mostrar indicador
        // esconder= esconderijos[rand]; //esconderijo escolhido

        // rand=Random.Range(0,esconderijos.Length); //escolhe esconderijo
        // EsconderijoScript= esconderijos[rand].GetComponent<scrEsconderijo>(); //!IMPORTANTE
        // EsconderijoScript.usado=true;
        
        // alvo[rand].SetActive(true); //mostra o alvo !fazer ser relativo a janela alvo 
        // indicacoes[rand].SetActive(true); //mostra onde ta escondido
        Debug.Log("Esconderijo: " + rand);

        //TODO FISHER-YATES

        Embaralha(esconderijos);

        for (int i = 0; i < npc.Length; i++)//coloca uma vez para cada escondedor
        {
            
                
            EsconderijoScript= esconderijos[i].GetComponent<scrEsconderijo>();
                

                Debug.Log("esconderijo da vez: " + rand);
                EsconderijoScript.hidder= npc[i];
                EsconderijoScript.usado=true;
           
           
            // if (!EsconderijoScript.usado) //se não ja estiver sendo usado, seleciona esse mesmo pra spawnar npc
            // {
            //     EsconderijoScript.hidder= npc[i];
            //     EsconderijoScript.usado=true;
            //     break;
            // }else //ja esse ja ta sendo usado
            // {
            //     //procura um numero aleatorio de novo e ve se ele ta sendo usado ou nao
            //     //se estiver sendo usado ve outro numero aleatorio ate achar um que nao ta
            // }
            
        }
        // co= Esperar(esperaTempo); //TODO: Aumentar tmepo
        // StartCoroutine(Esperar()); //spawna depois de um tempo
    }

    void Embaralha<T>(T[] array)
    {
        System.Random random= new System.Random();
        for (int i = esconderijos.Length-1; i > 0; i--)
        {
            int r= random.Next(0,i+1);
            
            T temp= array[i];
            array[i]= array[r];
            array[r]= temp;
            
        }
    }
    // void Update()
    // {
   
    //    VerificarPlayer(); //!chamar isso todo frame ta fazendo ele iniciar varios coroutine        
    
    // }

    // void VerificarPlayer()
    // {
    //   if(usado) //verificar se player ta perto se tiver um npc dentro
    //   {
    //      //ver se player ta perto
    //     if (Physics.CheckSphere(esconderijos[rand].transform.position, raio, 8))
    //     {
    //         Debug.Log("Player proximo");
    //         seguro=false;
            
    //         StopCoroutine(Esperar(timer));
            
    //     }else 
    //     {    
    //     seguro=true;
    //     Debug.Log("Player distante");
       
    //     StartCoroutine(Esperar(timer));
    //      //spawna depois de um tempo
    //     //spawnar aqui fazer ser uma unica vez ne pq se nao
    //     }
    //   }
    //     Debug.Log("nao usado");
    // }

  
    // IEnumerator Esperar(float esperaTempo)
    // {
        
    //    if (seguro)
    //    {
    //     Debug.Log("Seguro, pode nascer");

    //     yield return new WaitForSeconds(esperaTempo);
    //   if(invocar)
    //   {
    //     Instantiate(hidder, esconder.position, Quaternion.identity);
    //     invocar=false; //volta a ser 1 quando o npc volta
    //   }  
    //     indicacoes[rand].SetActive(false);
    //     usado=false; 


    //     Debug.Log("spawn");
    //    }else
    //    {
    //     Debug.Log("Perigoso");
    //    }
    // }
}
