using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }

    public TileBase tileUnknown;
    public TileBase tileEmpty;
    public TileBase tileMine;
    public TileBase tileExploded;
    public TileBase tileFlag;
    public TileBase tileNum1;
    public TileBase tileNum2;
    public TileBase tileNum3;
    public TileBase tileNum4;
    public TileBase tileNum5;
    public TileBase tileNum6;
    public TileBase tileNum7;
    public TileBase tileNum8;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Draw(CellGrid grid)
    {
        int width = grid.Width;
        int height = grid.Height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];
                tilemap.SetTile(cell.position, GetTile(cell));
            }
        }
    }

    private TileBase GetTile(Cell cell)
    {
        if (cell.revealed)
        {
            return GetRevealedTile(cell);
        }
        else if (cell.flagged)
        {
            return tileFlag;
        }
        else if (cell.chorded)
        {
            return tileEmpty;
        }
        else
        {
            return tileUnknown;
        }
    }

    private TileBase GetRevealedTile(Cell cell)
    {
        switch (cell.type)
        {
            case Cell.Type.Empty: return tileEmpty;
            case Cell.Type.Mine: return cell.exploded ? tileExploded : tileMine;
            case Cell.Type.Number: return GetNumberTile(cell);
            default: return null;
        }
    }

    private TileBase GetNumberTile(Cell cell)
    {
        switch (cell.number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 4: return tileNum4;
            case 5: return tileNum5;
            case 6: return tileNum6;
            case 7: return tileNum7;
            case 8: return tileNum8;
            default: return null;
        }
    }
}