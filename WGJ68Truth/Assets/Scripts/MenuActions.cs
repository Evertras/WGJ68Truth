using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

[RequireComponent(typeof(AdPlayer))]
public class MenuActions : MonoBehaviour {
    AdPlayer adPlayer;

    public Slider LoadingBar;
    public GameObject MenuContainer;
    public GameObject LoadingBarContainer;

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

        if (adCounter >= 40)
        {
            adCounter -= 40;

            adPlayer.ShowRegular();
        }

        PlayerPrefs.SetInt("adCounter", adCounter);

        AnalyticsEvent.LevelStart(size);

        if (LoadingBar != null)
        {
            LoadingBarContainer.SetActive(true);
            MenuContainer.SetActive(false);

            StartCoroutine(StartAsync(size));
        }
        else
        {
            SceneManager.LoadScene("Play");
        }
    }

    IEnumerator StartAsync(int size)
    {
        var progress = SceneManager.LoadSceneAsync("Play");

        LoadingBar.minValue = 0;
        LoadingBar.maxValue = 0.9f; // Scene will be swapped in after 0.9, so measure up until there
        LoadingBar.value = 0;

        while (!progress.isDone)
        {
            LoadingBar.value = progress.progress;

            Debug.Log(progress.progress);

            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
