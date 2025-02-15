using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selecionarFaseMenu;
    [SerializeField] private GameObject relatorioMenu;
    [SerializeField] private GameObject relatorioDetalhado;
    [SerializeField] private GameObject emailMenu;
    [SerializeField] private GameObject creditosMenu;

    [SerializeField] private GameObject botaoRelatorio;
    [SerializeField] private GameObject botaoRelatorioNaoConcluido;

    [SerializeField] private TMP_InputField inputNomeAluno;
    [SerializeField] private TMP_InputField inputEmailAluno;


    [SerializeField] private GameObject[] estrelasFase1 = new GameObject[3];
    [SerializeField] private GameObject[] estrelasFase2 = new GameObject[3];
    [SerializeField] private GameObject[] estrelasFase3 = new GameObject[3];
    [SerializeField] private GameObject[] estrelasFase4 = new GameObject[3];
    [SerializeField] private GameObject[] estrelasFase5 = new GameObject[3];
    [SerializeField] private GameObject[] estrelasFase6 = new GameObject[3];

    public void Awake()
    {
        loginMenu.SetActive(true);
        mainMenu.SetActive(false);
        selecionarFaseMenu.SetActive(false);
        relatorioMenu.SetActive(false);
        relatorioDetalhado.SetActive(false);
        emailMenu.SetActive(false);
        creditosMenu.SetActive(false);

        botaoRelatorio.SetActive(false);
        botaoRelatorioNaoConcluido.SetActive(true);

        if (RelatorioController.instance != null)
        {
            if (RelatorioController.instance.nomeAluno != "")
            {
                loginMenu.SetActive(false);
                mainMenu.SetActive(true);
                botaoRelatorio.SetActive(true);
                botaoRelatorioNaoConcluido.SetActive(false);
                MostrarEstrelas();
            }
        }
    }

    public void TrocarMenu(GameObject menuMostrar)
    {
        loginMenu.SetActive(false);
        mainMenu.SetActive(false);
        selecionarFaseMenu.SetActive(false);
        relatorioMenu.SetActive(false);
        relatorioDetalhado.SetActive(false);
        emailMenu.SetActive(false);
        creditosMenu.SetActive(false);

        menuMostrar.SetActive(true);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void TrocarScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void ReceberNome()
    {
        RelatorioController.instance.nomeAluno = inputNomeAluno.text;
    }

    public void ReceberEmail()
    {
        GerarRelatorioWord();
        RelatorioController.instance.emailProfessor = inputEmailAluno.text;
        RelatorioController.instance.EnviarEmail();
    }

    public void GerarRelatorioWord()
    {
        if (RelatorioController.instance.GetTempoTotal() > 0)
        {
            RelatorioController.instance.SubstituirTemposNoWord();
        }
    }

    public void MostrarEstrelas()
    {
        MostrarEstrelasPorFase(1, RelatorioController.instance.GetTempoFase(1));
        MostrarEstrelasPorFase(2, RelatorioController.instance.GetTempoFase(2));
        MostrarEstrelasPorFase(3, RelatorioController.instance.GetTempoFase(3));
        MostrarEstrelasPorFase(4, RelatorioController.instance.GetTempoFase(4));
        MostrarEstrelasPorFase(5, RelatorioController.instance.GetTempoFase(5));
        MostrarEstrelasPorFase(6, RelatorioController.instance.GetTempoFase(6));
    }

    private void MostrarEstrelasPorFase(int fase, float tempoFase)
    {
        GameObject[] estrelas = null;

        switch (fase)
        {
            case 1:
                estrelas = estrelasFase1;
                break;
            case 2:
                estrelas = estrelasFase2;
                break;
            case 3:
                estrelas = estrelasFase3;
                break;
            case 4:
                estrelas = estrelasFase4;
                break;
            case 5:
                estrelas = estrelasFase5;
                break;
            case 6:
                estrelas = estrelasFase6;
                break;
        }

        int pontuacao = CalcularPontuacaoEstrelas(tempoFase);

        for (int i = 0; i < 3; i++)
        {
            estrelas[i].SetActive(i < pontuacao);
        }
    }

    private int CalcularPontuacaoEstrelas(float tempo)
    {
        if (tempo <= 60)
        {
            return 3;
        }
        else if (tempo <= 90)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
