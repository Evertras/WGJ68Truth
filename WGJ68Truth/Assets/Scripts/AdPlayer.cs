using System.Collections;
using UnityEngine;
using UnityEngine.Monetization;

public class AdPlayer : MonoBehaviour {
    public void ShowForZoom()
    {
        StartCoroutine(ShowWhenReady("rewardedVideo", ZoomBoost));
    }

    public void ShowRegular(ShowAdFinishCallback cb = null)
    {
        StartCoroutine(ShowWhenReady("video", cb));
    }

    IEnumerator ShowWhenReady(string placementId, ShowAdFinishCallback cb)
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdCallbacks options = new ShowAdCallbacks();

        if (cb != null)
        {
            options.finishCallback = cb;
        }

        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(options);
        }
    }

    void ZoomBoost(ShowResult result) {
        Debug.Log("Do zoom boost things");
    }
}
