using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [Header("Room Information")]
    public RoomInfo RoomInfo;
    public GameObject Wall;
    public Transform[] WallSpawns;
    public Vector2 RoomSize = new Vector2(10, 10);

    public void SpawnWalls()
    {
        if ((RoomInfo.AdjacentRooms & AdjacentRooms.North) == 0)
        {
            Instantiate(Wall, WallSpawns[0]);
        }
        if ((RoomInfo.AdjacentRooms & AdjacentRooms.South) == 0)
        {
            Instantiate(Wall, WallSpawns[1]);
        }

        if ((RoomInfo.AdjacentRooms & AdjacentRooms.East) == 0)
        {
            Instantiate(Wall, WallSpawns[2]);
        }
        if ((RoomInfo.AdjacentRooms & AdjacentRooms.West) == 0)
        {
            Instantiate(Wall, WallSpawns[3]);
        }
    }
}
