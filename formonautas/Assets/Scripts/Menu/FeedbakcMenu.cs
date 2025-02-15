using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbakcMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] estrelas;

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

        int faseAtual = RelatorioController.instance.FaseAtual; // Pega a fase atual
        int estrelasGanhas = RelatorioController.instance.CalcularEstrelas(faseAtual);

        Debug.Log("Fase " + faseAtual + " - Estrelas: " + estrelas);

        for (int i = 0; i < estrelas.Length; i++)
        {
            estrelas[i].SetActive(i < estrelasGanhas);
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
