using System;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Empty, Spawn, Shop, EasyEnemies, MediumEnemies, HardEnemies, EliteEnemies
}

[Flags]
public enum AdjacentRooms
{
    None = 0,
    North = 1,
    East = 2,
    South = 4,
    West = 8,
}

[CreateAssetMenu(menuName = "ScriptableObjects/RoomInfo", fileName = "RoomInfoSO")]
public class RoomInfo : ScriptableObject
{
    public Vector2Int GridPosition;
    public Vector2 RoomSize;
    public Vector2 WorldPosition;

    public RoomType RoomType;
    public AdjacentRooms AdjacentRooms;

    public void CheckAdjacentRooms(in List<Vector2Int> gridPositions)
    {
        if (gridPositions.Contains(GridPosition + Vector2Int.up))
            AdjacentRooms |= AdjacentRooms.North;

        if (gridPositions.Contains(GridPosition + Vector2Int.down))
            AdjacentRooms |= AdjacentRooms.South;

        if (gridPositions.Contains(GridPosition + Vector2Int.right))
            AdjacentRooms |= AdjacentRooms.East;

        if (gridPositions.Contains(GridPosition + Vector2Int.left))
            AdjacentRooms |= AdjacentRooms.West;
    }
}
