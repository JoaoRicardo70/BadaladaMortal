using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSalto : MonoBehaviour, IInteractable
{
    GameObject player;

    Vector3 umPosicao;

    Vector3 novaPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); //usar só GameObject.Find

        novaPos= gameObject.transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Usar()
    {
        //salto
        umPosicao= player.transform.position;
        //esperar antes
        player.transform.position=new Vector3(novaPos.x,novaPos.y,novaPos.z);
        Debug.Log("Pulou");
    }
}
