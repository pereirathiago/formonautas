using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseBtnUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseBtnUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;

        GameObject professorAudioObj = GameObject.FindGameObjectWithTag("ProfessorAudio");
        if (professorAudioObj != null)
        {
            AudioSource audioSource = professorAudioObj.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.UnPause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseBtnUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;

        GameObject professorAudioObj = GameObject.FindGameObjectWithTag("ProfessorAudio");
        if (professorAudioObj != null)
        {
            AudioSource audioSource = professorAudioObj.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
