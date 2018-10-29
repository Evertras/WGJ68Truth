using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject Menu;

    private bool paused;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Pause()
    {
        Time.timeScale = 0.0f;
        Menu.SetActive(true);
        paused = true;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Menu.SetActive(false);
        paused = false;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
