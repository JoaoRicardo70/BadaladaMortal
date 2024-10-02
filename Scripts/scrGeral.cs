using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class scrGeral : MonoBehaviour
{
    

    public int pontos=0; //ver se static ajuda

    public int todosNpcs=0;

    [SerializeField] GameObject telaFimdejogo;

    [SerializeField] TMP_Text pontuacao;



    

    // Update is called once per frame
    void Update()
    {
        if (todosNpcs<=0)
        {
            Debug.Log("GG vc venceu meu compadre");
            telaFimdejogo.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pontuacao.text= "Pontos: " + pontos.ToString();

            //TODO: fazer cena de vitoria
        }
    }
}
