using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    public int width = 1;
    public int height = 1;

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }


    public List<Vector2Int> GetGridPositionList(Vector2Int offset)
    {       
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridPositionList.Add(offset + new Vector2Int(x, y));
            }
        }

        return gridPositionList;
    }

    
}
