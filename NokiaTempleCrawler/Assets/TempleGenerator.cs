using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] shapes;

    [SerializeField] private float gridScale = 6.0f;

    [SerializeField] private float gridOffset = -60.0f;

    [SerializeField] private int minimumDungeonSize = 150;

    private MapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();

        int visitedCellCount = 0;

        bool[,] visitedCells = new bool[mapGenerator.MapRows, mapGenerator.MapColumns];

        while (visitedCellCount < minimumDungeonSize)
        {
            Debug.Log("Found small dungeon: " + visitedCellCount + " rooms. Trying again.");

            mapGenerator.InitializeMap();

            visitedCells = mapGenerator.TraverseMap();

            visitedCellCount = GetVisitedCellsCount(visitedCells);
        }

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

                Instantiate(shapes[charPos], new Vector3(c * gridScale + gridOffset, 0.0f, -r * gridScale - gridOffset), shapes[charPos].transform.rotation, transform);
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
