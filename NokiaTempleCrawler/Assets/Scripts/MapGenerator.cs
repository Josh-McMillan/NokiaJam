using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public int MapRows = 16;
    public int MapColumns = 16;

    public char[,] Map;

    public string BoxCharacters;

    private string[] boxCharacterUpFriends;
    private string[] boxCharacterDownFriends;
    private string[] boxCharacterLeftFriends;
    private string[] boxCharacterRightFriends;

    void Awake()
    {
        InitializeBoxCharacters();
    }

    public void DisplayMap()
    {
        string output = "";
        for (int r = 0; r < MapRows; r++)
        {
            for (int c = 0; c < MapColumns; c++)
            {
                output += Map[r, c];
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    public void InitializeMap()
    {
        Map = new char[MapRows, MapColumns];

        // Put 'X's in top and bottom rows.
        for (int c = 0; c < MapColumns; c++)
        {
            Map[0, c] = 'X';
            Map[MapRows - 1, c] = 'X';
        }

        // Put 'X's in the left and right columns.
        for (int r = 0; r < MapRows; r++)
        {
            Map[r, 0] = 'X';
            Map[r, MapColumns - 1] = 'X';
        }

        // Set 'O' for the other map spaces (which means 'free').
        for (int r = 1; r < MapRows - 1; r++)
        {
            for (int c = 1; c < MapColumns - 1; c++)
            {
                Map[r, c] = 'O';
            }
        }

        Random.InitState(System.DateTime.Now.Millisecond);

        // Generate Map and get Valid Positions for Level Elements
        for (int r = 1; r < MapRows - 1; r++)
        {
            for (int c = 1; c < MapColumns - 1; c++)
            {
                string validCharacters = GetValidBoxCharacters(r, c);
                Map[r, c] = validCharacters[Random.Range(0, validCharacters.Length)];
            }
        }

        // Create Elements Map
    }


    private string GetValidBoxCharacters(int row, int column)
    {
        string validCharacters = "";

        for (int i = 0; i < BoxCharacters.Length; i++)
        {
            char ch = BoxCharacters[i];

            if (
                boxCharacterLeftFriends[i].Contains(Map[row, column - 1].ToString()) &&
                boxCharacterRightFriends[i].Contains(Map[row, column + 1].ToString()) &&
                boxCharacterUpFriends[i].Contains(Map[row - 1, column].ToString()) &&
                boxCharacterDownFriends[i].Contains(Map[row + 1, column].ToString()))
            {
                validCharacters += ch.ToString();
            }
        }

        if (validCharacters.Length == 0)
        {
            validCharacters = "O";
        }

        return validCharacters;
    }

    public bool[,] TraverseMap()
    {
        bool[,] visitedCells = new bool[MapRows, MapColumns];
        int currentRow = 1;
        int currentColumn = 1;

        TraverseCells(visitedCells, currentRow, currentColumn);

        return visitedCells;
    }

    private void TraverseCells(bool[,] visitedCells, int row, int column)
    {
        if (visitedCells[row, column])
        {
            return;
        }

        visitedCells[row, column] = true;

        switch (Map[row, column])
        {
            case '┌':
                TraverseCells(visitedCells, row, column + 1);
                TraverseCells(visitedCells, row + 1, column);
                break;

            case '┐':
                TraverseCells(visitedCells, row + 1, column);
                TraverseCells(visitedCells, row, column - 1);
                break;

            case '─':
                TraverseCells(visitedCells, row, column - 1);
                TraverseCells(visitedCells, row, column + 1);
                break;

            case '│':
                TraverseCells(visitedCells, row - 1, column);
                TraverseCells(visitedCells, row + 1, column);
                break;

            case '└':
                TraverseCells(visitedCells, row, column + 1);
                TraverseCells(visitedCells, row - 1, column);
                break;

            case '┘':
                TraverseCells(visitedCells, row - 1, column);
                TraverseCells(visitedCells, row, column - 1);
                break;

            case '├':
                TraverseCells(visitedCells, row - 1, column);
                TraverseCells(visitedCells, row + 1, column);
                TraverseCells(visitedCells, row, column + 1);
                break;

            case '┤':
                TraverseCells(visitedCells, row - 1, column);
                TraverseCells(visitedCells, row + 1, column);
                TraverseCells(visitedCells, row, column - 1);
                break;

            case '┬':
                TraverseCells(visitedCells, row + 1, column);
                TraverseCells(visitedCells, row, column - 1);
                TraverseCells(visitedCells, row, column + 1);
                break;

            case '┴':
                TraverseCells(visitedCells, row - 1, column);
                TraverseCells(visitedCells, row, column - 1);
                TraverseCells(visitedCells, row, column + 1);
                break;

            case '┼':
                TraverseCells(visitedCells, row - 1, column);
                TraverseCells(visitedCells, row + 1, column);
                TraverseCells(visitedCells, row, column - 1);
                TraverseCells(visitedCells, row, column + 1);
                break;

            case 'O':
                return;

            default:
                Debug.LogError("No idea how we got here (" + row + "," + column + ") '" + Map[row, column]);
                return;
        }
    }

    public void InitializeBoxCharacters()
    {
        BoxCharacters = "─│┌┐└┘├┤┬┴┼";
        boxCharacterUpFriends = new string[BoxCharacters.Length];
        boxCharacterDownFriends = new string[BoxCharacters.Length];
        boxCharacterLeftFriends = new string[BoxCharacters.Length];
        boxCharacterRightFriends = new string[BoxCharacters.Length];

        boxCharacterLeftFriends[0] = "O─┌└├┬┴┼";
        boxCharacterLeftFriends[1] = "O│┐┘┤X";
        boxCharacterLeftFriends[2] = "O│┐┘┤X";
        boxCharacterLeftFriends[3] = "O─┌└├┬┴┼";
        boxCharacterLeftFriends[4] = "O│┐┘┤X";
        boxCharacterLeftFriends[5] = "O─┌└├┬┴┼";
        boxCharacterLeftFriends[6] = "O│┐┘┤X";
        boxCharacterLeftFriends[7] = "O─┌└├┬┴┼";
        boxCharacterLeftFriends[8] = "O─┌└├┬┴┼";
        boxCharacterLeftFriends[9] = "O─┌└├┬┴┼";
        boxCharacterLeftFriends[10] = "O─┌└├┬┴┼";

        boxCharacterRightFriends[0] = "O─┐┘┤┬┴┼";
        boxCharacterRightFriends[1] = "O│┌└├X";
        boxCharacterRightFriends[2] = "O─┐┘┤┬┴┼";
        boxCharacterRightFriends[3] = "O│┌└├X";
        boxCharacterRightFriends[4] = "O─┐┘┤┬┴┼";
        boxCharacterRightFriends[5] = "O│┌└├X";
        boxCharacterRightFriends[6] = "O─┐┘┤┬┴┼";
        boxCharacterRightFriends[7] = "O│┌└├X";
        boxCharacterRightFriends[8] = "O─┐┘┤┬┴┼";
        boxCharacterRightFriends[9] = "O─┐┘┤┬┴┼";
        boxCharacterRightFriends[10] = "O─┐┘┤┬┴┼";

        boxCharacterUpFriends[0] = "O─└┘┴X";
        boxCharacterUpFriends[1] = "O│┌┐├┤┬┼";
        boxCharacterUpFriends[2] = "O─└┘┴X";
        boxCharacterUpFriends[3] = "O─└┘┴X";
        boxCharacterUpFriends[4] = "O│┌┐├┤┬┼";
        boxCharacterUpFriends[5] = "O│┌┐├┤┬┼";
        boxCharacterUpFriends[6] = "O│┌┐├┤┬┼";
        boxCharacterUpFriends[7] = "O│┌┐├┤┬┼";
        boxCharacterUpFriends[8] = "O─└┘┴X";
        boxCharacterUpFriends[9] = "O│┌┐├┤┬┼";
        boxCharacterUpFriends[10] = "O│┌┐├┤┬┼";

        boxCharacterDownFriends[0] = "O─┌┐┬X";
        boxCharacterDownFriends[1] = "O│└┘├┤┴┼";
        boxCharacterDownFriends[2] = "O│└┘├┤┴┼";
        boxCharacterDownFriends[3] = "O│└┘├┤┴┼";
        boxCharacterDownFriends[4] = "O─┌┐┬X";
        boxCharacterDownFriends[5] = "O─┌┐┬X";
        boxCharacterDownFriends[6] = "O│└┘├┤┴┼";
        boxCharacterDownFriends[7] = "O│└┘├┤┴┼";
        boxCharacterDownFriends[8] = "O│└┘├┤┴┼";
        boxCharacterDownFriends[9] = "O─┌┐┬X";
        boxCharacterDownFriends[10] = "O│└┘├┤┴┼";
    }

}
