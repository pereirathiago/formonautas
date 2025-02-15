using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncontrarController : MonoBehaviour
{
    [SerializeField] private List<GameObject> etapasDesenho;
    [SerializeField] private GameObject etapaAtualDesenho;

    private FaseController faseController;

    // Start is called before the first frame update
    void Start()
    {
        faseController = FindObjectOfType<FaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlterarEtapaDesenho()
    {
        etapaAtualDesenho.SetActive(false);
        etapaAtualDesenho = etapasDesenho[etapasDesenho.IndexOf(etapaAtualDesenho) + 1];
        etapaAtualDesenho.SetActive(true);
    }
}
