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
        //ElaborateLandscape();
    }
    
    public MapManager(Texture2D map)
    {
        Instance = this;
        this.map = new Node[map.width, map.height];
        DrawMap(map);
    }

    private Color ParseColor(Color wrongColor)
    {
        int r = wrongColor.r > .5f ? 1 : 0;
        int g = wrongColor.g > .5f ? 1 : 0;
        int b = wrongColor.b > .5f ? 1 : 0;
        int a = wrongColor.a > .5f ? 1 : 0;

        return new Color(r, g, b, a);
    }

    private void DrawMap(Texture2D data)
    {
        for (int y = 0; y < data.height; y++)
        {
            for (int x = 0; x < data.width; x++)
            {
                Color color = ParseColor(data.GetPixel(x, y));

                if (color.Equals(Color.black))
                {
                    map[x, y] = new Node(LandscapeType.MOUNTAIN);
                }
                else if (color.Equals(Color.blue))
                {
                    map[x, y] = new Node(LandscapeType.FOREST);
                }
                else if (color.Equals(Color.green))
                {
                    map[x, y] = new Node(LandscapeType.FIELD);
                }
                else
                {
                    map[x, y] = new Node(LandscapeType.MOUNTAIN);
                }
            }
        }
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

                map[x, y] = new Node();

                if (type < 15)
                {
                    map[x, y].SetLandscape(LandscapeType.MOUNTAIN);
                }
                else if (type < 35)
                {
                    map[x, y].SetLandscape(LandscapeType.FOREST);
                }
                else
                {
                    map[x, y].SetLandscape(LandscapeType.FIELD);
                }
            }
        }
    }
    
    /*
    private void ElaborateLandscape()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                List<Node> nodes = GetAllAdjacentNodes(x, y);
                int field = 0;
                int forest = 0;
                int mountain = 0;

                CountNearbyLandscape(nodes, ref field, ref forest, ref mountain);

                if (forest > 3)
                    map[x, y].SetLandscape(LandscapeType.FOREST);
                else if(mountain > 2)
                    map[x, y].SetLandscape(LandscapeType.MOUNTAIN);
                else
                    map[x, y].SetLandscape(LandscapeType.FIELD);
            }
        }
    }

    public void CountNearbyLandscape(List<Node> nodes, ref int field, ref int forest, ref int mountain)
    {
        int size = nodes.Count;
        for (int i = 0; i < size; i++)
        {
            if (nodes[i].GetLandScape() == LandscapeType.FIELD)
                field++;
            if (nodes[i].GetLandScape() == LandscapeType.FOREST)
                forest++;
            if (nodes[i].GetLandScape() == LandscapeType.MOUNTAIN)
                mountain++;
        }
    }
    */
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
