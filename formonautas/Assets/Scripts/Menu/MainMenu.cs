using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selecionarFaseMenu;
    [SerializeField] private GameObject relatorioMenu;
    [SerializeField] private GameObject relatorioDetalhado;
    [SerializeField] private GameObject emailMenu;
    [SerializeField] private GameObject creditosMenu;

    public void Awake()
    {
        loginMenu.SetActive(true);
        mainMenu.SetActive(false);
        selecionarFaseMenu.SetActive(false);
        relatorioMenu.SetActive(false);
        relatorioDetalhado.SetActive(false);
        emailMenu.SetActive(false);
        creditosMenu.SetActive(false);
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
}
