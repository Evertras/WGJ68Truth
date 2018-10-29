using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdHotOrbit : MonoBehaviour {
    public GameObject Player;
    public GameObject Goal;

    private Renderer Renderer;
    private MaterialPropertyBlock PropBlock;

	// Use this for initialization
	void Start () {
        Renderer = GetComponent<Renderer>();
        PropBlock = new MaterialPropertyBlock();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = Player.transform.position;

        position += new Vector3(Mathf.Sin(Time.time) * 1, 1.5f, Mathf.Cos(Time.time) * 1);

        transform.position = position;

        var playerToGoal = Goal.transform.position - Player.transform.position;
        var playerToSphere = transform.position - Player.transform.position;

        float angle = Vector3.Angle(playerToGoal, playerToSphere);

        var value = 1.0f - Mathf.Clamp01(angle / 180.0f);

        Renderer.GetPropertyBlock(PropBlock);

        PropBlock.SetColor("_Color", new Color(value, value, value));

        Renderer.SetPropertyBlock(PropBlock);
	}
}
