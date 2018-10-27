using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGet : MonoBehaviour {
    public GameObject Fade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }

        Fade.SetActive(true);
    }
}
