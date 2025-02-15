using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource currentAudioSource;

    public void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        // Verifica se o áudio está tocando
        if (currentAudioSource != audioSource || !currentAudioSource.isPlaying)
        {
            // Se o AudioSource não está tocando, ou se for o mesmo, mas não está tocando
            currentAudioSource = audioSource;
            audioSource.PlayOneShot(clip);
        }
    }

    public void PararAudio()
    {
        currentAudioSource.Stop();
    }
}
