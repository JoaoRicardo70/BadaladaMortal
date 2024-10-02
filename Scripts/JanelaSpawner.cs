using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaSpawner : MonoBehaviour
{
    private GameObject[] janelas;

    private void Awake()
    {
        janelas = GameObject.FindGameObjectsWithTag("Janela");

        Debug.Log("Encontrados " + janelas.Length + " objetos");
        /*for (int i = 0; i < janelas.Length; i++)
        {
            Debug.Log("Objeto " + i + ":  " + janelas[i].name);
        }*/
    }

    void Start()
    {
        for (int i = 0; i < janelas.Length; i++)
        {
            janelas[i].SetActive(false);
        }

        int rng = Random.Range(0, janelas.Length);
        Debug.Log("RNG = " + rng);
        janelas[rng].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
