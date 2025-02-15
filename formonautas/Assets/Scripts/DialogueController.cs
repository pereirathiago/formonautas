using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // Lista de áudios que serão tocados.
    public List<AudioClip> audioClips;

    // Áudio source para tocar os áudios.
    public AudioSource audioSource;

    // Lista de textos dos diálogos.
    public List<string> dialogueTexts;

    // Componente TMP para exibir o texto na tela.
    public Text dialogueText;

    // Índice para controlar qual diálogo está sendo mostrado.
    private int currentDialogueIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Função para alternar para o próximo diálogo
    public void ShowNextDialogue()
    {
        FaseController faseController = FindObjectOfType<FaseController>();
        faseController.FaseIniciada = false;
        // Verifica se o índice está dentro dos limites das listas
        if (currentDialogueIndex < audioClips.Count && currentDialogueIndex < dialogueTexts.Count)
        {
            // Toca o áudio correspondente ao índice atual
            audioSource.clip = audioClips[currentDialogueIndex];
            audioSource.Play();

            // Exibe o texto correspondente ao índice atual
            dialogueText.text = dialogueTexts[currentDialogueIndex];

            // Avança para o próximo diálogo
            currentDialogueIndex++;
        }
        else
        {
            // Caso não haja mais diálogos, pode-se fazer algo, como parar o áudio
            audioSource.Stop();
            RelatorioController.instance.PegarFaseAtual();
            faseController.FaseIniciada = true;
            faseController.AlterarEtapa();
        }
    }

    // Função que pode ser chamada para alternar para o próximo diálogo, por exemplo, ao pressionar uma tecla
    void Update()
    {
        // Detecta quando o jogador pressiona a tecla de interação (ex: espaço ou clique)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogue();
        }
    }
}
