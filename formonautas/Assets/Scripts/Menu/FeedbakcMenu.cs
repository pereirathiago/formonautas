using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbakcMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] estrelas;

    public AudioClip audioClip3Estrelas;
    public AudioClip audioClip2Estrelas;
    public AudioClip audioClip1Estrela;
    public AudioSource audioSource;

    private void OnEnable()
    {
        CalcularPontuacao();
    }

    private void CalcularPontuacao()
    {
        if (RelatorioController.instance == null)
        {
            Debug.LogError("RelatorioController não encontrado!");
            return;
        }

        int faseAtual = RelatorioController.instance.FaseAtual;
        int estrelasGanhas = RelatorioController.instance.CalcularEstrelas(faseAtual);

        Debug.Log("Fase " + faseAtual + " - Estrelas: " + estrelas);

        for (int i = 0; i < estrelas.Length; i++)
        {
            estrelas[i].SetActive(i < estrelasGanhas);
        }
        TocarSomEstrelas(estrelasGanhas);
    }

    private void TocarSomEstrelas(int estrelasGanhas)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não atribuído!");
            return;
        }

        AudioClip somParaTocar = null;

        if (estrelasGanhas == 3)
        {
            somParaTocar = audioClip3Estrelas;
        }
        else if (estrelasGanhas == 2)
        {
            somParaTocar = audioClip2Estrelas;
        }
        else if (estrelasGanhas == 1)
        {
            somParaTocar = audioClip1Estrela;
        }

        if (somParaTocar != null)
        {
            audioSource.Stop();
            audioSource.clip = somParaTocar;
            audioSource.Play();
        }
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ProximaFase(string nome)
    {
        SceneManager.LoadScene(nome);
    }
}
