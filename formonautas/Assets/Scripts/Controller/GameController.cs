using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Update()
    {
        
    }

    public void TrocarScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
