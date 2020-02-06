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

    [SerializeField] private GameObject caps;

    [SerializeField] private GameObject spawnShape;

    [SerializeField] private GameObject endShape;

    [SerializeField] private GameObject[] artifacts;

    [SerializeField] private GameObject battery;

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
        MapPosition endRoomChoice = new MapPosition(-1, -1);

        GenerateSpawnRoom(visitedCells, ref spawnRoomChoice, ref endRoomChoice);

        mapGenerator.DisplayMap();

        for (int r = 1; r < mapGenerator.MapRows - 1; r++)
        {
            for (int c = 1; c < mapGenerator.MapColumns - 1; c++)
            {
                string ch = mapGenerator.Map[r, c].ToString();
                int charPos = mapGenerator.BoxCharacters.IndexOf(ch);

                if (ch == "O")
                {
                    Instantiate(caps, new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), caps.transform.rotation, transform);
                }

                if (charPos < 0 || !visitedCells[r, c])
                {
                    continue;
                }

                if (r == spawnRoomChoice.R && c == spawnRoomChoice.C)
                {
                    Instantiate(spawnShape, new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), spawnShape.transform.rotation, transform);
                    continue;
                }

                if (r == endRoomChoice.R && c == endRoomChoice.C)
                {
                    Instantiate(endShape, new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), endShape.transform.rotation, transform);
                    continue;
                }

                Instantiate(shapes[charPos], new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), shapes[charPos].transform.rotation, transform);
            }
        }

        // Generate Pickups
        List<MapPosition> shuffledRooms = roomChoices.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Generating " + artifacts[i].name + " at " + shuffledRooms[i].R + "," + shuffledRooms[i].C);

            Instantiate(artifacts[i], new Vector3(shuffledRooms[i].C * gridScale + gridOffset,
                                                  0.0f,
                                                  -shuffledRooms[i].R * gridScale + gridOffset),
                        artifacts[i].transform.rotation, transform);
        }

        for (int i = 3; i < 6; i++)
        {
            Debug.Log("Generating Battery at " + shuffledRooms[i].R + "," + shuffledRooms[i].C);

            Instantiate(battery, new Vector3(shuffledRooms[i].C * gridScale + gridOffset,
                                             0.0f,
                                             -shuffledRooms[i].R * gridScale + gridOffset),
                        battery.transform.rotation, transform);
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

    private void GenerateSpawnRoom(bool[,] visitedCells, ref MapPosition spawnChoice, ref MapPosition endChoice)
    {
        bool spawnVisited = false;

        bool endVisited = false;

        List<MapPosition> options = mapGenerator.GenerateSpawnChoices();

        while (!spawnVisited)
        {
            spawnChoice = options[Random.Range(0, options.Count)];

            if (visitedCells[spawnChoice.R, spawnChoice.C])
            {
                Debug.Log("Spawn location found: " + spawnChoice.R + "," + spawnChoice.C);

                spawnVisited = true;
            }
            else
            {
                Debug.Log("Spawn location (" + spawnChoice.R + "," + spawnChoice.C + ") invalid. Regenerating.");
            }
        }

        while (!endVisited)
        {
            endChoice = options[Random.Range(0, options.Count)];

            if (visitedCells[endChoice.R, endChoice.C])
            {
                if (endChoice.R != spawnChoice.R && endChoice.C != spawnChoice.C)
                {
                    Debug.Log("End location found: " + endChoice.R + "," + endChoice.C);

                    endVisited = true;
                }
                else
                {
                    Debug.Log("End location (" + endChoice.R + "," + endChoice.C + ") same as spawn. Regenerating.");
                }
            }
            else
            {
                Debug.Log("End location (" + endChoice.R + "," + endChoice.C + ") invalid. Regenerating.");
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
