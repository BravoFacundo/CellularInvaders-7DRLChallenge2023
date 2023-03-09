using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GenerationMethod
{
    Fixed, Random
}

[Serializable]
public class RoomProbability
{
    [SerializeField] public RoomType RoomType;
    [SerializeField] [Range(0, 100)] public float Probability;
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
    [SerializeField] [Range(0, 100)] private float _shopProbability = 10f;
    [SerializeField] private List<RoomProbability> _roomProbabilities = new List<RoomProbability>();
    private float _accumulatedProbability = 0.0f;

    [Header("Generated Rooms Data")]
    private Vector2Int _currentRoomGenerated = Vector2Int.zero;
    [SerializeField] private List<Vector2Int> _roomPositions = new List<Vector2Int>();
    [SerializeField] private List<RoomInfo> _roomInfos = new List<RoomInfo>();

    [SerializeField] private bool _allowShopSpawn = true;
    private bool _shopSpawned = false;


    #endregion

    #region MonoBehaviour
    private void Start()
    {
        foreach (var probability in _roomProbabilities.Select(e => e.Probability))
            _accumulatedProbability += probability;

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
                roomInfo.RoomType = SelectRandomRoomType(roomsCreated, roomsToCreate);
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
            room.SetupRoom();
            Debug.Log("Instantiated room: " + roomInfo.RoomType);
        }
    }

    private RoomType SelectRandomRoomType(int roomsCreated, int roomsToCreate)
    {
        RoomType SelectAnotherRoomTypeLeft()
        {
            if (!_shopSpawned && _allowShopSpawn)
            {
                float rndShop = Random.value;
                if (rndShop <= _shopProbability / 100f) // Shop probability ranges from 0 to 100
                {
                    _shopSpawned = true;
                    return RoomType.Shop;
                }
            }
            
            float rnd = Random.Range(0.0f, _accumulatedProbability);
            float accum = 0f;
            foreach (var element in _roomProbabilities)
            {
                if (rnd < accum + element.Probability)
                    return element.RoomType;
                else
                    accum += element.Probability;
            }

            Debug.LogWarning("Selecting an empty room type. This should not happen and may indicate probabilities are wrong");
            return RoomType.Empty;
        }

        if (roomsCreated == 0) return RoomType.Spawn;
        else if (roomsCreated == roomsToCreate - 1) return RoomType.EliteEnemies;
        else return SelectAnotherRoomTypeLeft();
    }


    #endregion
}
