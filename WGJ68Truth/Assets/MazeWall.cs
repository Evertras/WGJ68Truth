﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MonoBehaviour {

    public Material Material;

    private float Probability;
    private Color WallColor;
    private Renderer Renderer;
    private MaterialPropertyBlock PropBlock;
    private bool IsPassable;

	// Use this for initialization
	void Start () {
        Probability = Random.Range(0.0f, 1.0f);
        WallColor = new Color(1.0f - Probability, Probability, 0.0f);
        Renderer = GetComponent<Renderer>();
        PropBlock = new MaterialPropertyBlock();

        if (Random.Range(0.0f, 1.0f) <= Probability)
        {
            IsPassable = true;
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            IsPassable = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Renderer.GetPropertyBlock(PropBlock);

        PropBlock.SetColor("_Color", WallColor);

        Renderer.SetPropertyBlock(PropBlock);
	}
}
