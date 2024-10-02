using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEsconderijo : MonoBehaviour
{
    public bool usado;
    public bool seguro;

    public bool invocar=true; //atualizar

    public GameObject hidder;


    public float minTime=30f; //!DEFINIR TEMPO
    public  float maxTime=40f;
    public float realTime; //nao usar
    public float raio=10f; //nao usar

    void Awake()
    {
        usado=false;
        seguro=true;
        realTime= Random.Range(minTime,maxTime);
    }

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(usado)
        {
            VerificarPlayer();
            Debug.Log("usado");
        }
    }

     void VerificarPlayer()
    {
     
         //ver se player ta perto
        if (Physics.CheckSphere(transform.position, raio, 8)) //mudar pra collision trigger ativada quando usado
        {
            Debug.Log("Player proximo");
            seguro=false;
            
            StopCoroutine(Esperar(realTime));
            
        }else 
        {    
        seguro=true;
        Debug.Log("Player distante");
       
        StartCoroutine(Esperar(realTime));
         //spawna depois de um tempo
        //spawnar aqui fazer ser uma unica vez ne pq se nao
        }
      
        Debug.Log("nao usado");
    }

    IEnumerator Esperar(float esperaTempo)
    {
        
       if (seguro)
       {
        Debug.Log("Seguro, pode nascer");

        yield return new WaitForSeconds(esperaTempo);

        if(invocar)
        {
        Instantiate(hidder, transform.position, Quaternion.identity); //spawnar o msm q entrou
        invocar=false; //volta a ser 1 quando o npc volta
        }  

        usado=false; 


        Debug.Log("spawn"); //!CHAMAR SÓ UMA VEZ
       }else
       {
        Debug.Log("Perigoso");
       }
    }
}
