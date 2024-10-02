using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrJanela : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject hud;
    ScrSurvivorIcon icon;
    public float bloqueio=10000f;
    public bool abrindo;
    public bool abriu;
    public bool liberado=true;

    // Start is called before the first frame update
    void Start()
    {
        abrindo=false;
        abriu=false;
        icon = hud.GetComponent<ScrSurvivorIcon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (abrindo)
        {
            Debug.Log("JANELA ABRINDO AAA");
            liberado=false;
            Abrir();
        }else liberado=true;

        if (bloqueio<=0)
        {
            abriu=true;
            Destroy(gameObject);// npc escapou
        }
    }

    void Abrir()
    {
        bloqueio-= Time.deltaTime;
    }

    public void Usar()
    {
        if(icon.fecharUsos > 0){//JR

            Debug.Log("Janela fechou!!!!");
            icon.interagirJanela();
            //icon.estadoJanela = true;
            gameObject.SetActive(false);

        }else{

            Debug.Log("Não Barrica janela!");

        }
        
    }
}
