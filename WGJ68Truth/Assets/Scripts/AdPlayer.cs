using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdPlayer : MonoBehaviour {
    public void Show()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();

        options.finishCallback = HandleResult;

        ShowAdPlacementContent ad = Monetization.GetPlacementContent("rewardedVideo") as ShowAdPlacementContent;

        ad.Show(options);
    }

    void HandleResult(ShowResult result) {
        Debug.Log(result);
    }
}
