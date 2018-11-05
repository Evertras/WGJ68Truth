using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {
    public void Start()
    {
        Cursor.visible = true;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play(int size)
    {
        PlayerPrefs.SetInt("size", size);
        SceneManager.LoadScene("Play");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
