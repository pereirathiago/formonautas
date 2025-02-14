using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FaseController : MonoBehaviour
{
    [SerializeField] private string nomeFase;
    [SerializeField] private GameObject objTitulo;
    [SerializeField] private TextMeshProUGUI tituloTxt;
    [SerializeField] private List<GameObject> objetosFase;

    // Start is called before the first frame update
    void Start()
    {
        tituloTxt.SetText(nomeFase);
        objTitulo.SetActive(true);
        foreach (GameObject obj in objetosFase)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarFase()
    {
        objTitulo.SetActive(false);
        foreach (GameObject obj in objetosFase)
        {
            obj.SetActive(true);
        }
    }
}
