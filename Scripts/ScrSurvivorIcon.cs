using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EstadoNPC{

    Vivo,
    Fugiu,
    Morto

}

public class ScrSurvivorIcon : MonoBehaviour
{

    [SerializeField] GameObject icon;
    [SerializeField] GameObject janelaFecha;
    public Sprite [] sprites;
    private EstadoNPC estadoAtual = EstadoNPC.Vivo;
    private Image imagemIcone;
    public Image [] icone = new Image [10];
    public Image [] fechaJanela = new Image [3];
    private Sprite vivo, morto, fugiu;
    int iconeAtual;
    public int fecharUsos = 3;
    public bool estadoJanela;
    public int vitima;
    public bool verificaEstado;
    
    
    // Start is called before the first frame update
    void Start()
    {
        vivo = Resources.Load<Sprite>("alive_icon");
        morto = Resources.Load<Sprite>("skull_icon");
        fugiu = Resources.Load<Sprite>("exit_icon");
        iconeAtual = 0;
        fecharUsos = 3;
        vitima = 3;
        verificaEstado = true;
        //estadoJanela = true;
        //icone[0].sprite = morto;
        //imagemIcone = GetComponent<Image>();

        AtualizarIcone();
        apagaHabilidade();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void AtualizarIcone(){

        foreach (Image imagem in icone){

            if (imagem != null){ // Certifica-se de que a imagem não é nula
            
                imagem.sprite = sprites[(int)estadoAtual];
            }
        }
    }

    private void AtualizarIndiceIcone(){

        iconeAtual++;
        if (iconeAtual >= icone.Length){

            iconeAtual = 0;
        }

    }

    public void Fugir(){

        if (iconeAtual >= 0 && iconeAtual < icone.Length && icone[iconeAtual] != null){
            
            estadoAtual = EstadoNPC.Fugiu;
            icone[iconeAtual].sprite = sprites[(int)estadoAtual];
        }

        // Move para o próximo ícone (se necessário)
        iconeAtual++;

        // Reseta o índice se atingir o final do array
        if (iconeAtual >= icone.Length){
            
            iconeAtual = 0;
        }
        //estadoAtual = EstadoNPC.Fugiu;
        //AtualizarIcone();
        //AtualizarIndiceIcone();

    }

    public void SerMorto(){

        if (iconeAtual >= 0 && iconeAtual < icone.Length && icone[iconeAtual] != null)
        {
            estadoAtual = EstadoNPC.Morto;
            icone[iconeAtual].sprite = sprites[(int)estadoAtual];
        }

        // Move para o próximo ícone (se necessário)
        iconeAtual++;

        // Reseta o índice se atingir o final do array
        if (iconeAtual >= icone.Length){
            
            iconeAtual = 0;
        }

    }

    public void apagaHabilidade(){

        for(int i = 0; i < fechaJanela.Length; i++){

            fechaJanela[i].gameObject.SetActive(false);

        }

        for(int i = 0; i < fecharUsos; i++){

            fechaJanela[i].gameObject.SetActive(true);
        }
    }

    public void interagirJanela(){

        if (fecharUsos > 0){

            fecharUsos--;
            apagaHabilidade();

        }else{

            Debug.Log("Não fecha mais janelas!");

        }

    }

    public void recuperaUsos(){

        if(fecharUsos < 3 && fecharUsos >= 0){

            fecharUsos++;
            apagaHabilidade();

        }else{

            Debug.Log("Você não pode conseguir mais usos para travar janelas!");

        }

    }

}
