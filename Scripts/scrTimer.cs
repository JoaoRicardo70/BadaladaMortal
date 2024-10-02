using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrTimer : MonoBehaviour
{

    [SerializeField] GameObject telaFimdejogo;
    [SerializeField] AudioSource batida;
    [SerializeField] float tempoAtual;
    [SerializeField] float tempoInicial;

    [SerializeField] TMP_Text contagem;

    public int minutos;
    public int segundos;

    public scrGeral geralScript;
    // Start is called before the first frame update
    void Start()
    {
        
        //tempoAtual = tempoInicial;

    }

    // Update is called once per frame
    void Update()
    {
        if(tempoAtual > 0){


         tempoAtual -= Time.deltaTime;

         //if((tempoInicial-tempoAtual)/60 == 5f){

                //Debug.Log("BADALADA!");
                //play.batida;

            //}

        }

        else if(tempoAtual <= 0) {
            
            
            tempoAtual = 0;
            Debug.Log("ACABOU O TEMPO!");
            
            geralScript.pontos=- 125 * geralScript.todosNpcs;
            
            telaFimdejogo.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;



        } 

        
        minutos = Mathf.FloorToInt(tempoAtual/60);
        segundos = Mathf.FloorToInt(tempoAtual % 60);
        contagem.text = string.Format("{0:00}:{1:00}", minutos, segundos);

                
        if (minutos % 5 == 0 && segundos == 0){

            Debug.Log("BADALADA!");
            batida.Play();

        }
    }


}