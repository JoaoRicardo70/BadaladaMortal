using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scrFimdeJogo : MonoBehaviour
{
    
    //scrTimer timer;
     //GameObject tela;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void tentarNovamente(){

        Debug.Log("BORA DE NOVO!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void voltaraoMenu(){

        Debug.Log("VOU PRO MENU! CANSEI DESSE JOGO!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

    }

}
