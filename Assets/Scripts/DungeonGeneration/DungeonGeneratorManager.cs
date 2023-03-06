using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum GenerationMethod
{
    Fixed, Random
}

public class DungeonGeneratorManager : MonoBehaviour
{
    #region Variables


    [Header("Dungeon Generation Config")]
    [SerializeField] private GenerationMethod _generationMethod;

    [SerializeField] private int _fixedRoomAmount = 8;
    [SerializeField] private int _randomRoomAmountLowerBound = 5;
    [SerializeField] private int _randomRoomAmountUpperBound = 10;

    [SerializeField] private RoomBehaviour _roomBehaviour;

    [Header("Generated Rooms Data")]
    private Vector2Int _currentRoomGenerated = Vector2Int.zero;
    [SerializeField] private List<Vector2Int> _roomPositions = new List<Vector2Int>();
    [SerializeField] private List<RoomInfo> _roomInfos = new List<RoomInfo>();


    #endregion

    #region MonoBehaviour
    private void Start()
    {
        GenerateDungeon();
    }

    #endregion

    #region Methods
    private void GenerateDungeon()
    {
        switch (_generationMethod)
        {
            case GenerationMethod.Fixed:
                GenerateProceduralRooms(_fixedRoomAmount);
                break;
            case GenerationMethod.Random:
                int roomsToCreate = Random.Range(_randomRoomAmountLowerBound, _randomRoomAmountUpperBound + 1);
                GenerateProceduralRooms(roomsToCreate);
                break;
        }

        RearrangeLevel();
    }

    private void GenerateProceduralRooms(int roomsToCreate)
    {
        int roomsCreated = 0;
        while (roomsCreated < roomsToCreate)
        {
            int rand = Random.Range(0, 4); // Four available directions

            if (!_roomPositions.Contains(_currentRoomGenerated))
            {
                var roomInfo = ScriptableObject.CreateInstance<RoomInfo>();
                roomInfo.GridPosition = _currentRoomGenerated;
                roomInfo.RoomSize = _roomBehaviour.RoomSize;
                //roomInfo.RoomSize = new Vector2(5.5f, 5.5f);
                roomInfo.WorldPosition = roomInfo.GridPosition * roomInfo.RoomSize;
                _roomInfos.Add(roomInfo);

                _roomPositions.Add(_currentRoomGenerated);
                roomsCreated++;
            }

            _currentRoomGenerated += rand switch
            {
                0 => Vector2Int.up,
                1 => Vector2Int.down,
                2 => Vector2Int.right,
                3 => Vector2Int.left,
                _ => Vector2Int.zero,
            };
        }
    }

    private void RearrangeLevel()
    {
        foreach (var roomInfo in _roomInfos)
        {
            roomInfo.CheckAdjacentRooms(_roomPositions);
#if false
            // This could be useful if we want thematic level generation, just in case
            var resource = Resources.Load<RoomBehaviour>("RoomPlaceholder");

            var room = Instantiate(resource, roomInfo.WorldPosition, Quaternion.identity);
#endif
            var room = Instantiate(_roomBehaviour, roomInfo.WorldPosition, Quaternion.identity);
            room.RoomInfo = roomInfo;
            room.SpawnWalls();
            Debug.Log("Instantiated room: " + roomInfo.RoomType);
        }
    }
#endregion
}
