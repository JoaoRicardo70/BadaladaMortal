using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    public bool estaPausado;

    GameObject jogador;
    [SerializeField] string JogadorNome = "PlayerCapsule v3";
    StarterAssetsInputs assetsInputs;
    //

    private void Awake()
    {
        menuPausa = GameObject.Find("Pausa");
        jogador = GameObject.Find(JogadorNome);
        assetsInputs = jogador.GetComponent<StarterAssetsInputs>();
    }

    void Start()
    {
        menuPausa.SetActive(false);
        estaPausado = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pausa"))
        {
            if (!estaPausado)
            {
                estaPausado = true;
            }
            else estaPausado = false;
        }

        if (estaPausado) Pausar();
        else Continuar();
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pausar()
    {
        estaPausado = true;
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        assetsInputs.cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Continuar()
    {
        estaPausado = false;
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        assetsInputs.cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
}