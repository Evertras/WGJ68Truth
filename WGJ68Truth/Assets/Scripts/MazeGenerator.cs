using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
    public GameObject Wall;
    public GameObject Pillar;

    public int Width = 5;
    public int Height = 5;
    public float WallHeight = 2.0f;
    [Range(4, 20)]
    public int CellSize = 5;

    private float WallLength;
    private Vector3 Offset;


	// Use this for initialization
	void Start () {
        WallLength = CellSize - 3.0f;
        Offset = new Vector3(-CellSize * 0.5f, 0.0f, -CellSize * 0.5f);

        for (int x = 0; x < Width; ++x)
        {
            for (int z = 0; z < Height; ++z)
            {
                var pillar = Instantiate(
                    Pillar,
                    new Vector3(
                        x * CellSize,
                        0.5f * WallHeight,
                        z * CellSize
                    ) + Offset,
                    Quaternion.identity);

                pillar.transform.localScale = new Vector3(1.0f, WallHeight, 1.0f);

                if (x != 0)
                {
                    SpawnWallLeft(x, z);
                }

                if (z != 0)
                {
                    SpawnWallBottom(x, z);
                }
            }
        }
	}

    private void SpawnWallBottom(int x, int z)
    {
        var wall = Instantiate(
            Wall,
            new Vector3(
                x * CellSize,
                0.5f * WallHeight,
                z * CellSize - (WallLength + 0.5f)
            ) + Offset,
            Quaternion.identity);

        wall.transform.localScale = new Vector3(1.0f, WallHeight, WallLength);

        if (x == 0 || x == Width - 1)
        {
            wall.GetComponent<MazeWall>().SetProbability(0.0f);
        }
        else
        {
            wall.GetComponent<MazeWall>().SetProbability(Random.Range(0.5f, 1.0f));
        }
    }

    private void SpawnWallLeft(int x, int z)
    {
        var wall = Instantiate(
            Wall,
            new Vector3(
                x * CellSize - (WallLength + 0.5f),
                0.5f * WallHeight,
                z * CellSize
            ) + Offset,
            Quaternion.identity);

        wall.transform.localScale = new Vector3(WallLength, WallHeight, 1.0f);

        if (z == 0 || z == Height - 1)
        {
            wall.GetComponent<MazeWall>().SetProbability(0.0f);
        }
        else
        {
            wall.GetComponent<MazeWall>().SetProbability(Random.Range(0.5f, 1.0f));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
