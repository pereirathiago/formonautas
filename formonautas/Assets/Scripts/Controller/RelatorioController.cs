using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xceed.Words.NET;

public class RelatorioController : MonoBehaviour
{
    public static RelatorioController instance;

    [SerializeField] private float tempoTotal;
    [SerializeField] private float[] temposPorFase = new float[6];
    [SerializeField] private int faseAtual = -1;
    
    public string nomeAluno = "";
    public string emailProfessor;

    public int FaseAtual { get => faseAtual; set => faseAtual = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FaseAtual >= 0)
        {
            tempoTotal += Time.deltaTime;
            temposPorFase[FaseAtual] += Time.deltaTime;
        }
    }

    public void IniciarFase(int numeroFase)
    {
        if (numeroFase >= 0 && numeroFase < 6)
        {
            FaseAtual = numeroFase;
            temposPorFase[FaseAtual] = 0;
        }
    }

    public void FinalizarFase()
    {
        FaseAtual = -1;
    }

    public float GetTempoTotal()
    {
        return tempoTotal;
    }

    public float GetTempoFase(int numeroFase)
    {
        if (numeroFase >= 0 && numeroFase < 6)
        {
            return temposPorFase[numeroFase];
        }
        return 0;
    }

    public void PegarFaseAtual()
    {
        faseAtual = SceneManager.GetActiveScene().buildIndex - 1;
    }

    public int CalcularEstrelas(int numeroFase)
    {
        if (numeroFase < 0 || numeroFase >= 6) return 1; // Evita erro

        float tempo = GetTempoFase(numeroFase);

        if (tempo <= 60) return 3; // 3 estrelas
        if (tempo <= 90) return 2; // 2 estrelas
        return 1; // 1 estrela
    }

    public void SubstituirTemposNoWord()
    {
        string caminhoOriginal = Path.Combine(Application.dataPath, "relatorio-word/modelo-relatorio-word.docx");
        string destino = Path.Combine(Application.persistentDataPath, "relatorio-word-" + nomeAluno + ".docx");

        if (!File.Exists(destino))
        {
            File.Copy(caminhoOriginal, destino, true);
        }

        string caminhoArquivo = destino;

        if (!File.Exists(caminhoArquivo))
        {
            Debug.LogError("Arquivo n�o encontrado: " + caminhoArquivo);
            return;
        }

        using (DocX documento = DocX.Load(caminhoArquivo))
        {
            float tempoTotal = GetTempoTotal();
            float tempoMedio = tempoTotal / 6; 
            int pontuacaoGeral = CalcularPontuacaoGeral();

            documento.ReplaceText("#nomeAluno", nomeAluno);
            documento.ReplaceText("#tempoTotal", tempoTotal.ToString("F2") + "s");
            documento.ReplaceText("#tempoMedioFase", tempoMedio.ToString("F2") + "s");
            documento.ReplaceText("#pontuacaoGeral", pontuacaoGeral.ToString());

            for (int i = 0; i < 6; i++)
            {
                string tempoKey = "#tempoFase" + (i + 1);
                string pontosKey = "#pontosFase" + (i + 1);

                documento.ReplaceText(tempoKey, GetTempoFase(i).ToString("F2") + "s");
                documento.ReplaceText(pontosKey, CalcularEstrelas(i).ToString());
            }

            documento.Save();
        }

        Debug.Log("Relat�rio atualizado com sucesso!");
    }

    public int CalcularPontuacaoGeral()
    {
        int totalPontos = 0;
        for (int i = 0; i < 6; i++)
        {
            totalPontos += CalcularEstrelas(i);
        }
        return totalPontos;
    }

    public void EnviarEmail()
    {
        string destinatarioEmail = emailProfessor;
        string assuntoEmail = "Relat�rio de Desempenho do(a) Aluno(a) " + nomeAluno;
        string corpoEmail = "Aqui est� o relat�rio de desempenho.";
        string caminhoArquivoRelatorio = Path.Combine(Application.persistentDataPath, "relatorio-word-" + nomeAluno + ".docx");

        string enderecoEmail = "";
        string senhaEmail = "";

        SmtpClient smtp = new SmtpClient("smtp.mailersend.net")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(enderecoEmail, senhaEmail)
        };

        MailMessage mensagem = new MailMessage();
        mensagem.From = new MailAddress(enderecoEmail);
        mensagem.To.Add(destinatarioEmail);
        mensagem.Subject = assuntoEmail;
        mensagem.Body = corpoEmail;

        if (!string.IsNullOrEmpty(caminhoArquivoRelatorio))
        {
            Attachment anexo = new Attachment(caminhoArquivoRelatorio);
            mensagem.Attachments.Add(anexo);
        }

        try
        {
            // Enviar o e-mail
            smtp.Send(mensagem);
            Debug.Log("E-mail enviado com sucesso!");
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro ao enviar e-mail: " + ex.Message);
        }
    }
}
