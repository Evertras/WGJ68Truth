using System;
using System.Collections;
using System.Collections.Generic;

public class Cell : IEquatable<Cell>
{
    public int X;
    public int Y;

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !GetType().Equals(obj.GetType()))
        {
            return false;
        }

        Cell c = (Cell)obj;

        return X == c.X && Y == c.Y;
    }

    public bool Equals(Cell other)
    {
        return other != null &&
               X == other.X &&
               Y == other.Y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }
}

public class Edge : IEquatable<Edge>
{
    public Cell A;
    public Cell B;

    public Edge(Cell a, Cell b)
    {
        A = a;
        B = b;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType().Equals(obj.GetType()))
        {
            return false;
        }

        Edge e = (Edge)obj;
        return
            A == e.A &&
            B == e.B;
    }

    public bool Equals(Edge other)
    {
        return other != null &&
               EqualityComparer<Cell>.Default.Equals(A, other.A) &&
               EqualityComparer<Cell>.Default.Equals(B, other.B);
    }

    public override int GetHashCode()
    {
        var hashCode = 2118541809;
        hashCode = hashCode * -1521134295 + EqualityComparer<Cell>.Default.GetHashCode(A);
        hashCode = hashCode * -1521134295 + EqualityComparer<Cell>.Default.GetHashCode(B);
        return hashCode;
    }
}

public static class KruskalMaze
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static IEnumerable<KeyValuePair<Edge, bool>> Generate(int width, int height)
    {
        int edgeCount = width * height - (width + height);
        List<HashSet<Cell>> cellSets = new List<HashSet<Cell>>();
        List<Edge> allEdges = new List<Edge>(edgeCount);
        Dictionary<Edge, bool> mazeEdges = new Dictionary<Edge, bool>();

        // Fill the edges bag with all possible permutations
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                Cell cell = new Cell(x, y);

                HashSet<Cell> cellSet = new HashSet<Cell>
                {
                    cell
                };

                cellSets.Add(cellSet);

                // Add right if we're not already all the way to the right
                if (x < width - 1)
                {
                    allEdges.Add(new Edge(
                        cell,
                        new Cell(x + 1, y)
                        ));
                }

                // Add bottom if we're not already all the way to the bottom
                if (y < height - 1)
                {
                    allEdges.Add(new Edge(
                        cell,
                        new Cell(x, y + 1)
                        ));
                }
            }
        }

        // The basic Kruskal algorithm is looking for a minimum spanning tree, we're looking
        // for an actual random maze, so we randomize the list before picking from it
        allEdges.Shuffle();

        // For each edge in edges, check if it connects two disjointed sets... if yes,
        // combine the sets mark edge as passable; if not, mark edge as impassable
        foreach (var edge in allEdges)
        {
            var setA = cellSets.Find(s => s.Contains(edge.A));

            if (setA.Contains(edge.B))
            {
                mazeEdges[edge] = false;
                continue;
            }

            mazeEdges[edge] = true;

            var setB = cellSets.Find(s => s.Contains(edge.B));

            // Combine
            setA.UnionWith(setB);
            cellSets.Remove(setB);
        }

        // Return the remaining edges in the list
        return mazeEdges;
    }
}
