using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MapPosition
{
    public int R;
    public int C;

    public MapPosition(int r_in, int c_in)
    {
        R = r_in;
        C = c_in;
    }

    public bool IsPosition(int r, int c)
    {
        if (R == r && C == c)
        {
            return true;
        }

        return false;
    }
}

public class TempleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] shapes;

    [SerializeField] private GameObject spawnShape;

    [SerializeField] private float gridScale = 6.0f;

    [SerializeField] private float gridOffset = -60.0f;

    [SerializeField] private int minimumDungeonSize = 150;

    private MapGenerator mapGenerator;

    private List<MapPosition> roomChoices = new List<MapPosition>();

    private void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();

        int visitedCellCount = 0;

        bool[,] visitedCells = new bool[mapGenerator.MapRows, mapGenerator.MapColumns];

        GenerateMap(ref visitedCellCount, ref visitedCells);

        roomChoices = mapGenerator.GenerateRoomChoices();

        MapPosition spawnRoomChoice = new MapPosition(-1, -1);

        GenerateSpawnRoom(visitedCells, ref spawnRoomChoice);

        mapGenerator.DisplayMap();

        for (int r = 1; r < mapGenerator.MapRows - 1; r++)
        {
            for (int c = 1; c < mapGenerator.MapColumns - 1; c++)
            {
                string ch = mapGenerator.Map[r, c].ToString();
                int charPos = mapGenerator.BoxCharacters.IndexOf(ch);

                if (charPos < 0 || !visitedCells[r, c])
                {
                    continue;
                }

                if (r == spawnRoomChoice.R && c == spawnRoomChoice.C)
                {
                    Instantiate(spawnShape, new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), shapes[charPos].transform.rotation, transform);
                    continue;
                }

                Instantiate(shapes[charPos], new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), shapes[charPos].transform.rotation, transform);
            }
        }
    }

    private void GenerateMap(ref int visitedCellCount, ref bool[,] visitedCells)
    {
        while (visitedCellCount < minimumDungeonSize && roomChoices.Count < 5)
        {
            Debug.Log("Invalid Temple: " + roomChoices.Count + " spawnable rooms and " + visitedCellCount + " total rooms. Trying again.");

            mapGenerator.InitializeMap();

            visitedCells = mapGenerator.TraverseMap();

            visitedCellCount = GetVisitedCellsCount(visitedCells);

            roomChoices = mapGenerator.GenerateRoomChoices();

            int startingRooms = roomChoices.Count;

            foreach (MapPosition pos in roomChoices.ToList())
            {
                if (!visitedCells[pos.R, pos.C])
                {
                    roomChoices.Remove(pos);
                }
            }

            int remainingRooms = startingRooms - roomChoices.Count;

            Debug.Log("Removed " + remainingRooms + " invalid rooms...");
        }
    }

    private void GenerateSpawnRoom(bool[,] visitedCells, ref MapPosition choice)
    {
        bool spawnVisited = false;

        List<MapPosition> spawnChoices = mapGenerator.GenerateSpawnChoices();

        while (!spawnVisited)
        {
            choice = spawnChoices[Random.Range(0, spawnChoices.Count)];

            if (visitedCells[choice.R, choice.C])
            {
                Debug.Log("Spawn location found: " + choice.R + "," + choice.C);

                spawnVisited = true;
            }
            else
            {
                Debug.Log("Spawn location (" + choice.R + "," + choice.C + ") invalid. Respawning.");
            }
        }
    }

    private int GetVisitedCellsCount(bool[,] visitedCells)
    {
        int visitedCellCount = 0;

        for (int r = 1; r < mapGenerator.MapRows - 1; r++)
        {
            for (int c = 1; c < mapGenerator.MapColumns - 1; c++)
            {
                if (visitedCells[r, c])
                {
                    visitedCellCount++;
                }
            }
        }

        return visitedCellCount;
    }
}
