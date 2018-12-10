using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using UnityEngine.Analytics;
using System.Collections.Generic;

[RequireComponent(typeof(AdPlayer))]
public class HintUnlock : MonoBehaviour {
    bool ZoomUnlocked = false;
    bool PathfindersUnlocked = false;

    public GameObject ZoomSlider;
    public GameObject PathfinderSpawner;

    public Button ZoomButton;
    public Button PathfinderButton;

    AdPlayer AdPlayer;

	void Start ()
    {
        AdPlayer = GetComponent<AdPlayer>();
	}

    public void UnlockZoom()
    {
        if (ZoomUnlocked)
        {
            return;
        }

        AdPlayer.ShowRewarded((ShowResult res) =>
        {
            if (res == ShowResult.Finished)
            {
                ZoomUnlocked = true;
                ZoomSlider.SetActive(true);
                ZoomButton.interactable = false;
                HideScreen();
                PlayerPrefs.SetInt("adCounter", 0);

                Analytics.CustomEvent("unlockZoom", new Dictionary<string, object> {
                    { "size", PlayerPrefs.GetInt("size") }
                });
            }
        });
    }

    public void UnlockPathfinders()
    {
        if (PathfindersUnlocked)
        {
            return;
        }

        AdPlayer.ShowRewarded((ShowResult res) =>
        {
            if (res == ShowResult.Finished)
            {
                PathfindersUnlocked = true;
                HideScreen();
                PlayerPrefs.SetInt("adCounter", 0);
                PathfinderButton.interactable = false;
                PathfinderSpawner.SetActive(true);

                Analytics.CustomEvent("unlockPathfinders", new Dictionary<string, object> {
                    { "size", PlayerPrefs.GetInt("size") }
                });
            }
        });
    }

    public void ShowScreen()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
