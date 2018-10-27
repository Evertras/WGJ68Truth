using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MonoBehaviour {

    public Material Material;

    public bool ColorizePassable = false;

    public bool Passable {
        get { return _passable; }
        set {
            _passable = value;
            GetComponent<BoxCollider>().enabled = !_passable;

            Colorize();
        }
    }

    private bool _passable;

    private float Probability;
    private Color WallColor;
    private Renderer Renderer;
    private MaterialPropertyBlock PropBlock;

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

    public void SetProbability(float probability)
    {
        Probability = probability;

        Passable = Random.Range(0.0f, 1.0f) < probability;

        Colorize();
    }

    private void Colorize()
    {
        if (ColorizePassable)
        {
            if (!_passable)
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
