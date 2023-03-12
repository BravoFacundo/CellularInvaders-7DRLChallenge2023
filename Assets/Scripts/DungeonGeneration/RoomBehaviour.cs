using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [Header("Room Layout")]
    [SerializeField] private GameObject[] possibleRoomLayouts;
    private GameObject _roomLayout;

    [Header("Room Information")]
    public RoomInfo RoomInfo;
    public GameObject DoorParent;
    public GameObject WallParent;
    public Vector2 RoomSize = new Vector2(10, 10);

    private void Awake()
    {
        if (possibleRoomLayouts.Length == 0)
        {
            Debug.LogError("This room has no available layouts to select", gameObject);
            return;
        }

        foreach (var layout in possibleRoomLayouts)
            layout.SetActive(false);

        int index = Random.Range(0, possibleRoomLayouts.Length);
        _roomLayout = possibleRoomLayouts[index];
        _roomLayout.SetActive(true);
    }

    public void SpawnWalls()
    {
        bool northAdjacent = (RoomInfo.AdjacentRooms & AdjacentRooms.North) == 0;
        WallParent.transform.GetChild(0).gameObject.SetActive(northAdjacent);
        DoorParent.transform.GetChild(0).gameObject.SetActive(!northAdjacent);

        bool southAdjacent = (RoomInfo.AdjacentRooms & AdjacentRooms.South) == 0;
        WallParent.transform.GetChild(1).gameObject.SetActive(southAdjacent);
        DoorParent.transform.GetChild(1).gameObject.SetActive(!southAdjacent);

        bool eastAdjacent = (RoomInfo.AdjacentRooms & AdjacentRooms.East) == 0;
        WallParent.transform.GetChild(2).gameObject.SetActive(eastAdjacent);
        DoorParent.transform.GetChild(2).gameObject.SetActive(!eastAdjacent);

        bool westAdjacent = (RoomInfo.AdjacentRooms & AdjacentRooms.West) == 0;
        WallParent.transform.GetChild(3).gameObject.SetActive(westAdjacent);
        DoorParent.transform.GetChild(3).gameObject.SetActive(!westAdjacent);
    }

    public void SetupRoom()
    {
        switch (RoomInfo.RoomType)
        {
            case RoomType.Spawn:
                SetupSpawnRoom();
                break;
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

        ActivateRoomLayout();
    }

    private void SetupSpawnRoom()
    {
        _roomLayout.SetActive(false); // We don't want enemies on the spawning room
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

    private void ActivateRoomLayout()
    {
        // A dummy behaviour pattern for setting up the room layout
        _roomLayout.transform.RotateAroundLocal(Vector3.forward, Random.Range(0.0f, 359.9f));
    }
}
