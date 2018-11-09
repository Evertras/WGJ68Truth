﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent(typeof(AdPlayer))]
public class HintUnlock : MonoBehaviour {
    bool ZoomUnlocked;

    public GameObject ZoomSlider;
    public Button ZoomButton;

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
