using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class ZoomableCamera : MonoBehaviour {
    private CinemachineFramingTransposer transposer;

	// Use this for initialization
	void Start () {
        var camera = GetComponent<CinemachineVirtualCamera>();
        transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (transposer == null)
        {
            Debug.LogError("Could not find transposer on camera object");
        }
	}

    public void SetCameraDistance(float distance)
    {
        transposer.m_CameraDistance = distance; 
    }
}
