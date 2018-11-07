using UnityEngine;
using UnityEngine.Monetization;

public class AdInitializer : MonoBehaviour {

    public bool TestMode = true;

#if UNITY_IOS
    const string gameID = "2897106";
#elif UNITY_ANDROID
    const string gameID = "2897105";
#endif

    void Start () {
        if (!Monetization.isInitialized && Monetization.isSupported)
        {
            Monetization.Initialize(gameID, TestMode);
        }
	}
}
