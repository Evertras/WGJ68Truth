using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimate : MonoBehaviour {

    public float RotateSpeed = 30.0f;
    public float BobInterval = 1.0f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0.0f, 1.0f), RotateSpeed * Time.deltaTime);
	}
}
