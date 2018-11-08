using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

    public float RotateSpeed = 15f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, RotateSpeed * Time.deltaTime, 0f, Space.World);
	}
}
