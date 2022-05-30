using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentStats : MonoBehaviour
{
    //Atributes for placing something on the grid
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

    //Creat a list of all vectors/cells that we would occupy on the grid
    public List<Vector2Int> GetGridPositionList(Vector2Int offset)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        //run through the width and height
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
