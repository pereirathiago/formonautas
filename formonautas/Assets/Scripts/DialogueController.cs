using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // Lista de �udios que ser�o tocados.
    public List<AudioClip> audioClips;

    // �udio source para tocar os �udios.
    public AudioSource audioSource;

    // Lista de textos dos di�logos.
    public List<string> dialogueTexts;

    // Componente TMP para exibir o texto na tela.
    public Text dialogueText;

    // �ndice para controlar qual di�logo est� sendo mostrado.
    private int currentDialogueIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Fun��o para alternar para o pr�ximo di�logo
    public void ShowNextDialogue()
    {
        FaseController faseController = FindObjectOfType<FaseController>();
        faseController.FaseIniciada = false;
        // Verifica se o �ndice est� dentro dos limites das listas
        if (currentDialogueIndex < audioClips.Count && currentDialogueIndex < dialogueTexts.Count)
        {
            // Toca o �udio correspondente ao �ndice atual
            audioSource.clip = audioClips[currentDialogueIndex];
            audioSource.Play();

            // Exibe o texto correspondente ao �ndice atual
            dialogueText.text = dialogueTexts[currentDialogueIndex];

            // Avan�a para o pr�ximo di�logo
            currentDialogueIndex++;
        }
        else
        {
            // Caso n�o haja mais di�logos, pode-se fazer algo, como parar o �udio
            audioSource.Stop();
            RelatorioController.instance.PegarFaseAtual();
            faseController.FaseIniciada = true;
            faseController.AlterarEtapa();
        }
    }

    // Fun��o que pode ser chamada para alternar para o pr�ximo di�logo, por exemplo, ao pressionar uma tecla
    void Update()
    {
        // Detecta quando o jogador pressiona a tecla de intera��o (ex: espa�o ou clique)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogue();
        }
    }
}
