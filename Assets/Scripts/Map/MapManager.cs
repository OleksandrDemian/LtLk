using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private Node[,] map;

    public static MapManager Instance
    {
        get;
        private set;
    }

    public MapManager()
    {
        Instance = this;
        CreateNodes(5, 5);
    }

    public MapManager(int width, int height)
    {
        Instance = this;
        CreateNodes(width, height);
    }

    public bool IsWalkable(int x, int y)
    {
        if (!IsPointValid(x, y))
            return false;

        return map[x, y].IsWalkable;
    }

    public Node GetNode(int x, int y)
    {
        if (IsPointValid(x, y))
            return map[x, y];

        return null;
    }

    private bool IsPointValid(int x, int y)
    {
        if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1))
            return false;

        return true;
    }

    private void CreateNodes(int width, int height)
    {
        map = new Node[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int type = Random.Range(0, 100);

                map[x, y] = new Node(true);
                map[x, y].SetLandscape((type < 70) ? LandscapeType.FIELD : LandscapeType.FOREST);
            }
        }
    }

    public List<Node> GetAllAdjacentNodes(int x, int y)
    {
        return GetAllAdjacentNodes(x, y, 1);
    }

    public List<Node> GetAllAdjacentNodes(int x, int y, int radius)
    {
        List<Node> adjacentNodes = new List<Node>();
        for (int cY = y - radius; cY <= y + radius; cY++)
        {
            for (int cX = x - radius; cX <= x + radius; cX++)
            {
                if (IsPointValid(cX, cY))
                {
                    adjacentNodes.Add(GetNode(cX, cY));
                }
            }
        }
        return adjacentNodes;
    }

    public List<Node> GetDirectlyAdjacentNodes(int x, int y)
    {
        List<Node> adjacentNodes = new List<Node>();

        if (IsPointValid(x, y + 1))
        {
            adjacentNodes.Add(GetNode(x, y+1));
        }
        if (IsPointValid(x, y - 1))
        {
            adjacentNodes.Add(GetNode(x, y - 1));
        }
        if (IsPointValid(x + 1, y))
        {
            adjacentNodes.Add(GetNode(x + 1, y));
        }
        if (IsPointValid(x - 1, y))
        {
            adjacentNodes.Add(GetNode(x - 1, y));
        }

        return adjacentNodes;
    }

    public void GenerateMap()
    {
        if (map == null)
            throw new System.Exception("Map was not initialized");

        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject ground = ObjectPool.Get(map[x,y].GetLandScape().ToString() + Random.Range(0, 2));
                ground.transform.position = new Vector3(x, 0, y);
            }
        }
    }
}
