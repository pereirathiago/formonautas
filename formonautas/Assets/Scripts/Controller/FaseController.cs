using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FaseController : MonoBehaviour
{
    [Header("Configurações do Titulo")]

    [SerializeField] private string nomeFase;
    [SerializeField] private GameObject objTitulo;
    [SerializeField] private TextMeshProUGUI tituloTxt;
    [SerializeField] private List<GameObject> objetosFase;

    [Header("Configurações das Etapas")]

    [SerializeField] private GameObject objSucesso;

    [SerializeField] private List<GameObject> etapas;
    [SerializeField] private GameObject etapaAtual;

    [Header("Configurações dos Objetos")]

    [SerializeField] private List<GameObject> objetosEncontrar;
    [SerializeField] private List<GameObject> objetosEncontrados;

    [SerializeField] private bool fasePrecisaTitulo;
    [SerializeField] private bool faseVerificacaoEncontrados = true;

    [SerializeField] private GameObject botoes;

    [SerializeField] private bool faseIniciada = false;

    public List<GameObject> ObjetosEncontrar { get => objetosEncontrar; set => objetosEncontrar = value; }
    public List<GameObject> ObjetosEncontrados { get => objetosEncontrados; set => objetosEncontrados = value; }
    public bool FaseIniciada { get => faseIniciada; set => faseIniciada = value; }
    public GameObject ObjSucesso { get => objSucesso; set => objSucesso = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (fasePrecisaTitulo)
        {
            tituloTxt.SetText(nomeFase);
            objTitulo.SetActive(true);
            foreach (GameObject obj in objetosFase)
            {
                obj.SetActive(false);
            }
        } else
        {
            RelatorioController.instance.PegarFaseAtual();
            IniciarFase();
        }
        faseIniciada = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (faseVerificacaoEncontrados && ObjetosEncontrados.Count != 0)
        {
            TerminarFaseEncontrar();
        }
    }

    public void IniciarFase()
    {
        objTitulo.SetActive(false);
        foreach (GameObject obj in objetosFase)
        {
            obj.SetActive(true);
        }
        FaseIniciada = true;
    }

    public void AlterarEtapa()
    {
        etapaAtual.SetActive(false);
        etapaAtual = etapas[etapas.IndexOf(etapaAtual) + 1];
        objetosFase[0].SetActive(false);
        etapaAtual.SetActive(true);
        FaseIniciada = true;

        if (botoes != null)
        {
            botoes.SetActive(true);
        }
    }

    public void TerminarFaseEncontrar()
    {
        if (objetosEncontrar.Count == objetosEncontrados.Count)
        {
            ObjSucesso.SetActive(true);
            RelatorioController.instance.FinalizarFase();
        }
    }
}
