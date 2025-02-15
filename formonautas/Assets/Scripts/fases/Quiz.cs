using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Quiz : MonoBehaviour
{
    [System.Serializable]
    public class Pergunta
    {
        public GameObject objetoPergunta;
        public Button[] botoesRespostas;
        public int respostaCorretaIndex;
        public VideoClip clip;
    }

    [SerializeField] private Pergunta[] perguntas;
    public VideoPlayer videoPlayer;
    private int perguntaAtual = 0;

    public GameObject professor;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < perguntas.Length; i++)
        {
            perguntas[i].objetoPergunta.SetActive(i == 0);
        }

        ConfigurarBotoes();
    }

    void ConfigurarBotoes()
    {
        Pergunta pergunta = perguntas[perguntaAtual];

        for (int i = 0; i < pergunta.botoesRespostas.Length; i++)
        {
            int escolha = i;
            videoPlayer.clip = pergunta.clip;
            pergunta.botoesRespostas[i].onClick.AddListener(() => Responder(escolha));
            pergunta.botoesRespostas[i].gameObject.SetActive(true);
        }
    }

    public void Responder(int escolha)
    {
        Pergunta pergunta = perguntas[perguntaAtual];

        if (escolha == pergunta.respostaCorretaIndex)
        {
            pergunta.objetoPergunta.SetActive(false);

            perguntaAtual++;

            Debug.Log(perguntaAtual + " " + perguntas.Length);
            if (perguntaAtual < perguntas.Length)
            {
                Debug.Log("Próxima pergunta!");
                StartCoroutine(CentralizarMouse());

                perguntas[perguntaAtual].objetoPergunta.SetActive(true);
                ConfigurarBotoes();
            }
            else
            {
                FaseController faseController = FindObjectOfType<FaseController>();
                faseController.ObjSucesso.SetActive(true);
                RelatorioController.instance.FinalizarFase();
                professor.SetActive(false);
            }
        }
        else
        {
            pergunta.botoesRespostas[escolha].gameObject.SetActive(false);
        }
    }

    IEnumerator CentralizarMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Trava o cursor no centro da tela
        yield return null; // Espera um frame para garantir a atualização
        Cursor.lockState = CursorLockMode.None;    // Libera o cursor, agora ele estará centralizado
    }

}