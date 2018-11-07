using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

[RequireComponent(typeof(AdPlayer))]
public class MenuActions : MonoBehaviour {
    AdPlayer adPlayer;

    public void Start()
    {
        Cursor.visible = true;
        adPlayer = GetComponent<AdPlayer>();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play(int size)
    {
        int adCounter = PlayerPrefs.GetInt("adCounter", 0);

        PlayerPrefs.SetInt("size", size);

        adCounter += size;

        if (adCounter >= 20)
        {
            adCounter -= 20;

            adPlayer.ShowRegular();
        }

        PlayerPrefs.SetInt("adCounter", adCounter);

        AnalyticsEvent.LevelStart(size);

        SceneManager.LoadScene("Play");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
