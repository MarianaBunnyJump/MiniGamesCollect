using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows;

    public TileCell[] cells;

    //16
    public int size => cells.Length;

    //4列
    public int height => rows.Length;

    //4
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();

        //Debug.Log($"{size},{height},{width}");
        //x(0,0)为左上角的点
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].coordinates = new Vector2Int(i % width, i / width);
            //Debug.Log($"元素{i}的坐标为:{cells[i].coordinates}");
            //0,0，1,0，2,0，3,0
            //0,1，1,1，2,1，3,1
            //0,2，1,2，2,2，3,2
            //0,3，1,3，2,3，3,3
        }
    }

    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }
    }


    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].occupied)
        {
            index++;
            if (index >= cells.Length)
            {
                index = 0;
            }

            // all cells are occupied
            if (index == startingIndex)
            {
                return null;
            }
        }

        return cells[index];
    }
}