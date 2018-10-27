using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MonoBehaviour {

    public Material Material;

    public bool ColorizePassable = false;

    private float Probability;
    private Color WallColor;
    private Renderer Renderer;
    private MaterialPropertyBlock PropBlock;
    private bool IsPassable;

	// Use this for initialization
	void Start () {
        Renderer = GetComponent<Renderer>();
        PropBlock = new MaterialPropertyBlock();
	}
	
	// Update is called once per frame
	void Update () {
        Renderer.GetPropertyBlock(PropBlock);

        PropBlock.SetColor("_Color", WallColor);

        Renderer.SetPropertyBlock(PropBlock);
	}

    public void ForcePassable(bool passable)
    {
        IsPassable = passable;

        GetComponent<BoxCollider>().enabled = !IsPassable;
    }

    public void SetProbability(float probability)
    {
        Probability = probability;

        if (Random.Range(0.0f, 1.0f) <= Probability)
        {
            IsPassable = true;
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            IsPassable = false;
        }

        if (ColorizePassable)
        {
            if (!IsPassable)
            {
                WallColor = new Color(1.0f, 0.0f, 0.0f);
            }
            else
            {
                WallColor = new Color(0.0f, 1.0f, 0.0f);
            }
        }
        else
        {
            WallColor = new Color(1.0f - Probability, Probability, 0.0f);
        }
    }
}
