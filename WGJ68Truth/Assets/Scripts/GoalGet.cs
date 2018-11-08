using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GoalGet : MonoBehaviour {
    public GameObject Fade;
    public AudioSource WinSound;
    public PlayableDirector Timeline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }

        WinSound.Play();
        Timeline.Play();

        StartCoroutine(FadeOut(3f));

        AnalyticsEvent.LevelComplete(PlayerPrefs.GetInt("size", -1));
    }

    IEnumerator FadeOut(float time)
    {
        yield return new WaitForSeconds(time);
        Fade.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
