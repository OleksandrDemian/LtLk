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
                map[x, y] = new Node(true);
            }
        }
    }

    public List<Node> GetAdiacentNodes(int x, int y)
    {
        List<Node> adiacentNodes = new List<Node>();
        for (int cY = y - 1; cY < y + 2; cY++)
        {
            for (int cX = x - 1; cX < x + 2; cX++)
            {
                if (IsPointValid(cX, cY))
                {
                    adiacentNodes.Add(GetNode(cX, cY));
                }
            }
        }
        return adiacentNodes;
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
                GameObject ground = ObjectPool.Get("Cube");
                MonoBehaviour.Instantiate(ground, new Vector3(x, Random.Range(0f, 0.1f), y), Quaternion.identity);
            }
        }
    }
}
