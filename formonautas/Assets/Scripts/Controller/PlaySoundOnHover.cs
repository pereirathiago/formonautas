using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioManager audioManager;

    private void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Chama o gerenciador de áudio para tocar o som
        audioManager.PlaySound(audioSource, clip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        audioManager.PararAudio();
    }
}
