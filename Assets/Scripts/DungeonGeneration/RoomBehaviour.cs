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
            Instantiate(Wall, WallSpawns[0]);

        if ((RoomInfo.AdjacentRooms & AdjacentRooms.South) == 0)
            Instantiate(Wall, WallSpawns[1]);

        if ((RoomInfo.AdjacentRooms & AdjacentRooms.East) == 0)
            Instantiate(Wall, WallSpawns[2]);

        if ((RoomInfo.AdjacentRooms & AdjacentRooms.West) == 0)
            Instantiate(Wall, WallSpawns[3]);
    }

    public void SetupRoom()
    {
        switch (RoomInfo.RoomType)
        {
            case RoomType.Shop:
                SetupShopRoom();
                break;
            case RoomType.EasyEnemies:
            case RoomType.MediumEnemies:
            case RoomType.HardEnemies:
                SetupEnemiesRoom();
                break;
            case RoomType.EliteEnemies:
                SetupEliteRoom();
                break;
        }
    }

    private void SetupShopRoom()
    {
        // This will generate the shop inside the room
    }

    private void SetupEnemiesRoom()
    {
        // This will generate enemies inside the room
    }

    private void SetupEliteRoom()
    {
        // This will generate elite enemies inside the room
    }
}
