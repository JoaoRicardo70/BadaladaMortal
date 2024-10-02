using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject MenuPrincipal;
    [SerializeField] GameObject MenuOpcoes;

    private void Start()
    {
        MenuOpcoes.SetActive(false);
        MenuPrincipal.SetActive(true);
    }

    public void Jogar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
}
