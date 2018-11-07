using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Pillar;
    public GameObject Goal;
    public GameObject HotCold;
    public GameObject[] LandmarkProps;

    [Range(0.0f, 1.0f)]
    public float LandmarkChance = 0.5f;

    public bool StaticSize = false;
    public int Width = 5;
    public int Height = 5;
    public float WallHeight = 2.0f;

    [Range(4, 20)]
    public int CellSize = 5;

    private float WallLength;
    private Vector3 Offset;

    private void Start()
    {
        if (!StaticSize)
        {
            Width = PlayerPrefs.GetInt("size", Width);
            Height = PlayerPrefs.GetInt("size", Height);
        }

        WallLength = CellSize - 3.0f;
        Offset = new Vector3(-CellSize * 0.5f, 0.0f, -CellSize * 0.5f);

        var edges = KruskalMaze.Generate(Width, Height);

        for (int x = 0; x <= Width; ++x)
        {
            for (int z = 0; z <= Height; ++z)
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

                if ((z == 0 || z == Height) && x != 0)
                {
                    var wall = SpawnWallBottom(x, z);

                    wall.Passable = false;
                    wall.SetProbability(0.0f);
                }

                if ((x == 0 || x == Width) && z != 0)
                {
                    var wall = SpawnWallLeft(x, z);
                    wall.Passable = false;
                    wall.SetProbability(0.0f);
                }

                if (
                    z != 0 &&
                    x != Width - 1 &&
                    LandmarkProps.Length > 0 &&
                    Random.Range(0.0f, 1.0f) < LandmarkChance)
                {
                    int propIndex = Random.Range(0, LandmarkProps.Length);

                    Instantiate(
                        LandmarkProps[propIndex],
                        new Vector3(
                            pillar.transform.position.x + 1.5f,
                            0.0f,
                            pillar.transform.position.z - 1.5f),
                        Quaternion.identity);
                }
            }
        }

        foreach (var pair in edges)
        {
            var edge = pair.Key;
            var passable = pair.Value;
            MazeWall wall;

            if (edge.A.X == edge.B.X)
            {
                wall = SpawnWallBottom(edge.A.X + 1, edge.A.Y + 1);
            }
            else
            {
                wall = SpawnWallLeft(edge.A.X + 1, edge.A.Y + 1);
            }

            wall.Passable = passable;
            wall.SetProbability(Probability(passable));
        }

        var goal = Instantiate(
            Goal,
            new Vector3(
                CellSize * (Width - 1),
                1.0f,
                CellSize * (Height - 1)
            ),
            Quaternion.identity);

        if (HotCold != null)
        {
            HotCold.GetComponent<ColdHotOrbit>().Goal = goal;
        }
    }

    private float Probability(bool passable)
    {
        float prob =
                Random.Range(0.0f, 1.0f)
                * Random.Range(0.0f, 0.8f);

        prob = prob + 0.1f;

        if (passable)
        {
            prob = 1.0f - prob;
        }

        return prob;
    }

    private MazeWall SpawnWallLeft(int x, int z)
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

        return wall.GetComponent<MazeWall>();
    }

    private MazeWall SpawnWallBottom(int x, int z)
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

        return wall.GetComponent<MazeWall>();
    }
}
