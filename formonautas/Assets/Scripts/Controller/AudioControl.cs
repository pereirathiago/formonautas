using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource currentAudioSource;

    public void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        // Verifica se o �udio est� tocando
        if (currentAudioSource != audioSource || !currentAudioSource.isPlaying)
        {
            // Se o AudioSource n�o est� tocando, ou se for o mesmo, mas n�o est� tocando
            currentAudioSource = audioSource;
            audioSource.PlayOneShot(clip);
        }
    }

    public void PararAudio()
    {
        currentAudioSource.Stop();
    }
}
