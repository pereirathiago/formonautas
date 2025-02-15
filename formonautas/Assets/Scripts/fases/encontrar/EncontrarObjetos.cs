using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncontrarObjetos : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteObjs;
    [SerializeField] private SpriteRenderer spriteRendererObjs;

    private FaseController faseController;

    // Start is called before the first frame update
    void Awake()
    {
        faseController = FindObjectOfType<FaseController>();
        faseController.ObjetosEncontrar.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        EncontrarObjeto();
    }

    public void EncontrarObjeto()
    {
        if (!faseController.ObjetosEncontrados.Contains(gameObject))
        {
            faseController.ObjetosEncontrados.Add(gameObject);
            spriteRendererObjs.sprite = spriteObjs;
            spriteRendererObjs.gameObject.transform.localScale = new Vector3(0.08900759f, 0.02820622f, 0.2555764f);
        }
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = new Color32(255, 12, 0, 255);
    }
}
