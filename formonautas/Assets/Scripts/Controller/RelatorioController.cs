using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelatorioController : MonoBehaviour
{
    public static RelatorioController instance;

    [SerializeField] private float tempoTotal;
    [SerializeField] private float[] temposPorFase = new float[6];
    [SerializeField] private int faseAtual = -1;

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
}
