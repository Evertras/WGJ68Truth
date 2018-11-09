using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewRequester : MonoBehaviour {
	void Start () {
        int completedCount = PlayerPrefs.GetInt("completedCount", 0);

        if (completedCount >= 2)
        {
            Debug.Log("Asking for platform-specific review");

#if UNITY_IOS
            UnityEngine.iOS.Device.RequestStoreReview();
#endif
        }
    }
}
