using UnityEngine;

[CreateAssetMenu(menuName = "MyCreate / new Tile State", fileName = "Tile State")]
public class TileState : ScriptableObject
{
    public int number;
    public Color backgroundColor;
    public Color textColor;
}