using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncontrarForma : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private FaseController faseController;

    private void Awake()
    {
        faseController = FindObjectOfType<FaseController>();
        faseController.ObjetosEncontrar.Add(gameObject);
    }

    private void OnMouseDown()
    {
        TrocarSprite();
    }

    public void TrocarSprite()
    {
        if (!faseController.ObjetosEncontrados.Contains(gameObject))
            faseController.ObjetosEncontrados.Add(gameObject);
        sprite.color = new Color(Random.value, Random.value, Random.value);
    }
}
