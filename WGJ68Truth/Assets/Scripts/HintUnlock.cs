using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

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
            PathfindersUnlocked = true;
            HideScreen();
            PlayerPrefs.SetInt("adCounter", 0);
            PathfinderButton.interactable = false;
            PathfinderSpawner.SetActive(true);
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
