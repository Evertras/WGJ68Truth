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

	// Use this for initialization
	void Start () {
        float wallLength = CellSize - 3.0f;
        Vector3 offset = new Vector3(-CellSize * 0.5f, 0.0f, -CellSize * 0.5f);

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
                    ) + offset,
                    Quaternion.identity);

                pillar.transform.localScale = new Vector3(1.0f, WallHeight, 1.0f);

                if (x != 0)
                {
                    var wall = Instantiate(
                        Wall,
                        new Vector3(
                            x * CellSize - (wallLength + 0.5f),
                            0.5f * WallHeight,
                            z * CellSize
                        ) + offset,
                        Quaternion.identity);

                    wall.transform.localScale = new Vector3(wallLength, WallHeight, 1.0f);
                }

                if (z != 0)
                {
                    var wall = Instantiate(
                        Wall,
                        new Vector3(
                            x * CellSize,
                            0.5f * WallHeight,
                            z * CellSize - (wallLength + 0.5f)
                        ) + offset,
                        Quaternion.identity);

                    wall.transform.localScale = new Vector3(1.0f, WallHeight, wallLength);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
