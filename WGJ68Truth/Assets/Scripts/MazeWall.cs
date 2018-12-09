using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MonoBehaviour {

    public Material Material;
    public GameObject PuffEffect;

    public bool ColorizePassable = false;

    private const int layerPassable = 0;
    private const int layerImpassable = 10;

    public bool Passable {
        get { return _passable; }
        set {
            _passable = value;
            GetComponent<BoxCollider>().isTrigger = _passable;
            Colorize();
            gameObject.layer = _passable ? layerPassable : layerImpassable;
        }
    }

    private bool _passable;

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
            if (Random.Range(0f, 1f) < 0.5f)
            {
                WallColor = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                if (_passable)
                {
                    WallColor = new Color(1f, 1f, 1f);
                }
                else
                {
                    WallColor = new Color(0f, 0f, 0f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(PuffEffect, other.ClosestPoint(transform.position) + transform.up, Quaternion.identity);
    }

    private void OnTriggerExit(Collider other)
    {
        Instantiate(PuffEffect, other.ClosestPoint(transform.position) + transform.up, Quaternion.identity);
    }
}
